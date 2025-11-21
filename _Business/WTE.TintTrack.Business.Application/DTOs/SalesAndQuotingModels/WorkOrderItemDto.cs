using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;
using WTE.TintTrack.Business.Domain.Entities.TintServiceEntities;

namespace WTE.TintTrack.Business.Application.DTOs.SalesAndQuotingModels;

public class WorkOrderItemDto : GuidKeyedAuditableModel
{
    public string? Description { get; set; }

    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
    

    public required Guid WorkOrderId { get; set; }
    public virtual WorkOrderDto WorkOrder { get; set; }

    public required Guid? TintServiceId { get; set; }
    public virtual TintService? TintService { get; set; }

    public required Guid? TintMaterialId { get; set; }
    public virtual TintMaterial? TintMaterial { get; set; }

}
