using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using static WTE.TintTrack.Common.Constants.Consts;
using WTE.TintTrack.Application.Shared.Validator.Attributes;

namespace WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

public class TenantSubscriptionDto : GuidKeyedAuditableModel
{
    [Required]
    public required SubscriptionStatusEnum SubscriptionStatus { get; set; } = SubscriptionStatusEnum.ForReview;

    [Required]
    public required Guid TenantId { get; set; }
    public TenantDto Tenant { get; set; }

    [Required]
    public required Guid SubscriptionPlanId { get; set; }
    public SubscriptionPlanDto SubscriptionPlan { get; set; }
}
