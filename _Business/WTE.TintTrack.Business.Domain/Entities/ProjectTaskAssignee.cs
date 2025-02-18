using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities;

public class ProjectTaskAssignee : GuidKeyedAuditableEntity
{
    public required Guid ProjectActivityId { get; set; }
    public virtual ProjectTask ProjectActivity { get; set; }

    public required string UserCode { get; set; }

    public required TaskAssigneeRolesEnum TaskAssigneeRole { get; set; }
}