using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Core.Domain.Entities;

public class SubscriptionPlanDiscount : GuidKeyedAuditableEntity
{
    public required string PlanDiscountCode { get; set; }
    public required string Name { get; set; }
    public required decimal Percentage { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }

    public required Guid SubscriptionPlanId { get; set; }
    public virtual SubscriptionPlan SubscriptionPlan { get; set; }
    
}