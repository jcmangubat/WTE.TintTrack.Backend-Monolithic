using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Shared;

public abstract class OfferDocument : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }

    public required OfferDocumentTypesEnum OfferDocumentType { get; set; } 

    public string Notes { get; set; }

    public decimal EstimatedAmount { get; set; }

    public DateTime DateIssued { get; set; }
    public DateTime ExpiryDate { get; set; }

    // Creator
    public required string CreatorUserCode { get; set; }

    public virtual ICollection<CommercialOfferRecipient> CommercialOfferRecipients { get; set; } = new HashSet<CommercialOfferRecipient>();
    
    // Linked Inquiry (if any)
    public Guid? InquiryId { get; set; }
    public virtual Inquiry? Inquiry { get; set; }

}
