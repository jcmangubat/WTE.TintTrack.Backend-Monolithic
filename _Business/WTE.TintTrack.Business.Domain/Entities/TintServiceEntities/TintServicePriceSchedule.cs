using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities.TintServiceEntities;

public class TintServicePriceSchedule : GuidKeyedAuditableEntity
{
    public decimal UnitCost { get; set; } // Base cost per unit
    public decimal MarkupPercentage { get; set; } // Markup percentage

    public decimal FinalPrice => UnitCost * (1 + MarkupPercentage / 100); // Computed dynamically

    public DateTime EffectiveFrom { get; set; } // Activation date
    public DateTime? EffectiveTo { get; set; } // Optional expiration date
    public bool IsCurrent { get; set; } // Indicates if this is the active price

    public PriceCalculationTypesEnum CalculationType { get; set; } = PriceCalculationTypesEnum.Standard;
    public string? CustomFormula { get; set; } // Optional custom formula (e.g., "UnitCost * 1.2")

    public required Guid TintServiceId { get; set; }
    public virtual TintService TintService { get; set; }

    public ICollection<TintServicePriceTier> TintServicePriceTiers { get; set; } = new HashSet<TintServicePriceTier>();
    public ICollection<TintServicePriceOverride> TintServicePriceOverrides { get; set; } = new HashSet<TintServicePriceOverride>();
}
