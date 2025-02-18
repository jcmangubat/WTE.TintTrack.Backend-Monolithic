using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Domain.Entities;


/// <summary>
/// A tenant subscription can have multiple payments associated to an invoice
/// </summary>
public class TenantSubscriptionPayment : GuidKeyedAuditableEntity
{
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatusEnum PaymentStatus { get; set; }


    public required Guid InvoiceId { get; set; }
    public virtual TenantSubscriptionInvoice TenantSubscriptionInvoice { get; set; }
}
