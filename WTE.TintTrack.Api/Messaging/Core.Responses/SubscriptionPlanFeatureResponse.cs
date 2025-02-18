using WTE.TintTrack.Api.Messaging._Abstractions;

namespace WTE.TintTrack.Api.Messaging.Core.Responses;

public class SubscriptionPlanFeatureResponse : ApiMessageResponse
{
    public required string FeatureCode { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }
}
