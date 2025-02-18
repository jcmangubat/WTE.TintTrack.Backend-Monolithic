using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Domain.Shared.BillingProfile.Abstractions;

public interface IBillingDetails
{
    BillingProfileTypesEnum BillingMethod { get; }
}