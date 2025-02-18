using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities;

public class ProductInventory : GuidKeyedAuditableEntity
{
    public decimal QuantityInStock { get; set; } // Current stock level
    public decimal ReservedQuantity { get; set; } // Quantity reserved for tasks/orders
    public decimal ReorderLevel { get; set; } // Minimum stock level for reorder
    public UnitOfMeasuresEnum UnitOfMeasure { get; set; } // Unit of measure for inventory

    public required Guid ProductId { get; set; } // Foreign key to Product
    public virtual Product Product { get; set; } // Navigation property to Product
}
