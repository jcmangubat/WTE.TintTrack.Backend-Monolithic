using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Api.Messaging.Core.Requests
{
    public class RegisterTenantRequest
    {
        [Required]
        [MaxLength(FieldLengths.Tenant.TenantCode)]
        public required string TenantCode { get; set; }

        [Required]
        [MaxLength(FieldLengths.Tenant.Name)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(FieldLengths.Tenant.Description)]
        public required string Description { get; set; }

        [MaxLength(FieldLengths.Tenant.Domain)]
        public string? Domain { get; set; }

        [Required]
        public required Consts.TenantStatusEnum TenantStatus { get; set; }
    }
}