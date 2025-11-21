using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;

/// <summary>
/// Use when: The offering is more complex, customized, or includes multiple options. 
///             A proposal usually outlines the overall solution, benefits, scope, terms, and costs.
/// Contains: Detailed descriptions of the product or service, objectives, deliverables, timelines, 
///             and the pricing model.It may also outline the terms of agreement, project milestones, 
///             and how you plan to meet the customer's needs.
/// Example: A custom software development project or a consulting engagement.
/// When to use: 
///         When the project or offering requires significant customization or when 
///         you're offering a detailed solution to a problem, often in B2B settings.
/// </summary>
public class Proposal : GuidKeyedAuditableEntity, ICodedEntity
{
    public string SolutionDescription { get; set; } // Detailed description of the proposed solution
    public decimal TotalCost { get; set; } // Total cost for the proposed solution
    public string TermsAndConditions { get; set; } // Terms and conditions associated with the proposal
    public string ProjectTimeline { get; set; } // Timeline for the proposed project (start date, milestones, etc.)
    public string Deliverables { get; set; } // What the customer can expect to receive upon completion

    public required string Code { get; set; }
    public DateTime? ExpiryDate { get; set; } // Optional expiration date for this proposal
    public DateTime IssuanceDate { get; set; }
    public string? SourceDocRef { get; set; } // Reference to the source document (e.g., original estimate, proposal, etc.)
    public OfferDocumentStatusEnum OfferDocumentStatus { get; set; } // Status of the estimate (e.g., Pending, Approved, Rejected)
    public string Currency { get; set; } // Currency of the proposal (e.g., USD, EUR)

    public Guid? InquiryId { get; set; } // Optional reference to an Inquiry if related
    public Inquiry? Inquiry { get; set; } // Navigation property (optional)

    // One-to-one relationship (only one of these will be non-null)
    public Guid? ContractId { get; set; }
    public virtual Contract? Contract { get; set; }

    public required Guid CustomerContactId { get; set; }
    public virtual CustomerContact CustomerContact { get; set; }


    public ICollection<ProposalItem> ProposalItems { get; set; } = new HashSet<ProposalItem>();
    public ICollection<OfferRecipient> OfferRecipients { get; set; } = new HashSet<OfferRecipient>();
    public ICollection<OfferMilestone> OfferMilestones { get; set; } = new HashSet<OfferMilestone>();
}
