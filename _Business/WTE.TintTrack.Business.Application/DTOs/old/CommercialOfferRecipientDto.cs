using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.old;

public class CommercialOfferRecipientDto : GuidKeyedAuditableEntity
{
    public OfferDocumentRecipientRolesEnum OfferDocumentRecipientRole { get; set; }

    public required Guid CustomerContactId { get; set; }
    public virtual CustomerContactDto CustomerContact { get; set; }

    public  Guid? ProposalId { get; set; }
    public virtual ProposalDto Proposal { get; set; }

    public Guid? QuoteId { get; set; }
    public virtual QuoteDto Quote{ get; set; }

    public Guid? EstimateId { get; set; }
    public virtual EstimateDto Estimate{ get; set; }


    public virtual IEnumerable<CommercialOfferHistoryDto> CommercialOfferHistories { get; set; } = [];
}

