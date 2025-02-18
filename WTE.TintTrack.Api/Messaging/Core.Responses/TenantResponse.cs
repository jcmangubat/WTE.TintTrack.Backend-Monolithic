using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Api.Messaging.Core.Responses;

public class TenantResponse : ApiMessageResponse
{
    public string TenantCode { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string? Domain { get; set; }

    public required Consts.TenantStatusEnum TenantStatus { get; set; }

    public string? ConnectionString { get; set; }

    public string? SubscriptionPlanCode { get; set; }
}