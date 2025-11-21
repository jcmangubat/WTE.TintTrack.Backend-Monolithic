using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Domain.Shared;

namespace WTE.TintTrack.Business.Application.DTOs.CommercialOffersModels;

public class OfferMilestoneDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public DateTime? ExpectedStartDate { get; set; }
    public DateTime? ExpectedEndDate { get; set; }
    public decimal? EstimatedAmount { get; set; } // Optional: money tied to milestone if billing phased

    // Associations - one and only one should be non-null
    public Guid? ProposalId { get; set; }
    public virtual ProposalDto? Proposal { get; set; }

    public Guid? EstimateId { get; set; }
    public virtual EstimateDto? Estimate { get; set; }
}