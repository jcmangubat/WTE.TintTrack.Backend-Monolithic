using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;

namespace WTE.TintTrack.Business.Application.DTOs.SalesAndQuotingModels;

public class WorkOrderAssignmentDto : GuidKeyedAuditableModel
{
    public required string UserCode { get; set; } // Staff or Technician
    public string Role { get; set; } = "Technician";

    public required Guid WorkOrderId { get; set; }
    public virtual WorkOrderDto WorkOrder { get; set; }
}
