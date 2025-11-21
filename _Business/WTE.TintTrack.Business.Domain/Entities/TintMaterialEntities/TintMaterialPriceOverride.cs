using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;

public class TintMaterialPriceOverride : GuidKeyedAuditableEntity
{
    public decimal CustomPrice { get; set; } // Override FinalPrice for this customer

    public required Guid TintMaterialPriceScheduleId { get; set; }
    public virtual TintMaterialPriceSchedule TintMaterialPriceSchedule { get; set; }


    public required Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}
