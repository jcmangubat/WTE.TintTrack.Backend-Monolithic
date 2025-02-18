using WTE.TintTrack.Core.Application.DTOs;

namespace WTE.TintTrack.Api.Messaging.Core.Responses;

public class GotoTenantResponse(ClientTokenDto? clientToken, IEnumerable<string>? roles, IEnumerable<string>? permissions)
{
    public ClientTokenDto? ClientToken { get; } = clientToken;
    public IEnumerable<string>? Roles { get; private set; } = roles;
    public IEnumerable<string>? Permissions { get; private set; } = permissions;
}