using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;

namespace WTE.TintTrack.Business.Application.DTOs.TintMaterialModels;

/// <summary>
/// Ability to define Customer-Specific Pricing
/// </summary>
public class TintMaterialPriceTierDto : GuidKeyedAuditableModel
{
    public int MinQuantity { get; set; } // Minimum quantity for this tier
    public decimal DiscountPercentage { get; set; } // Discount for this tier

    public required Guid TintMaterialPriceScheduleId { get; set; } // FK to TintMaterialPriceSchedule
    public virtual TintMaterialPriceScheduleDto TintMaterialPriceSchedule { get; set; }
}
