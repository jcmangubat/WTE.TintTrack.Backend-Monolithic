using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;

public class WorkOrderAssignment : GuidKeyedAuditableEntity
{
    public required string UserCode { get; set; } // Staff or Technician
    public string Role { get; set; } = "Technician";

    public required Guid WorkOrderId { get; set; }
    public virtual WorkOrder WorkOrder { get; set; }
}
