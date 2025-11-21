using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities;

// Join entity for the many-to-many relationship between Customer and Contact
public class CustomerContact : GuidKeyedAuditableEntity
{
    public required Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public required Guid ContactId { get; set; }
    public virtual Contact Contact { get; set; }

    public required CustomerContactRelationshipTypesEnum RelationshipType { get; set; } // Relationship type: Primary, Billing, etc.

    public ICollection<Inquiry> Inquiries { get; set; } = new HashSet<Inquiry>();
    //public ICollection<OfferRecipient> CommercialOfferRecipients { get; set; } = new HashSet<OfferRecipient>();
    //public ICollection<Proposal> Proposals { get; set; } = new HashSet<Proposal>();
    //public ICollection<Quote> Quotes { get; set; } = new HashSet<Quote>();
    //public ICollection<Estimate> Estimates { get; set; } = new HashSet<Estimate>();
}

