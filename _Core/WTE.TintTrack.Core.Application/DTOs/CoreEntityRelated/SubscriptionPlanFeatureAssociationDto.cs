namespace WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

public class SubscriptionPlanFeatureAssociationDto
{
    public required Guid SubscriptionPlanId { get; set; }
    public SubscriptionPlanDto SubscriptionPlan { get; set; }

    public required Guid SubscriptionPlanFeatureId { get; set; }
    public SubscriptionPlanFeatureDto SubscriptionPlanFeature { get; set; }
}