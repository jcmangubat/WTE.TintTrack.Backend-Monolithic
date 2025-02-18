using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Domain.Entities;

public class ProductPriceSchedule : GuidKeyedAuditableEntity
{
    public decimal UnitCost { get; set; } // Cost per unit length or area
    public decimal MarkupPercentage { get; set; } // Markup percentage
    public decimal FinalPrice { get; set; } // Computed final price (optional)

    public DateTime EffectiveFrom { get; set; } // Date when the price becomes active
    public DateTime? EffectiveTo { get; set; } // Optional: Date when the price ends
    public bool IsCurrent { get; set; } // Indicates whether this is the current active price

    public required Guid ProductId { get; set; } // Foreign Key to Product
    public virtual Product Product { get; set; } // Navigation Property
}