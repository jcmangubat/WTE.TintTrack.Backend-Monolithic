using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;

public class OfferHistory : GuidKeyedAuditableEntity
{
    public required OfferDocumentStatusEnum OfferDocumentStatus { get; set; }

    public string? Comments { get; set; } // Comments or notes related to the status change
    public string ChangedByUserCode { get; set; } = default!;

    public required Guid OfferRecipientId { get; set; }
    public virtual OfferRecipient OfferRecipient { get; set; }
}
