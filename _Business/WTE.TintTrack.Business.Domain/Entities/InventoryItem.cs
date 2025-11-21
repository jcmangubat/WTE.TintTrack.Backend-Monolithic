using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities;

public class InventoryItem : GuidKeyedAuditableEntity
{
    public decimal QuantityInStock { get; set; } // Current stock level
    public decimal ReservedQuantity { get; set; } // Quantity reserved for tasks/orders
    public decimal ReorderLevel { get; set; } // Minimum stock level for reorder
    public UnitOfMeasuresEnum UnitOfMeasure { get; set; } // Unit of measure for inventory

    public required Guid TintMaterialId { get; set; } // Foreign key to TintMaterial
    public virtual TintMaterial TintMaterial { get; set; } // Navigation property to TintMaterial
}
