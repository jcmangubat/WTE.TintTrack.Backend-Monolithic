using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Api.Messaging.Core.Requests;

public class UserTenantRoleRequest
{
    [Required]
    [MaxLength(FieldLengths.ApplicationUser.UserCode)]
    public string UserCode { get; set; }

    [Required]
    [MaxLength(FieldLengths.Tenant.TenantCode)]
    public string TenantCode { get; set; }

    [Required]
    public string RoleName { get; set; }
}
