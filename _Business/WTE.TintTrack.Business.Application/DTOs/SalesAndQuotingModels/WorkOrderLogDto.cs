using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;

namespace WTE.TintTrack.Business.Application.DTOs.SalesAndQuotingModels;

public class WorkOrderLogDto : GuidKeyedAuditableModel
{
    public required string UserCode { get; set; } // Who did the work
    public DateTime WorkDateTime { get; set; }
    public TimeSpan Duration { get; set; } // e.g. 2.5 hours
    public string Notes { get; set; }

    public required Guid WorkOrderId { get; set; }
    public virtual WorkOrderDto WorkOrder { get; set; }

    public ICollection<WorkOrderLogPhotoDto> WorkOrderLogPhotos { get; set; } = new HashSet<WorkOrderLogPhotoDto>();
}
