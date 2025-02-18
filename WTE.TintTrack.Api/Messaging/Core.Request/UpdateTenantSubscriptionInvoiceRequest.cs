using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Api.Messaging.Core.Request;

public class UpdateTenantSubscriptionInvoiceRequest
{
    [Required]
    [MaxLength(FieldLengths.TenantSubscriptionInvoice.InvoiceNo)]
    public required string InvoiceNo { get; set; }

    public decimal? Amount { get; set; }

    public string? Currency { get; set; }

    public DateTime? DueDate { get; set; }

    public string? Notes { get; set; }

    public Consts.InvoiceStatusEnum? InvoiceStatus { get; set; }

    public decimal? LateFeeAmount { get; set; }
}