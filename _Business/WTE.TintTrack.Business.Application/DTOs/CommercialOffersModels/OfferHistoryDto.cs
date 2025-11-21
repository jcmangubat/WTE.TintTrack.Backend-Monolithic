using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.CommercialOffersModels;

public class OfferHistoryDto : GuidKeyedAuditableModel
{
    public required OfferDocumentStatusEnum OfferDocumentStatus { get; set; }

    public string? Comments { get; set; } // Comments or notes related to the status change
    public string ChangedByUserCode { get; set; } = default!;

    public required Guid CommercialOfferRecipientId { get; set; }
    public virtual OfferRecipientDto OfferRecipient { get; set; }
}
