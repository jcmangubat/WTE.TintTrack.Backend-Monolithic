using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;
using WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities.TintServiceEntities;

public class TintService : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; } // Name of the service (e.g., "Ceramic Window Tinting")
    public required string Description { get; set; } // Description of the service
    public decimal Price { get; set; } // Price for the service
    public TintServiceTypesEnum ServiceType { get; set; } // Type of service (e.g., "Automotive", "Residential", "Commercial")
    public int EstimatedDurationMinutes { get; set; } // Estimated time in minutes for completion
    public string? AdditionalFeatures { get; set; } // Any additional features of the service (e.g., "Lifetime warranty", "UV blocking", "Heat rejection")

    public ICollection<EstimateItem> EstimateItems { get; set; } = new HashSet<EstimateItem>(); // Associated estimates for this service
    public ICollection<QuoteItem> QuoteItems { get; set; } = new HashSet<QuoteItem>();  // Associated quotes for this service
    public ICollection<ProposalItem> ProposalItems { get; set; } = new HashSet<ProposalItem>();  // Associated proposals for this service


    public ICollection<WorkOrderItem> WorkOrderItems { get; set; } = new HashSet<WorkOrderItem>();

    public ICollection<TintServicePriceSchedule> TintServicePriceSchedules { get; set; } = new HashSet<TintServicePriceSchedule>();
}
