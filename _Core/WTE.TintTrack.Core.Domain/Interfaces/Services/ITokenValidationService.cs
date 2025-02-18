using System.Security.Claims;

namespace WTE.TintTrack.Core.Domain.Interfaces.Services;

public interface ITokenValidationService
{
    ClaimsPrincipal ValidateToken(string accessToken);
    bool TokenIsValid(string accessToken);
}