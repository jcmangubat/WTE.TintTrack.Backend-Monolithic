using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;

public class WorkOrder : GuidKeyedAuditableEntity
{
    public required string Title { get; set; }
    public string? Description { get; set; }

    public DateTime ScheduledDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public WorkOrderStatusEnum WorkOrderStatus { get; set; }

    // Relationships
    public required Guid ProjectId { get; set; }
    public virtual Project Project { get; set; }

    /// <summary>
    /// This is the milestone that this work order is associated with if it does make sense.
    /// </summary>
    public Guid? ProjectMilestoneId { get; set; }
    public virtual ProjectMilestone? ProjectMilestone { get; set; }

    public ICollection<WorkOrderAssignment> WorkOrderAssignments { get; set; } = new HashSet<WorkOrderAssignment>();
    public ICollection<WorkOrderLog> WorkOrderLogs { get; set; } = new HashSet<WorkOrderLog>();
    public ICollection<WorkOrderItem> WorkOrderItems { get; set; } = new HashSet<WorkOrderItem>();
}
