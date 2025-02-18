using WTE.TintTrack.Api.Messaging._Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Messaging.Core.Responses;

public class TenantSubscriptionPaymentResponse : ApiMessageResponse
{
    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public PaymentStatusEnum PaymentStatus { get; set; }

    public required string InvoiceNo { get; set; }
}