using WTE.TintTrack.Core.Application.DTOs;

namespace WTE.TintTrack.Api.Messaging.Core.Responses;

public class LoginResponse
{
    public LoginResponse()
    {
    }

    public LoginResponse(ClientTokenDto? clientToken, List<string>? administrativeRoles, List<UserTenantStripDto>? userTenantStrips)
    {
        ClientToken = clientToken;
        Roles = administrativeRoles;
        UserTenantStrips = userTenantStrips;
    }

    public ClientTokenDto? ClientToken { get; private set; }

    public List<string>? Roles { get; private set; }

    public List<UserTenantStripDto>? UserTenantStrips { get; private set; }
}
