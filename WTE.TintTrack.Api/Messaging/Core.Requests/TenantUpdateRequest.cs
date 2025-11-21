using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Messaging.Core.Requests;

public class TenantUpdateRequest
{
    [MaxLength(FieldLengths.Tenant.Name)]
    public string? Name { get; set; }

    [MaxLength(FieldLengths.Tenant.Description)]
    public string? Description { get; set; }

    [MaxLength(FieldLengths.Tenant.Domain)]
    public string? Domain { get; set; }

    public required TenantStatusEnum? TenantStatus { get; set; }
}
