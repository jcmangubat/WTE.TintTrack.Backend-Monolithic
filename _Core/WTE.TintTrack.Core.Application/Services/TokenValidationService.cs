using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SMEAppHouse.Core.CodeKits.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Common.Exceptions;
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Core.Domain.Interfaces.Services;

namespace WTE.TintTrack.Core.Application.Services;

public class TokenValidationService(ILogger<TokenValidationService> logger, IOptions<JwtSettings> jwtSettings, IMessageProviderService messageProviderService) : ITokenValidationService
{
    private readonly ILogger<TokenValidationService> _logger = logger;
    private readonly IMessageProviderService _messageProviderService = messageProviderService;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public ClaimsPrincipal ValidateToken(string accessToken)
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
            var claimsPrincipal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);

            // Ensure the token is a valid JWT
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                var apiMsg = _messageProviderService.GetMessage("ERR0070");
                throw new CustomSecurityTokenException(apiMsg.Message, apiMsg.Code);
            }

            return claimsPrincipal;
        }
        catch (CustomSecurityTokenException ex)
        {
            _logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public bool TokenIsValid(string accessToken)
    {
        try
        {
            _ = ValidateToken(accessToken);
            return true;
        }
        catch { return false; }
    }
}