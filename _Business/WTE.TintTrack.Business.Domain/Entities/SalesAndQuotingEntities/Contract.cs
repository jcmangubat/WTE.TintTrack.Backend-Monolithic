using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;

public class Contract : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }
    // Contract specifics
    public required BillingTypesEnum BillingType { get; set; }
    public decimal? FixedAmount { get; set; }  // If the contract is based on fixed pricing
    public decimal? HourlyRate { get; set; }   // If the contract is Time & Materials

    // Payment terms
    public PaymentTermsEnum PaymentTerm { get; set; }  // e.g., Net30, Net60, etc.
    public bool IsPaidInFull { get; set; }  // Track if contract is paid in full

    // Date range for the contract
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // Additional contract clauses or notes
    public string? Notes { get; set; }

    // Contract status flags
    public bool IsViewed { get; set; }  // Track if the client has viewed the contract
    public bool? IsApproved { get; set; }  // Track if the client has approved the contract or cancelled

    // Signature / Acknowledgment
    public InvoiceSignatureTypesEnum SignatureType { get; set; } = InvoiceSignatureTypesEnum.None;
    public bool IsSigned { get; set; }
    public DateTime? SignedDate { get; set; }
    public string? SignatureUrl { get; set; }     // Signed doc or signature image
    //public byte[]? SignatureContent { get; set; } // Optional, if embedded or from 3rd party
    public string? SignedBy { get; set; } // Email, name, or external user ID
    public string? SignatureProvider { get; set; } // e.g., "DocuSign", "AdobeSign"
    public string? SignatureEnvelopeId { get; set; } // External ID for tracking



    // One-to-one relationship (only one of these will be non-null)
    public Guid? ProposalId { get; set; }
    public virtual Proposal? Proposal { get; set; }

    public Guid? QuoteId { get; set; }
    public virtual Quote? Quote { get; set; }

    public Guid? EstimateId { get; set; }
    public virtual Estimate? Estimate { get; set; }


    // Link to the Project (which has WorkOrders, Milestones, etc.)
    // Can be optional at first until the project is created
    public Guid? ProjectId { get; set; }
    public virtual Project? Project { get; set; }

    public ICollection<ContractMilestone> ContractMilestones { get; set; } = new HashSet<ContractMilestone>();
}
