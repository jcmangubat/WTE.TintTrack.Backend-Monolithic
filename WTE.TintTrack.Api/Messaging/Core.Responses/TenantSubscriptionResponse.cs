using WTE.TintTrack.Api.Messaging._Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Messaging.Core.Responses;

public class TenantSubscriptionResponse : ApiMessageResponse
{
    public SubscriptionStatusEnum SubscriptionStatus { get; }

    public string TenantCode { get; }

    public string SubscriptionPlanCode { get; }
}