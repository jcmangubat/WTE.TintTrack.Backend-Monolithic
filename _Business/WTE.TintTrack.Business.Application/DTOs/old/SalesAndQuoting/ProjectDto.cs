using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.old.SalesAndQuoting;

public class ProjectDto : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectStatusEnum Status { get; set; }

    // One-to-one relationship with Contract
    public Guid? ContractId { get; set; }
    public virtual ContractDto? Contract { get; set; }

    public ICollection<ProjectMilestoneDto> Milestones { get; set; } = new HashSet<ProjectMilestoneDto>();
    public ICollection<WorkOrderDto> WorkOrders { get; set; } = new HashSet<WorkOrderDto>();
}
