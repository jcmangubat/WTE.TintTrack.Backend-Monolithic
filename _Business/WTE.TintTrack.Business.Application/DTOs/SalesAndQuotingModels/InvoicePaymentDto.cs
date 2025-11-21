using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.SalesAndQuotingModels;

public class InvoicePaymentDto : GuidKeyedAuditableModel
{
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    public PaymentMethodsEnum PaymentMethod { get; set; }
    public string? Reference { get; set; } // e.g., transaction ID or receipt

    public Guid InvoiceId { get; set; }
    public virtual InvoiceDto Invoice { get; set; } = default!;
}
