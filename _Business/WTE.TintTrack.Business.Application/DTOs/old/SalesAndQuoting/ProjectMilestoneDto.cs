using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Business.Application.DTOs.old.CommercialOffers;

namespace WTE.TintTrack.Business.Application.DTOs.old.SalesAndQuoting;

public class ProjectMilestoneDto : GuidKeyedAuditableModel
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? TargetDate { get; set; }
    public bool IsCompleted { get; set; }

    public required Guid ProposalId { get; set; }
    public virtual ProposalDto Proposal { get; set; }
}