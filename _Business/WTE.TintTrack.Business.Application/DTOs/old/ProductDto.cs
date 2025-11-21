using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.old;

public class ProductDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; } // Name of the product (e.g., "Solar Tint Film")
    public required string Description { get; set; } // Detailed description of the product
    public decimal RollLength { get; set; } // Total length of the roll (raw material) in specified units
    public decimal RollWidth { get; set; } // Width of the roll in meters (for area-based calculations)

    public required UnitOfMeasuresEnum UnitOfMeasure { get; set; }

    public virtual IEnumerable<ProductPriceScheduleDto> PriceSchedules { get; set; } = [];
    public virtual IEnumerable<InventoryItemDto> InventoryItems { get; set; } = [];
}