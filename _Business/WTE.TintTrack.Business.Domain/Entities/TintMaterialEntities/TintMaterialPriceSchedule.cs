using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;

public class TintMaterialPriceSchedule : GuidKeyedAuditableEntity
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
    public virtual TintMaterial TintMaterial { get; set; }

    public ICollection<TintMaterialPriceTier> TintMaterialPriceTiers { get; set; } = new HashSet<TintMaterialPriceTier>();
    public ICollection<TintMaterialPriceOverride> TintMaterialPriceOverrides { get; set; } = new HashSet<TintMaterialPriceOverride>();
}
