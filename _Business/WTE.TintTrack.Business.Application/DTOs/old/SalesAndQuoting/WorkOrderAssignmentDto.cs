using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Application.DTOs.old.SalesAndQuoting;

public class WorkOrderAssignmentDto : GuidKeyedAuditableEntity
{
    public required string UserCode { get; set; } // Staff or Technician
    public string Role { get; set; } = "Technician";

    public required Guid WorkOrderId { get; set; }
    public virtual WorkOrderDto WorkOrder { get; set; }
}
