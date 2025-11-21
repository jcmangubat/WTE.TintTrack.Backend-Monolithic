using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Domain.Shared;

namespace WTE.TintTrack.Business.Application.DTOs.SalesAndQuotingModels;

public class ProjectMilestoneDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public DateTime? ExpectedStartDate { get; set; }
    public DateTime? ExpectedEndDate { get; set; }
    public decimal? EstimatedAmount { get; set; } // Optional: money tied to milestone if billing phased

    public bool IsCompleted { get; set; }

    public required Guid ProjectId { get; set; }
    public virtual ProjectDto Project { get; set; }

    public ICollection<WorkOrderDto> WorkOrders { get; set; } = new HashSet<WorkOrderDto>();  
}