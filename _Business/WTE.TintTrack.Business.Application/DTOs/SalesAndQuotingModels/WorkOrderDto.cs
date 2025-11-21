using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.SalesAndQuotingModels;

public class WorkOrderDto : GuidKeyedAuditableModel
{
    public required string Title { get; set; }
    public string? Description { get; set; }

    public DateTime ScheduledDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public WorkOrderStatusEnum WorkOrderStatus { get; set; }

    // Relationships
    public required Guid ProjectId { get; set; }
    public virtual ProjectDto Project { get; set; }

    /// <summary>
    /// This is the milestone that this work order is associated with if it does make sense.
    /// </summary>
    public Guid? ProjectMilestoneId { get; set; }
    public virtual ProjectMilestoneDto? ProjectMilestone { get; set; }

    public ICollection<WorkOrderAssignmentDto> WorkOrderAssignments { get; set; } = new HashSet<WorkOrderAssignmentDto>();
    public ICollection<WorkOrderLogDto> WorkOrderLogs { get; set; } = new HashSet<WorkOrderLogDto>();
    public ICollection<WorkOrderItemDto> WorkOrderItems { get; set; } = new HashSet<WorkOrderItemDto>();
}
