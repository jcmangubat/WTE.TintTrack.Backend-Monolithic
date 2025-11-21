using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;

public class OfferRecipient : GuidKeyedAuditableEntity
{
    public OfferDocumentRecipientRolesEnum OfferDocumentRecipientRole { get; set; }

    public required Guid CustomerContactId { get; set; }
    public virtual CustomerContact CustomerContact { get; set; }

    public  Guid? ProposalId { get; set; }
    public virtual Proposal? Proposal { get; set; }

    public Guid? QuoteId { get; set; }
    public virtual Quote? Quote{ get; set; }

    public Guid? EstimateId { get; set; }
    public virtual Estimate? Estimate{ get; set; }


    public ICollection<OfferHistory> OfferHistories { get; set; } = new HashSet<OfferHistory>();
}

