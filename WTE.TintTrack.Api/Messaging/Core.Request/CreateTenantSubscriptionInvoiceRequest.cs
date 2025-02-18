using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Common.Helpers;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Messaging.Core.Request;

public class CreateTenantSubscriptionInvoiceRequest
{
    [Required]
    public required decimal Amount { get; set; }

    [Required]
    [MaxLength(FieldLengths.TenantSubscriptionInvoice.Currency)]
    public required string Currency { get; set; }

    [Required]
    public required DateTime DueDate { get; set; }

    [MaxLength(FieldLengths.TenantSubscriptionInvoice.Notes)]
    public string? Notes { get; set; }

    [Required]
    public required InvoiceStatusEnum InvoiceStatus { get; set; }

    public decimal? LateFeeAmount { get; set; }

    /*[Required]
    public required string PlanCode { get; set; }*/

    [Required]
    public required string UserCode { get; set; }

    [Required]
    public required string TenantCode { get; set; }

    public string InvoiceCode =>
        CodeGenerator.GenerateUniqueCode($"{UserCode}-{TenantCode}-{DueDate}-{LateFeeAmount}-{Amount}-{Currency}",
            FieldLengths.TenantSubscriptionInvoice.InvoiceNo);
}

public class CreateTenantSubscriptionPaymentRequest
{
    [Required]
    public decimal Amount { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; }

    [Required]
    public PaymentStatusEnum PaymentStatus { get; set; }

    [Required]
    public required string InvoiceNo { get; set; }
}

public class UpdateTenantSubscriptionPaymentRequest
{
    [Required]
    public required string InvoiceNo { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; }

    public decimal? Amount { get; set; }

    public PaymentStatusEnum? PaymentStatus { get; set; }
}