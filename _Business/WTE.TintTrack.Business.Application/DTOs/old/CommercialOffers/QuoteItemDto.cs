using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Business.Domain.Entities;

namespace WTE.TintTrack.Business.Application.DTOs.old.CommercialOffers;

public class QuoteItemDto: GuidKeyedAuditableModel
{
    public required string Description { get; set; } // Description or details of the item/service
    public required int Quantity { get; set; } // Quantity of this service/item (useful if services are offered in multiples)
    public decimal Amount { get; set; } // The cost of this particular item or service

    public required Guid TintServiceId { get; set; }
    public virtual TintService TintService { get; set; }

    public required Guid QuoteId { get; set; } // Reference to the parent quote
    public virtual QuoteDto Quote { get; set; } // Navigation property to the Quote entity
}