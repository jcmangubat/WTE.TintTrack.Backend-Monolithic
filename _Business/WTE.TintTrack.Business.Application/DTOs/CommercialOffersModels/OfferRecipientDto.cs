using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.CommercialOffersModels;

public class OfferRecipientDto : GuidKeyedAuditableModel
{
    public OfferDocumentRecipientRolesEnum OfferDocumentRecipientRole { get; set; }

    public required Guid CustomerContactId { get; set; }
    public virtual CustomerContactDto CustomerContact { get; set; }

    public  Guid? ProposalId { get; set; }
    public virtual ProposalDto? Proposal { get; set; }

    public Guid? QuoteId { get; set; }
    public virtual QuoteDto? Quote{ get; set; }

    public Guid? EstimateId { get; set; }
    public virtual EstimateDto? Estimate{ get; set; }


    public ICollection<OfferHistoryDto> OfferHistories { get; set; } = new HashSet<OfferHistoryDto>();
}

