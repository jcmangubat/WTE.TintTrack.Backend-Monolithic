using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Application.Shared.Validator.Attributes;

namespace WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

public class TenantSubscriptionInvoiceDto : GuidKeyedAuditableModel
{
    [Required]
    [MaxLength(FieldLengths.TenantSubscriptionInvoice.InvoiceNo)]
    public required string InvoiceNo { get; set; }

    [Required]
    [MaxLength(FieldLengths.TenantSubscriptionInvoice.InvoiceCode)]
    public required string InvoiceCode { get; set; }

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
    public required Consts.InvoiceStatusEnum InvoiceStatus { get; set; }

    public decimal? LateFeeAmount { get; set; }

    [Required]
    public required Guid TenantSubscriptionId { get; set; }
    public TenantSubscriptionDto TenantSubscription { get; set; }

    [Required]
    public required Guid BillingProfileId { get; set; }
    public UserBillingProfileDto BillingProfile { get; set; }

    public IList<TenantSubscriptionPaymentDto> TenantSubscriptionPayments { get; set; } = [];
}
