using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Messaging.Business.Requests.TintMaterial;

public class CreateTintMaterialPriceTierRequest : IEntityCreateRequest, ICodedModel
{
    /*[Required]
    [MaxLength(FieldLengths.Customer.Code)]*/
    public required string Code { get; set; }
    public required string Name { get; set; } // Name of the product (e.g., "Solar Tint Film")
    public required string Description { get; set; } // Detailed description of the product
    public decimal RollLength { get; set; } // Total length of the roll (raw material) in specified units
    public decimal RollWidth { get; set; } // Width of the roll in meters (for area-based calculations)

    public required UnitOfMeasuresEnum UnitOfMeasure { get; set; }
}