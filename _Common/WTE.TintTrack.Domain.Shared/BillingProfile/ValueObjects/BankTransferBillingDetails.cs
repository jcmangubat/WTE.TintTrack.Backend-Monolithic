using WTE.TintTrack.Domain.Shared.BillingProfile.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Domain.Shared.BillingProfile.ValueObjects;

public class BankTransferBillingDetails : BillingDetailsBase
{
    public override BillingProfileTypesEnum BillingMethod => BillingProfileTypesEnum.BankTransfer;

    public required string AccountNumber { get; set; }
    public required string BankName { get; set; }
    public required string AccountHolderName { get; set; } // New field

    public string? RoutingNumber { get; set; }
    public string? SwiftCode { get; set; }
    public string? Iban { get; set; }
    public string? Country { get; set; }
    public string? BankAddress { get; set; }
    public string? Currency { get; set; }
}