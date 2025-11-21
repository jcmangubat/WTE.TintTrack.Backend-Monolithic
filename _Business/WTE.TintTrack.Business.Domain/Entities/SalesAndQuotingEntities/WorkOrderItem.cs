using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;
using WTE.TintTrack.Business.Domain.Entities.TintServiceEntities;

namespace WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;

public class WorkOrderItem : GuidKeyedAuditableEntity
{
    public string? Description { get; set; }

    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
    

    public required Guid WorkOrderId { get; set; }
    public virtual WorkOrder WorkOrder { get; set; }

    public required Guid? TintServiceId { get; set; }
    public virtual TintService? TintService { get; set; }

    public required Guid? TintMaterialId { get; set; }
    public virtual TintMaterial? TintMaterial { get; set; }

}
