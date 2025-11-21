using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SMEAppHouse.Core.CodeKits.Helpers;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Common.Exceptions;
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Core.Application.DTOs;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Application.Services;

/// <summary>
/// Token Service (Simplified)
/// </summary>
/// <param name="mapper"></param>
/// <param name="logger"></param>
/// <param name="configuration"></param>
public class TokenService(IMapper mapper,
                    ILogger<TokenService> logger,
                    IMessageProviderService messageProviderService,
                    IOptions<JwtSettings> jwtSettings,
                    IOptions<ApplicationSettings> appSettings,
                    RoleManager<ApplicationRole> roleManager,
                    UserManager<ApplicationUser> userManager,
                    ITokenRepository tokenRepository,
                    IUserRepository userRepository,
                    ITenantRepository tenantRepository,
                    IUserTenantRepository userTenantRepository,
                    ITenantSubscriptionRepository tenantSubscriptionRepository)
    : MappedLoggingService<ITokenService>(mapper, logger, messageProviderService), ITokenService
{
    private readonly ApplicationSettings _appSettings = appSettings.Value;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    private readonly ITokenRepository _tokenRepository = tokenRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITenantRepository _tenantRepository = tenantRepository;
    private readonly IUserTenantRepository _userTenantRepository = userTenantRepository;
    private readonly ITenantSubscriptionRepository _tenantSubscriptionRepository = tenantSubscriptionRepository;

    public async Task<ClientTokenDto> GenerateTokenAsync(ApplicationUserDto user,
                                                        TenantDto? tenant = null)
    {
        try
        {
            TenantSubscriptionDto? tenantSubscriptionDto = null;

            if (tenant != null)
            {
                var tenantSubscriptions = await _tenantSubscriptionRepository.GetByTenantAsync(tenant.TenantCode);
                if (tenantSubscriptions == null || !tenantSubscriptions.Any())
                    throw RecordNotFoundException("ERR011");

                var activeTenantSubscription = tenantSubscriptions.FirstOrDefault(p => p.SubscriptionStatus == SubscriptionStatusEnum.Active) ??
                    throw RecordNotFoundException("ERR012");

                tenantSubscriptionDto = Mapper.Map<TenantSubscriptionDto>(activeTenantSubscription);
            }

            Token? token = null;
            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_appSettings.AccessTokenExpiryAgeInMinutes);
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_appSettings.RefreshTokenExpiryAgeInDays);

            token = new Token
            {
                Id = Guid.NewGuid(),
                RefreshTokenExpiration = refreshTokenExpiration,
                RefreshToken = Guid.NewGuid().ToString(),
                UserId = user.Id,
                TenantId = tenant?.Id
            };
            await _tokenRepository.AddAsync(token);
            await _tokenRepository.CommitAsync();

            var claims = await CreateClaimsAsync(user, tenant, tenantSubscriptionDto, token.RefreshToken);
            var accessTokenString = GenerateTokenString(claims, accessTokenExpiration);

            // Return both tokens
            return new ClientTokenDto
            {
                AccessToken = accessTokenString,
                RefreshToken = token.RefreshToken,
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration
            };
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (ServiceOperationException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task RevokeTokenForUserAsync(string refreshToken, bool? allDevices = false)
    {
        try
        {
            // Retrieve all tokens associated with the user in the specified tenant
            var token = await _tokenRepository.GetSingleAsync(p => p.RefreshToken == refreshToken,
                                                    p => p.Include(t => t.User).Include(t => t.Tenant))
                ?? throw RecordNotFoundException("ERR080");

            // Delete the token associated with the user
            if (allDevices ?? false)
                await _tokenRepository.DeleteAsync(t => t.UserId == token.UserId && t.TenantId == token.TenantId);
            else
                await _tokenRepository.DeleteTokenAsync(token.Id);

            await _tokenRepository.CommitAsync();

            // log this action for audit purposes
            var apiMsg = MessageProviderService.GetMessage("INF012");
            Logger.LogInformation(apiMsg.Message);
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogWarning(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /*
        var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var userCode = User.Claims.FirstOrDefault(c => c.Type == "user_code")?.Value;
        var tenantCode = User.Claims.FirstOrDefault(c => c.Type == "tenant_code")?.Value;
        var planCode = User.Claims.FirstOrDefault(c => c.Type == "plan_code")?.Value;
        var sessionCode = User.Claims.FirstOrDefault(c => c.Type == "session_code")?.Value;
     */

    /// <inheritdoc />
    public async Task<(ApplicationUserDto User,
                        TenantDto? Tenant,
                        SubscriptionPlanDto? SubscriptionPlanDto,
                        List<string> Roles,
                        Guid RefreshToken)?>
                        GetDetailsFromAccessTokenAsync(string accessToken)
    {
        try
        {
            // Token validation parameters
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                ValidateLifetime = true // Ensure the token is not expired
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);

            // Ensure the token is a valid JWT
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            // Extract user information from claims
            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type.Contains("claims/nameidentifier"));
            var userEmailClaim = principal.Claims.FirstOrDefault(c => c.Type.Contains("claims/emailaddress"));
            if (userIdClaim == null || userEmailClaim == null)
                throw ServiceOperationException("ERR081");

            // Parse the userId from the token's 'sub' claim
            if (!Guid.TryParse(userIdClaim.Value, out var userId))
                throw ServiceOperationException("ERR082");

            var userEmail = userEmailClaim.Value;

            // Fetch the user based on userId
            var user = await _userRepository.GetByIdAsync(userId)
                        ?? throw RecordNotFoundException("ERR083");

            var userDto = Mapper.Map<ApplicationUserDto>(user);

            // Extract tenant information from tenant_code claim
            var tenantCodeClaim = principal.Claims.FirstOrDefault(c => c.Type.Equals("tenant_code"));

            Tenant? tenant = null;

            if (tenantCodeClaim != null &&
                !string.IsNullOrEmpty(tenantCodeClaim.Value))
            {
                var tenantCode = tenantCodeClaim.Value;

                tenant = await _tenantRepository.GetByTenantCodeAsync(tenantCode)
                            ?? throw RecordNotFoundException("ERR074");
            }
            var tenantDto = Mapper.Map<TenantDto>(tenant);

            // Extract subscription information from tenant_code claim

            var subscriptionPlanClaim = principal.Claims.FirstOrDefault(c => c.Type.Equals("plan_code"));
            TenantSubscription? tenantSubscription = null;
            if (subscriptionPlanClaim != null)
            {
                var subscriptionPlanCode = subscriptionPlanClaim.Value;

                tenantSubscription = await _tenantSubscriptionRepository.GetSingleAsync(
                                                            p => p.SubscriptionPlan.PlanCode == subscriptionPlanClaim.Value,
                                                            p => p.Include(x => x.SubscriptionPlan)
                                                        );
            }
            var subscriptionPlanDto = Mapper.Map<SubscriptionPlanDto>(tenantSubscription?.SubscriptionPlan);

            // Extract refresh token from refreshtoken claim
            Guid refreshToken = Guid.Empty;
            var refreshTokenClaim = principal.Claims.FirstOrDefault(c => c.Type.Equals("refreshtoken"));
            if (refreshTokenClaim != null && !string.IsNullOrEmpty(refreshTokenClaim.Value))
                refreshToken = Guid.Parse(refreshTokenClaim.Value);

            var roleClaims = principal.Claims.Where(c => c.Type.Contains("claims/role"));
            var roles = roleClaims.Select(r => r.Value).ToList();

            (ApplicationUserDto User, TenantDto? Tenant, SubscriptionPlanDto? SubscriptionPlan, List<string> Roles, Guid RefreshToken) result =
                    new(userDto, tenantDto, subscriptionPlanDto, roles, refreshToken);

            return result;
        }
        catch (SecurityTokenException stEx)
        {
            Logger.LogError(stEx, stEx.GetExceptionMessages());
            throw;
        }
        catch (ServiceOperationException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        try
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                ValidateLifetime = false, // Allow expired tokens
                ClockSkew = TimeSpan.Zero // No clock skew adjustment
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw MessageProviderService.CustomSecurityTokenException("ERR070");

            return principal;
        }
        catch (SecurityTokenException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex, "An error occurred while processing the token.");
        }
    }

    public async Task<ClientTokenDto?> RefreshAccessTokenAsync(Guid refreshToken)
    {
        try
        {
            // Step 1: Get the user associated with the refresh token
            Token? storedToken = await _tokenRepository.GetTokenAsync(refreshToken.ToString());
            if (storedToken == null || DateTime.Now >= storedToken.RefreshTokenExpiration)
                throw CustomSecurityTokenException("ERR084");

            // Step 2: Get the user and tenant associated with the refresh token
            var user = await _userRepository.GetByIdAsync(storedToken.UserId)
                ?? throw ServiceOperationException("ERR085");

            Tenant? tenant = null;
            TenantSubscriptionDto? tenantSubscriptionDto = null;

            if (storedToken.TenantId != null)
            {
                tenant = await _tenantRepository.GetByIdAsync(storedToken.TenantId.Value)
                                ?? throw ServiceOperationException("ERR086");

                var tenantSubscriptions = await _tenantSubscriptionRepository.GetByTenantAsync(tenant.TenantCode);
                if (tenantSubscriptions == null || !tenantSubscriptions.Any())
                    throw ServiceOperationException("ERR011");

                var activeTenantSubscription = tenantSubscriptions.FirstOrDefault(p => p.SubscriptionStatus == SubscriptionStatusEnum.Active)
                    ?? throw ServiceOperationException("ERR012");

                tenantSubscriptionDto = Mapper.Map<TenantSubscriptionDto>(activeTenantSubscription);

            }

            var claims = await CreateClaimsAsync(Mapper.Map<ApplicationUserDto>(user),
                                                Mapper.Map<TenantDto>(tenant),
                                                tenantSubscriptionDto,
                                                storedToken.RefreshToken);

            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_appSettings.AccessTokenExpiryAgeInMinutes);
            var accessTokenString = GenerateTokenString(claims, accessTokenExpiration);

            // Step 4: Generate a new refresh token

            //storedToken.Expiration = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpirationDays);

            await _tokenRepository.UpdateAsync(storedToken);

            ClientTokenDto? clientTokenDto = new()
            {
                AccessToken = accessTokenString,
                RefreshToken = refreshToken.ToString(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = storedToken.RefreshTokenExpiration,
            };

            // Return the new tokens
            return clientTokenDto;
        }
        catch (ServiceOperationException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages()); ;
            throw;
        }
        catch (SecurityTokenException stEx)
        {
            Logger.LogError(stEx, "Invalid refresh token.");
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex, "An error occurred while refreshing the token.");
        }
    }

    public async Task<TokenDto?> GetTokenByRefreshTokenAsync(Guid refreshToken)
    {
        try
        {
            // Step 1: Get the user associated with the refresh token
            Token? storedToken = await _tokenRepository.GetTokenAsync(refreshToken.ToString());
            return Mapper.Map<TokenDto>(storedToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex, "An error occurred while refreshing the token.");
        }
    }

    private async Task<IEnumerable<Claim>> CreateClaimsAsync(ApplicationUserDto user,
                                                                TenantDto? tenant,
                                                                TenantSubscriptionDto? tenantSubscriptionDto,
                                                                string refreshToken)
    {
        var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new("user_code", user.UserCode ?? string.Empty),
                new("tenant_code", tenant?.TenantCode  ?? string.Empty),
                new("plan_code", tenantSubscriptionDto?.SubscriptionPlan?.PlanCode ?? string.Empty),
                new("refreshtoken", refreshToken)
            };

        // Add to claims any global roles if exists
        var userEntity = Mapper.Map<ApplicationUser>(user);
        var roles = await _userManager.GetRolesAsync(userEntity);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        // Add to claims any tenant-specific roles if tenant exists
        if (tenant != null)
        {
            var userTenantRoles = await _userTenantRepository.GetUserRolesInTenantAsync(user.Id, tenant.Id);
            var userTenantRoleNames = userTenantRoles.Select(utr => utr.Role.Name).ToList();
            claims.AddRange(userTenantRoleNames.Select(utRoleName => new Claim(ClaimTypes.Role, utRoleName)));
        }

        return claims;
    }

    private string GenerateTokenString(IEnumerable<Claim> claims,
                                                        DateTime? accessTokenExpiration)
    {
        // Create the security key and signing credentials for the access token
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Generate the access token
        var accessToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: accessTokenExpiration,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(accessToken);
    }
}
