using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.old.SalesAndQuoting;

public class InvoicePaymentDto : GuidKeyedAuditableEntity
{
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    public PaymentMethodsEnum PaymentMethod { get; set; }
    public string? Reference { get; set; } // e.g., transaction ID or receipt

    public Guid InvoiceId { get; set; }
    public virtual InvoiceDto Invoice { get; set; } = default!;
}
