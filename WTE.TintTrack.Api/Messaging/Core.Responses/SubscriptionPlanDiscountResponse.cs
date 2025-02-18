using WTE.TintTrack.Api.Messaging._Abstractions;

namespace WTE.TintTrack.Api.Messaging.Core.Responses;

public class SubscriptionPlanDiscountResponse : ApiMessageResponse
{
    public required string Code { get; set; }

    public required string Name { get; set; }

    public required decimal Percentage { get; set; }

    public required DateTime StartDate { get; set; }

    public required DateTime EndDate { get; set; }
}