using WTE.TintTrack.Api.Messaging._Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Messaging.Core.Responses;

public class TenantSubscriptionInvoiceResponse : ApiMessageResponse
{
    public string InvoiceNo { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; }

    public DateTime DueDate { get; set; }

    public string Notes { get; set; }

    public InvoiceStatusEnum InvoiceStatus { get; set; }

    public decimal? LateFeeAmount { get; set; }

    public string PlanCode { get; set; }
}
