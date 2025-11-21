using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Domain.Entities.TintServiceEntities;

public class TintServicePriceOverride : GuidKeyedAuditableEntity
{
    public decimal CustomPrice { get; set; } // Override FinalPrice for this customer

    public required Guid TintTintServicePriceScheduleId { get; set; }
    public virtual TintServicePriceSchedule TintTintServicePriceSchedule { get; set; }


    public required Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}
