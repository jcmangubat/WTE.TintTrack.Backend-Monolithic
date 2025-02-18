using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Api.Messaging.Core.Responses;

public class UserBillingProfileResponse : ApiMessageResponse
{
    public string BillingAddress { get; set; }

    public Consts.BillingProfileTypesEnum BillingProfileType { get; set; }

    public string BillingDetailsJson { get; set; }

    public string UserCode { get; set; }
}
