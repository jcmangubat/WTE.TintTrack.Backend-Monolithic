using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.SalesAndQuotingModels;

public class ProjectDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectStatusEnum Status { get; set; }

    // One-to-one relationship with Contract
    public Guid ContractId { get; set; }
    public virtual ContractDto Contract { get; set; }

    public ICollection<ProjectMilestoneDto> ProjectMilestones { get; set; } = new HashSet<ProjectMilestoneDto>();
    public ICollection<WorkOrderDto> WorkOrders { get; set; } = new HashSet<WorkOrderDto>();
}
