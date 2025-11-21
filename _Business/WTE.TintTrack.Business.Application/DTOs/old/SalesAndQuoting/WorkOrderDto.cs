using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Business.Domain.Entities.SalesAndQuoting;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.old.SalesAndQuoting;


public class WorkOrderDto : GuidKeyedAuditableEntity
{
    public required string Title { get; set; }
    public string? Description { get; set; }

    public DateTime ScheduledDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public WorkOrderStatusEnum Status { get; set; }

    // Relationships
    public required Guid ProjectId { get; set; }
    public virtual ProjectDto Project { get; set; }

    public Guid? ProjectMilestoneId { get; set; }
    public virtual ProjectMilestoneDto? ProjectMilestone { get; set; }

    public ICollection<WorkOrderAssignmentDto> Assignments { get; set; } = new HashSet<WorkOrderAssignmentDto>();
    public ICollection<WorkLogDto> WorkLogs { get; set; } = new HashSet<WorkLogDto>();

    public ICollection<WorkOrderItemDto> WorkOrderItems { get; set; } = new HashSet<WorkOrderItemDto>();
}
