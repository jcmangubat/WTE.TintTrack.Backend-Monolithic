using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;

public class WorkOrderLog : GuidKeyedAuditableEntity
{
    public required string UserCode { get; set; } // Who did the work
    public DateTime WorkDateTime { get; set; }
    public TimeSpan Duration { get; set; } // e.g. 2.5 hours
    public string Notes { get; set; }

    public required Guid WorkOrderId { get; set; }
    public virtual WorkOrder WorkOrder { get; set; }

    public ICollection<WorkOrderLogPhoto> WorkOrderLogPhotos { get; set; } = new HashSet<WorkOrderLogPhoto>();
}
