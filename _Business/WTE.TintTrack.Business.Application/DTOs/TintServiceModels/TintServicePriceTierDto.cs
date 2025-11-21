using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;

namespace WTE.TintTrack.Business.Application.DTOs.TintServiceModels;

/// <summary>
/// Ability to define Customer-Specific Pricing
/// </summary>
public class TintServicePriceTierDto : GuidKeyedAuditableModel
{
    public int MinQuantity { get; set; } // Minimum quantity for this tier
    public decimal DiscountPercentage { get; set; } // Discount for this tier

    public required Guid TintServicePriceScheduleId { get; set; } // FK to TintServicePriceSchedule
    public virtual TintServicePriceScheduleDto TintServicePriceSchedule { get; set; }
}
