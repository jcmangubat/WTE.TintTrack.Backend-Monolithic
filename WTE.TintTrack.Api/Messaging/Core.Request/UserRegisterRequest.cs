using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Api.Messaging.Core.Request;

public class UserRegisterRequest
{
    [Required]
    [Email]
    public string Email { get; set; }

    [Required]
    [Password]
    public string Password { get; set; }

    [MaxLength(FieldLengths.Tenant.Name)]
    public string? TenantName { get; set; }

    [MaxLength(FieldLengths.Tenant.TenantCode)]
    public string? TenantCode { get; set; }
}
