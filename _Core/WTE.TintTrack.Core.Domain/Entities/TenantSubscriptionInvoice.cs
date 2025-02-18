using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Core.Domain.Entities;

public class TenantSubscriptionInvoice : GuidKeyedAuditableEntity
{
    /// <summary>
    /// A user-friendly invoice number for referencing.
    /// </summary>
    public required string InvoiceNo { get; set; }
    
    public required string InvoiceCode { get; set; }

    /// <summary>
    /// The total amount billed in the invoice.
    /// </summary>
    public required decimal Amount { get; set; }

    /// <summary>
    /// The currency used in the transaction.
    /// </summary>
    public required string Currency { get; set; }

    /// <summary>
    /// The date by which the payment is due
    /// </summary>
    public required DateTime DueDate { get; set; }

    /// <summary>
    /// Any additional information or remarks related to the invoice.
    /// </summary>
    public string? Notes { get; set; }

    public required Consts.InvoiceStatusEnum InvoiceStatus { get; set; }

    /// <summary>
    /// Applicable late fees if the payment is overdue.
    /// </summary>
    public decimal? LateFeeAmount { get; set; }

    public required Guid TenantSubscriptionId { get; set; }
    public virtual TenantSubscription TenantSubscription { get; set; }

    // Foreign key to ApplicationUserBillingProfile
    public required Guid BillingProfileId { get; set; }
    public virtual UserBillingProfile BillingProfile { get; set; }

    /// <summary>
    /// This invoice may have several payment records involved. 
    /// For example, partial amount is paid in two separate payment methods.
    /// </summary>
    public virtual ICollection<TenantSubscriptionPayment> TenantSubscriptionPayments { get; set; }
}
