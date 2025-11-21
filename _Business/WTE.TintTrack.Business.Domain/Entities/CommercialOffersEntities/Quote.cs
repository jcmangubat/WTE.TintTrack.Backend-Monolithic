using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;

/// <summary>
/// Use when: The customer has provided enough detail, and you are ready to give an exact price for the 
///             product or service based on their specific requirements.
/// Contains: A specific price for goods or services, often with a breakdown of costs(materials, labor, etc.), 
///             valid for a certain period.
/// Example: A quote for a specific product or service that has a fixed cost, such as installing 
///             equipment or purchasing a standard product.
/// When to use: When you can provide an accurate and firm price based on the customer’s defined requirements.
///                 A quote is often used for more straightforward, less customized offerings.
/// </summary>

public class Quote : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }
    public DateTime? ExpiryDate { get; set; } // Optional expiration date for this proposal
    public DateTime IssuanceDate { get; set; }
    public string? SourceDocRef { get; set; } // Reference to the source document (e.g., original estimate, proposal, etc.)
    public OfferDocumentStatusEnum OfferDocumentStatus { get; set; } // Status of the estimate (e.g., Pending, Approved, Rejected)
    public string Currency { get; set; } // Currency of the proposal (e.g., USD, EUR)


    public decimal TotalAmount { get; set; } // The total amount for the quote
    public string? Notes { get; set; } // Any additional notes or conditions related to the quote (e.g., special discounts)


    public Guid? InquiryId { get; set; } // Optional reference to an Inquiry if related
    public Inquiry? Inquiry { get; set; } // Navigation property (optional)

    // One-to-one relationship (only one of these will be non-null)
    public Guid? ContractId { get; set; }
    public virtual Contract? Contract { get; set; }

    public required Guid CustomerContactId { get; set; }
    public virtual CustomerContact CustomerContact { get; set; }


    public ICollection<QuoteItem> QuoteItems { get; set; } = new HashSet<QuoteItem>();
    public ICollection<OfferRecipient> OfferRecipients { get; set; } = new HashSet<OfferRecipient>();
    public ICollection<OfferMilestone> OfferMilestones { get; set; } = new HashSet<OfferMilestone>();
}
