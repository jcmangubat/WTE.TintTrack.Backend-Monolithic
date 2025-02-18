namespace WTE.TintTrack.Core.Domain.Entities;

public class SubscriptionPlanFeatureAssociation
{
    public required Guid SubscriptionPlanId { get; set; }
    public SubscriptionPlan SubscriptionPlan { get; set; }

    public required Guid SubscriptionPlanFeatureId { get; set; }
    public SubscriptionPlanFeature SubscriptionPlanFeature { get; set; }
}

