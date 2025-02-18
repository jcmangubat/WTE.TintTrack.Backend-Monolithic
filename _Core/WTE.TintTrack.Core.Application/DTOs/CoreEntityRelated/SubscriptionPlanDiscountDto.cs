using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

public class SubscriptionPlanDiscountDto : GuidKeyedAuditableModel
{
    [Required]
    [MaxLength(FieldLengths.SubscriptionPlanDiscount.Code)]
    public required string Code { get; set; }

    [Required]
    [MaxLength(FieldLengths.SubscriptionPlanDiscount.Name)]
    public required string Name { get; set; }

    [Required]
    public required decimal Percentage { get; set; }

    [Required]
    public required DateTime StartDate { get; set; }

    [Required]
    public required DateTime EndDate { get; set; }
}
