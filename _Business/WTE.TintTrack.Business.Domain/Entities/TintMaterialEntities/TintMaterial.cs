using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;
using WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;

public class TintMaterial : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; } // Name of the product (e.g., "Solar Tint Film")
    public required string Description { get; set; } // Detailed description of the product
    public decimal RollLength { get; set; } // Total length of the roll (raw material) in specified units
    public decimal RollWidth { get; set; } // Width of the roll in meters (for area-based calculations)

    public required UnitOfMeasuresEnum UnitOfMeasure { get; set; }

    public ICollection<EstimateItem> EstimateItems { get; set; } = new HashSet<EstimateItem>(); // Associated estimates for this service
    public ICollection<QuoteItem> QuoteItems { get; set; } = new HashSet<QuoteItem>();  // Associated quotes for this service
    public ICollection<ProposalItem> ProposalItems { get; set; } = new HashSet<ProposalItem>();  // Associated proposals for this service


    public ICollection<TintMaterialPriceSchedule> TintMaterialPriceSchedules { get; set; } = new HashSet<TintMaterialPriceSchedule>();
    public ICollection<InventoryItem> InventoryItems { get; set; } = new HashSet<InventoryItem>();
    public ICollection<WorkOrderItem> WorkOrderItems { get; set; } = new HashSet<WorkOrderItem>();
}