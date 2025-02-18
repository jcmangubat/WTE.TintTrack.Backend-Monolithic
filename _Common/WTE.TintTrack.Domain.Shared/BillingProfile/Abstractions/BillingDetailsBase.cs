using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Domain.Shared.BillingProfile.Abstractions;

public abstract class BillingDetailsBase : IBillingDetails
{
    public abstract BillingProfileTypesEnum BillingMethod { get; }
}
