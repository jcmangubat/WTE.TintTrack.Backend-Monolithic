using WTE.TintTrack.Domain.Shared.BillingProfile.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Domain.Shared.BillingProfile.ValueObjects;

public class CreditCardBillingDetails : BillingDetailsBase
{
    public override BillingProfileTypesEnum BillingMethod => BillingProfileTypesEnum.CreditCard;

    public required string CreditCardNumber { get; set; }
    public required DateTime ExpirationDate { get; set; }
    public required string Cvc { get; set; }

    public required string CardholderName { get; set; }  // New field
    public string? BillingAddress { get; set; }
    public string? BillingZipCode { get; set; }
    public string? CardType { get; set; }
    public string? Currency { get; set; }
    public string? IssuerBank { get; set; }
}
