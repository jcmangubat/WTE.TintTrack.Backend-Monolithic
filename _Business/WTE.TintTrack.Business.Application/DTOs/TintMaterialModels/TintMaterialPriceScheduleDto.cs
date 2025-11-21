using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.TintMaterialModels;

public class TintMaterialPriceScheduleDto : GuidKeyedAuditableModel
{
    public decimal UnitCost { get; set; } // Base cost per unit
    public decimal MarkupPercentage { get; set; } // Markup percentage

    public decimal FinalPrice => UnitCost * (1 + MarkupPercentage / 100); // Computed dynamically

    public DateTime EffectiveFrom { get; set; } // Activation date
    public DateTime? EffectiveTo { get; set; } // Optional expiration date
    public bool IsCurrent { get; set; } // Indicates if this is the active price

    public PriceCalculationTypesEnum CalculationType { get; set; } = PriceCalculationTypesEnum.Standard;
    public string? CustomFormula { get; set; } // Optional custom formula (e.g., "UnitCost * 1.2")

    public required Guid TintMaterialId { get; set; }
    public virtual TintMaterialDto TintMaterial { get; set; }

    public ICollection<TintMaterialPriceTierDto> TintMaterialPriceTiers { get; set; } = new HashSet<TintMaterialPriceTierDto>();
    public ICollection<TintMaterialPriceOverrideDto> TintMaterialPriceOverrides { get; set; } = new HashSet<TintMaterialPriceOverrideDto>();
}
