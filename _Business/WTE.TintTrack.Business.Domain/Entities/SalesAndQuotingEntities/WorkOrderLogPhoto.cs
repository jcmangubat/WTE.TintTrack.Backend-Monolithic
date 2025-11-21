using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;

public class WorkOrderLogPhoto : GuidKeyedAuditableEntity
{
    public required string FileCode { get; set; }
    public required string FileUrl { get; set; }

    public required Guid WorkOrderLogId { get; set; }
    public virtual WorkOrderLog WorkOrderLog { get; set; }
}
