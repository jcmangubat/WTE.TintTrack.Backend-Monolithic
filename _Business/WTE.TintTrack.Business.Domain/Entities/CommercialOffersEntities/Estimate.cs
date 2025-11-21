using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;

/// <summary>
/// Use when: The scope or specifics are unclear, and the price is rough or approximate. An estimate gives the customer 
///                 a ballpark figure, but it’s not a commitment to the final cost.
/// Contains: A general idea of pricing, often expressed as a range or approximation, based on preliminary information.
/// Example: Estimating the cost for a project or service where the final scope hasn't been fully defined, like a renovation 
///             or a new feature in a software product.
/// When to use: When you don't have all the details yet but need to give the customer an idea of what the cost might be.
/// </summary>
public class Estimate : GuidKeyedAuditableEntity, ICodedEntity
{
    public decimal EstimatedAmount { get; set; } // The estimated total cost
    public decimal LaborCost { get; set; } // The cost for labor
    public decimal MaterialCost { get; set; } // The cost for materials
    public decimal AdditionalFees { get; set; } // Any additional fees (e.g., delivery, taxes)
    public string? Description { get; set; } // Description of the services/products covered in the estimate
    public string? Notes { get; set; } // Any special notes related to the estimate


    public required string Code { get; set; }
    public DateTime? ExpiryDate { get; set; } // Optional expiration date for the estimate
    public DateTime IssuanceDate { get; set; }
    public string? SourceDocRef { get; set; } // Reference to the source document (e.g., original estimate, proposal, etc.)
    public OfferDocumentStatusEnum OfferDocumentStatus { get; set; } // Status of the estimate (e.g., Pending, Approved, Rejected)
    public string Currency { get; set; } // Currency of the estimate (e.g., USD, EUR)
    


    public Guid? InquiryId { get; set; } // Optional reference to an Inquiry if related
    public Inquiry? Inquiry { get; set; } // Navigation property (optional)

    public required Guid CustomerContactId { get; set; }
    public virtual CustomerContact CustomerContact { get; set; }

    // One-to-one relationship (only one of these will be non-null)
    public Guid? ContractId { get; set; }
    public virtual Contract? Contract { get; set; }

    public ICollection<EstimateItem> EstimateItems { get; set; } = new HashSet<EstimateItem>();
    public ICollection<OfferRecipient> OfferRecipients { get; set; } = new HashSet<OfferRecipient>();
    public ICollection<OfferMilestone> OfferMilestones { get; set; } = new HashSet<OfferMilestone>();
}
