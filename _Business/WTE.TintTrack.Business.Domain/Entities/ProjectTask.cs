using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities;

public class ProjectTask : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; } // Task name (e.g., "Site Preparation", "Window Tinting")
    public required string Description { get; set; } // Detailed description of the task
    public required TaskStatusEnum Status { get; set; } // Enum to represent task status (e.g., Pending, In Progress, Completed)
    public DateTime? StartDate { get; set; } // Task start date
    public DateTime? EndDate { get; set; } // Task end date
    public decimal? Cost { get; set; } // Cost of the task (if applicable)
    public required PriorityEnums Priority { get; set; }

    // Relationships
    public required Guid ProjectId { get; set; } // Foreign key to Project
    public virtual Project Project { get; set; } // Navigation property to Project

    public virtual ICollection<ProjectTaskMaterial> ProjectActivityMaterials { get; set; } = new HashSet<ProjectTaskMaterial>();
    public virtual ICollection<ProjectTaskAssignee> ProjectActivityAssignees { get; set; } = new HashSet<ProjectTaskAssignee>();
}
