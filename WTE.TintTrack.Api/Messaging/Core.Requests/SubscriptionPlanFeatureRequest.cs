using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Api.Messaging.Core.Requests;

public class SubscriptionPlanFeatureRequest
{
    [Required]
    [MaxLength(FieldLengths.SubscriptionPlan.PlanCode)]
    public required string PlanCode { get; set; }

    [Required]
    [MaxLength(FieldLengths.SubscriptionPlanFeature.Code)]
    public required string FeatureCode { get; set; }
}