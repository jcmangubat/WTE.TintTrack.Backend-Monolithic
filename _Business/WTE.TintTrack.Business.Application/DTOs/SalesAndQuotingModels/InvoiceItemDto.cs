using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;

namespace WTE.TintTrack.Business.Application.DTOs.SalesAndQuotingModels;

public class InvoiceItemDto : GuidKeyedAuditableModel
{
    // The description of the item (e.g., "Consulting Service", "Software License")
    public string ItemDescription { get; set; }

    // Quantity of the item
    public decimal Quantity { get; set; }

    // Unit price of the item
    public decimal UnitPrice { get; set; }

    // Total amount for the item (Quantity * UnitPrice)
    //public decimal TotalAmount => Quantity * UnitPrice;

    // Optional tax applied to the item
    public decimal? ItemTaxAmount { get; set; }

    // Total cost of the item including tax
    //public decimal TotalWithTax => TotalAmount + (ItemTaxAmount ?? 0);

    // Discount applied to the item, if any
    public decimal? ItemDiscountAmount { get; set; }

    // Total cost of the item after discount
    //public decimal TotalAfterDiscount => TotalWithTax - (ItemDiscountAmount ?? 0);

    // A flag to indicate whether this item has been paid
    public bool IsPaid { get; set; }

    // Date when the item was billed
    public DateTime BillDate { get; set; }


    public Guid InvoiceId { get; set; }
    public virtual InvoiceDto Invoice { get; set; }
}

