using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;

public class InvoicePayment : GuidKeyedAuditableEntity
{
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    public PaymentMethodsEnum PaymentMethod { get; set; }
    public string? Reference { get; set; } // e.g., transaction ID or receipt

    public Guid InvoiceId { get; set; }
    public virtual Invoice Invoice { get; set; } = default!;
}
