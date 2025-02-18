using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Domain.Shared;

namespace WTE.TintTrack.Business.Application.DTOs;

public class QuoteDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }

    public required string QuoteNumber { get; set; }
    public required DateTime QuoteDate { get; set; }
    public required decimal TotalAmount { get; set; }
    public required string Description { get; set; }
    public bool? IsAccepted { get; set; }

    // Navigation Property: A Quote is linked to a Customer
    public required Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    // Optional navigation to Proposals (could be null)
    public virtual ICollection<Proposal>? Proposals { get; set; }

    // Navigation Property: One Quote can have multiple Projects
    public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
}
