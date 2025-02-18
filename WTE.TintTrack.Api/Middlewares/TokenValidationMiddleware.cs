using Newtonsoft.Json;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Common.Exceptions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Domain.Interfaces.Services;

namespace WTE.TintTrack.Api.Middlewares;

public class TokenValidationMiddleware(RequestDelegate next,
                                            ILogger<TokenValidationMiddleware> logger,
                                            IMessageProviderService messageProviderService,
                                            IServiceScopeFactory serviceScopeFactory,
                                            ITokenValidationService tokenValidationService)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<TokenValidationMiddleware> _logger = logger;
    private readonly IMessageProviderService _messageProviderService = messageProviderService;

    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    private readonly ITokenValidationService _tokenValidationService = tokenValidationService;

    public async Task InvokeAsync(HttpContext context)
    {
        var authHeader = context.Request.Headers.Authorization.ToString();

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            await _next(context);
            return;
        }

        using var scope = _serviceScopeFactory.CreateScope();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();

        var accessToken = authHeader["Bearer ".Length..].Trim();

        (ApplicationUserDto User, TenantDto? Tenant, SubscriptionPlanDto? SubscriptionPlanDto, List<string> Roles, Guid RefreshToken)? tupleResult =
            await tokenService.GetDetailsFromAccessTokenAsync(accessToken);

        var tokenDto = await tokenService.GetTokenByRefreshTokenAsync(tupleResult.Value.RefreshToken);

        try
        {
            if (tokenDto == null)
                throw new UnauthorizedAccessException(nameof(accessToken));

            var claimsPrincipal = _tokenValidationService.ValidateToken(accessToken);

            /*var expClaim = claimsPrincipal.FindFirst("exp")?.Value;
            if (long.TryParse(expClaim, out long expTimestamp))
            {
                DateTime expirationDate = DateTimeOffset.FromUnixTimeSeconds(expTimestamp).UtcDateTime;
                if(expirationDate != tokenDto.Expiration)
                    throw new UnauthorizedAccessException(nameof(accessToken));
            }*/

            context.User = claimsPrincipal; // Set the authenticated user
        }
        catch (CustomSecurityTokenException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync((string)JsonConvert.SerializeObject(new
            {
                ex.ErrorCode,
                context.Response.StatusCode,
                ex.Message,
                Success = false,
                Errors = new { },
                Data = new { }
            }));

            return;
        }
        catch
        {
            var apiMsg = _messageProviderService.GetMessage("ERR0070");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync((string)JsonConvert.SerializeObject(new
            {
                context.Response.StatusCode,
                ErrorCode = apiMsg.Code,
                apiMsg.Message,
                Success = false,
                Errors = new { },
                Data = new { }
            }));

            return;
        }

        await _next(context); // Continue processing the request
    }

}