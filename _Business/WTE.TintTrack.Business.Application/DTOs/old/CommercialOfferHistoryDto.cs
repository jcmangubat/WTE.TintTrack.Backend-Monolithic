using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.old;

public class CommercialOfferHistoryDto : GuidKeyedAuditableEntity
{
    public required OfferDocumentStatusEnum OfferDocumentStatus { get; set; }

    public string? Comments { get; set; } // Comments or notes related to the status change
    public string ChangedByUserCode { get; set; } = default!;

    public required Guid CommercialOfferRecipientId { get; set; }
    public virtual CommercialOfferRecipientDto CommercialOfferRecipient { get; set; }
}
