using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Domain.Shared;

namespace WTE.TintTrack.Business.Application.DTOs;

public class ProjectDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }

    public required string ProjectName { get; set; }
    public required string Description { get; set; }
    public required DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public required decimal EstimatedCost { get; set; }
    public required decimal ActualCost { get; set; }

    // Foreign Key: A Project is linked to a specific Quote
    public required Guid QuoteId { get; set; } // Foreign Key to Quote
    public virtual QuoteDto Quote { get; set; }

    // Optional Foreign Key: A Project may be linked to a specific Proposal
    public Guid? ProposalId { get; set; }
    public virtual Proposal? Proposal { get; set; }

    // Navigation Property: One Project can have multiple associated Invoices
    public virtual ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
}
