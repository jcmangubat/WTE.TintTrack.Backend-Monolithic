using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;
using WTE.TintTrack.Business.Domain.Entities.TintServiceEntities;

namespace WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;

public class ProposalItem : GuidKeyedAuditableEntity
{
    public required string Description { get; set; } // Description or details of the item/service
    public required int Quantity { get; set; } // Quantity of this service/item (useful if services are offered in multiples)
    public decimal Amount { get; set; } // The cost of this particular item or service

    public required Guid? TintServiceId { get; set; }
    public virtual TintService? TintService { get; set; }

    public required Guid? TintMaterialId { get; set; }
    public virtual TintMaterial? TintMaterial { get; set; }

    public required Guid ProposalId { get; set; }
    public virtual Proposal Proposal { get; set; }
}
