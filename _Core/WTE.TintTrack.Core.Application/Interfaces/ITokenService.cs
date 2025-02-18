using System.Security.Claims;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Core.Application.DTOs;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

namespace WTE.TintTrack.Core.Application.Interfaces;

public interface ITokenService : IMappedLoggingService<ITokenService>
{
    Task<ClientTokenDto> GenerateTokenAsync(ApplicationUserDto user,
                                            TenantDto? tenant = null);

    Task<ClientTokenDto?> RefreshAccessTokenAsync(Guid refreshToken);

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    Task RevokeTokenForUserAsync(string refreshToken, bool? allDevices = false);

    /// <summary>
    /// Retrieves user information and tenant details from a JWT token.
    /// </summary>
    /// <param name="token">The JWT token used to authenticate and retrieve user data.</param>
    /// <returns>
    /// A <see cref="UserFromTokenResponse"/> object containing the user information and tenant details
    /// extracted from the token, or <c>null</c> if the user cannot be found.
    /// </returns>
    /// <exception cref="ApplicationException">
    /// Thrown when the token is invalid, expired, or does not contain valid user or tenant information.
    /// </exception>
    /// <remarks>
    /// This method validates the provided JWT token by verifying its signature and expiration. 
    /// It extracts user claims, including the user ID, email, tenant ID, and tenant code, 
    /// and then retrieves the corresponding user data from the repository. 
    /// If the token is invalid or cannot be processed, appropriate exceptions are logged and thrown.
    /// </remarks>
    Task<(ApplicationUserDto User, TenantDto? Tenant, SubscriptionPlanDto? SubscriptionPlanDto, List<string> Roles, Guid RefreshToken)?> GetDetailsFromAccessTokenAsync(string accessToken);

    Task<TokenDto?> GetTokenByRefreshTokenAsync(Guid refreshToken);
}