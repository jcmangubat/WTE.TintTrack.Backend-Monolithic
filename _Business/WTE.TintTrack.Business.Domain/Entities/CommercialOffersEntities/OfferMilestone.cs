using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Domain.Shared;

namespace WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;

public class OfferMilestone : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public DateTime? ExpectedStartDate { get; set; }
    public DateTime? ExpectedEndDate { get; set; }
    public decimal? EstimatedAmount { get; set; } // Optional: money tied to milestone if billing phased

    // Associations - one and only one should be non-null
    public Guid? ProposalId { get; set; }
    public virtual Proposal? Proposal { get; set; }

    public Guid? EstimateId { get; set; }
    public virtual Estimate? Estimate { get; set; }

    public Guid? QuoteId { get; set; }
    public virtual Quote? Quote { get; set; }
}