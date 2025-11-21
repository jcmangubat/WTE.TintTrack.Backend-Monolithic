using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Business.Application.DTOs.old.SalesAndQuoting;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.old.CommercialOffers;

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

public class QuoteDto : GuidKeyedAuditableModel, ICodedEntity
{
    
    public decimal TotalAmount { get; set; } // The total amount for the quote
    public string? Notes { get; set; } // Any additional notes or conditions related to the quote (e.g., special discounts)
    

    public required string Code { get; set; }
    public DateTime? ExpiryDate { get; set; } // Optional expiration date for this proposal
    public DateTime IssuanceDate { get; set; }
    public string? SourceDocRef { get; set; } // Reference to the source document (e.g., original estimate, proposal, etc.)
    public OfferDocumentStatusEnum OfferDocumentStatus { get; set; } // Status of the estimate (e.g., Pending, Approved, Rejected)
    public string Currency { get; set; } // Currency of the proposal (e.g., USD, EUR)



    public Guid? InquiryId { get; set; } // Optional reference to an Inquiry if related
    public Inquiry? Inquiry { get; set; } // Navigation property (optional)

    public required Guid CustomerContactId { get; set; }
    public virtual CustomerContact CustomerContact { get; set; }

    // One-to-one relationship (only one of these will be non-null)
    public Guid ContractId { get; set; }
    public virtual ContractDto? Contract { get; set; }

    public virtual ICollection<QuoteItemDto> QuoteItems { get; set; } // Collection of items or services included in the quote (e.g., additional tinting services, extra fees)
    public virtual ICollection<CommercialOfferRecipientDto> CommercialOfferRecipients { get; set; } = new HashSet<CommercialOfferRecipientDto>();
}
