using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Api.Messaging.Core.Requests;

public class GotoTenantRequest
{
    [Required]
    [MaxLength(FieldLengths.Tenant.TenantCode)]
    public string TenantCode { get; set; }
}
