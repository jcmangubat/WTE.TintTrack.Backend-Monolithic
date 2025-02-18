using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

public class SubscriptionPlanFeatureDto : GuidKeyedAuditableModel
{
    [Required]
    [MaxLength(FieldLengths.SubscriptionPlanFeature.Code)]
    public required string FeatureCode { get; set; }

    [Required]
    [MaxLength(FieldLengths.SubscriptionPlanFeature.Name)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(FieldLengths.SubscriptionPlanFeature.Description)]
    public required string Description { get; set; }

    /*public IList<SubscriptionPlanDto> SubscriptionPlans { get; set; } = [];*/
}
