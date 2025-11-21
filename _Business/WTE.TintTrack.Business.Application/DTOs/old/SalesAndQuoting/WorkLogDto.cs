using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Application.DTOs.old.SalesAndQuoting;

public class WorkLogDto : GuidKeyedAuditableEntity
{
    public required Guid UserId { get; set; } // Who did the work
    public DateTime WorkDate { get; set; }
    public TimeSpan Duration { get; set; } // e.g. 2.5 hours
    public string Notes { get; set; }

    public required Guid WorkOrderId { get; set; }
    public virtual WorkOrderDto WorkOrder { get; set; }

    public ICollection<WorkLogPhotoDto> Photos { get; set; } = new HashSet<WorkLogPhotoDto>();
}
