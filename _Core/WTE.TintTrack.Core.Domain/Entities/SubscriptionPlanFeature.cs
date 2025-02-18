using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Core.Domain.Entities;

public class SubscriptionPlanFeature : GuidKeyedAuditableEntity
{
    public required string FeatureCode { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    //public virtual ICollection<SubscriptionPlan> SubscriptionPlans { get; set; } = new HashSet<SubscriptionPlan>();

    public virtual ICollection<SubscriptionPlanFeatureAssociation> SubscriptionPlanFeatureAssociations { get; set; }
}

