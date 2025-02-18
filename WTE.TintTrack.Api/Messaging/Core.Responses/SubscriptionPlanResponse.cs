using WTE.TintTrack.Api.Messaging._Abstractions;

namespace WTE.TintTrack.Api.Messaging.Core.Responses;

public class SubscriptionPlanResponse : ApiMessageResponse
{
    public string Name { get; set; }

    public string PlanCode { get; set; }

    public int Level { get; set; }

    public decimal Price { get; set; }

    public int? MaxUsers { get; set; }
}
