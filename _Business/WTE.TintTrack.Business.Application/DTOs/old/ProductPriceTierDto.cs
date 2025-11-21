using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;

namespace WTE.TintTrack.Business.Application.DTOs.old;

/// <summary>
/// Ability to define Customer-Specific Pricing
/// </summary>
public class ProductPriceTierDto : GuidKeyedAuditableModel
{
    public int MinQuantity { get; set; } // Minimum quantity for this tier
    public decimal DiscountPercentage { get; set; } // Discount for this tier

    public required Guid ProductPriceScheduleId { get; set; } // FK to ProductPriceSchedule
    public virtual ProductPriceScheduleDto ProductPriceSchedule { get; set; }
}
