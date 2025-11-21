using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Business.Domain.Entities;

namespace WTE.TintTrack.Business.Application.DTOs.old.SalesAndQuoting;

public class WorkOrderItemDto : GuidKeyedAuditableEntity
{
    public required Guid TintServiceId { get; set; }
    public virtual TintService TintService { get; set; }

    public decimal Rate { get; set; }
    public int Quantity { get; set; }
    public string? Description { get; set; }
    
    
    public required Guid WorkOrderId { get; set; }
    public virtual WorkOrderDto WorkOrder { get; set; }
}
