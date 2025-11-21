using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;

public class Project : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectStatusEnum Status { get; set; }

    // One-to-one relationship with Contract
    public Guid ContractId { get; set; }
    public virtual Contract Contract { get; set; }

    public ICollection<ProjectMilestone> ProjectMilestones { get; set; } = new HashSet<ProjectMilestone>();
    public ICollection<WorkOrder> WorkOrders { get; set; } = new HashSet<WorkOrder>();
}
