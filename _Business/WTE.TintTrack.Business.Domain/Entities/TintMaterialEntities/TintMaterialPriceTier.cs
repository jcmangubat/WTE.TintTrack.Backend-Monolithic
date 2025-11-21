using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;

/// <summary>
/// Ability to define Customer-Specific Pricing
/// </summary>
public class TintMaterialPriceTier : GuidKeyedAuditableEntity
{
    public int MinQuantity { get; set; } // Minimum quantity for this tier
    public decimal DiscountPercentage { get; set; } // Discount for this tier

    public required Guid TintMaterialPriceScheduleId { get; set; } // FK to TintMaterialPriceSchedule
    public virtual TintMaterialPriceSchedule TintMaterialPriceSchedule { get; set; }
}
