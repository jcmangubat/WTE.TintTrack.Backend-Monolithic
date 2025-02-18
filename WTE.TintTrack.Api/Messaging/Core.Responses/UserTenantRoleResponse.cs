using WTE.TintTrack.Api.Messaging._Abstractions;

namespace WTE.TintTrack.Api.Messaging.Core.Responses;

public class UserTenantRoleResponse : ApiMessageResponse
{
    public string TenantCode { get; set; }
    public string RoleName { get; set; }
}
