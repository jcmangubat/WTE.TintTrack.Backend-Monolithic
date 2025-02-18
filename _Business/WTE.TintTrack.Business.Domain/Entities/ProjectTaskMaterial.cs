using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Domain.Entities;

public class ProjectTaskMaterial : GuidKeyedAuditableEntity
{
    public required Guid ProjectActivityId { get; set; }
    public virtual ProjectTask ProjectActivity { get; set; }

    public required Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
}