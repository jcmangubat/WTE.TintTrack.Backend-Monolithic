using WTE.TintTrack.Application.Shared.Validator.Attributes;
namespace WTE.TintTrack.Api.Messaging.Core.Requests;

public class UserTenantRequest
{
    public bool? IsDefault { get; set; }
    public bool? UserIsOwner { get; set; }

    [Required]
    public string UserCode { get; set; }

    [Required]
    public string TenantCode { get; set; }
}