using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

public class SubscriptionPlanDto : GuidKeyedAuditableModel
{
    [Required]
    [NumericRange(1, 20)]
    public required int Level { get; set; }

    [Required]
    [MaxLength(FieldLengths.SubscriptionPlan.Name)]
    public required string Name { get; set; }

    [MaxLength(FieldLengths.SubscriptionPlan.PlanCode)]
    public required string PlanCode { get; set; }

    public required BillingCyclesEnum BillingCycle { get; set; }

    [Required]
    public decimal Price { get; set; }

    public int? MaxUsers { get; set; }
}
