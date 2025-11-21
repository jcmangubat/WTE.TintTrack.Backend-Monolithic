using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.TintMaterialModels;

public class TintMaterialDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; } // Name of the product (e.g., "Solar Tint Film")
    public required string Description { get; set; } // Detailed description of the product
    public decimal RollLength { get; set; } // Total length of the roll (raw material) in specified units
    public decimal RollWidth { get; set; } // Width of the roll in meters (for area-based calculations)

    public required UnitOfMeasuresEnum UnitOfMeasure { get; set; }

    //public ICollection<EstimateItemDto> EstimateItems { get; set; } = new HashSet<EstimateItemDto>(); // Associated estimates for this service
    //public ICollection<QuoteItemDto> QuoteItems { get; set; } = new HashSet<QuoteItemDto>();  // Associated quotes for this service
    //public ICollection<ProposalItemDto> ProposalItems { get; set; } = new HashSet<ProposalItemDto>();  // Associated proposals for this service

    public ICollection<TintMaterialPriceScheduleDto> TintMaterialPriceSchedules { get; set; } = new HashSet<TintMaterialPriceScheduleDto>();
    public ICollection<InventoryItemDto> InventoryItems { get; set; } = new HashSet<InventoryItemDto>();
    //public ICollection<WorkOrderItemDto> WorkOrderItems { get; set; } = new HashSet<WorkOrderItemDto>();
}