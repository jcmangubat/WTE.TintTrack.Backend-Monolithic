using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

public class TenantDto : GuidKeyedAuditableModel
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

    [MaxLength(FieldLengths.General.URL)]
    public string? LogoImageUrl { get; set; }

    [Required]
    public required Consts.TenantStatusEnum TenantStatus { get; set; }

    [MaxLength(FieldLengths.Tenant.ConnectionString)]
    public string? ConnectionString { get; set; }

    [MaxLength(FieldLengths.Tenant.CountryOfHost)]
    public string? CountryOfHost { get; set; }
}
