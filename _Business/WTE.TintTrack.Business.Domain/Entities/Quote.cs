using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Domain.Shared;

namespace WTE.TintTrack.Business.Domain.Entities;

public class Quote : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }

    public required string QuoteNumber { get; set; }
    public required DateTime QuoteDate { get; set; }
    public required decimal TotalAmount { get; set; }
    public required string Description { get; set; }
    public bool? IsAccepted { get; set; }

    public required Guid ProposalId { get; set; }
    public virtual Proposal Proposal { get; set; }

    // Navigation Property: One Quote can have multiple Projects
    public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
}