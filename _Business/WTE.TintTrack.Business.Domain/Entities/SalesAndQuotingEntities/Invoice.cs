using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;

public class Invoice : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }

    // Invoice details
    public required DateTime IssueDate { get; set; }
    public DateTime? DueDate { get; set; }

    public InvoiceStatusEnum Status { get; set; } = InvoiceStatusEnum.Draft;

    public decimal Subtotal { get; set; } // Total before taxes/discounts
    public decimal TaxAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Total { get; set; } // Final total amount after adjustments

    public string? Notes { get; set; } // e.g., payment instructions, legal disclaimers

    // Tracking
    public bool IsViewed { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? PaidDate { get; set; }


    // Signature / Acknowledgment
    public InvoiceSignatureTypesEnum SignatureType { get; set; } = InvoiceSignatureTypesEnum.None;
    public bool IsSigned { get; set; }
    public DateTime? SignedDate { get; set; }
    public string? SignatureUrl { get; set; }     // Signed doc or signature image
    public byte[]? SignatureContent { get; set; } // Optional, if embedded or from 3rd party
    public string? SignedBy { get; set; } // Email, name, or external user ID
    public string? SignatureProvider { get; set; } // e.g., "DocuSign", "AdobeSign"
    public string? SignatureEnvelopeId { get; set; } // External ID for tracking


    // Payment method tracking
    public PaymentMethodsEnum? PaymentMethod { get; set; }

    // PDF/attachment
    public string? InvoiceFileUrl { get; set; }

    // Associations
    public Guid? ContractId { get; set; }
    public virtual Contract? Contract { get; set; }

    // Billing coverage
    public ICollection<WorkOrder> WorkOrders { get; set; } = new HashSet<WorkOrder>(); // Can link to specific completed/partial work orders
    public ICollection<WorkOrderItem> WorkOrderItems { get; set; } = new HashSet<WorkOrderItem>(); // For itemized billing if needed
    public ICollection<WorkOrderLog> WorkLogs { get; set; } = new HashSet<WorkOrderLog>(); // For hourly/labor billing if T&M

    // Partial adjustments (Invoice Adjustments, Credit Memos)
    public ICollection<InvoiceAdjustment> Adjustments { get; set; } = new HashSet<InvoiceAdjustment>(); // Adjustments to the invoice amount
    public ICollection<CreditMemo> CreditMemos { get; set; } = new HashSet<CreditMemo>(); // Credit memos issued for the invoice

    // Late fees
    public ICollection<LateFee> LateFees { get; set; } = new HashSet<LateFee>(); // Late fee charges

    // Payments made towards the invoice
    public ICollection<InvoicePayment> Payments { get; set; } = new HashSet<InvoicePayment>(); // Payments associated with this invoice
    public ICollection<InvoiceItem> InvoiceItems { get; set; } = new HashSet<InvoiceItem>();
}

