using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Domain.Shared;

namespace WTE.TintTrack.Business.Application.DTOs;

public class ProposalDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }

    public required string ProposalNumber { get; set; }
    public required string Terms { get; set; }
    public required string ScopeOfWork { get; set; }
    public required decimal TotalAmount { get; set; }

    // Navigation property to Quote: A Proposal is associated with one Quote
    public required Guid QuoteId { get; set; }
    public virtual QuoteDto Quote { get; set; }

    // Navigation Property: One Proposal can be associated with one or more Projects (optional)
    public virtual IEnumerable<ProjectDto> Projects { get; set; } = [];
}
