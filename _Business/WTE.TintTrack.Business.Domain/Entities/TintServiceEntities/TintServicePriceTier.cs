using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Domain.Entities.TintServiceEntities;

/// <summary>
/// Ability to define Customer-Specific Pricing
/// </summary>
public class TintServicePriceTier : GuidKeyedAuditableEntity
{
    public int MinQuantity { get; set; } // Minimum quantity for this tier
    public decimal DiscountPercentage { get; set; } // Discount for this tier

    public required Guid TintServicePriceScheduleId { get; set; } // FK to TintServicePriceSchedule
    public virtual TintServicePriceSchedule TintServicePriceSchedule { get; set; }
}
