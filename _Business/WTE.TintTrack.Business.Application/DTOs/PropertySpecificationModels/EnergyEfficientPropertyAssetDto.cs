using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.PropertySpecificationModels;

public class EnergyEfficientPropertyAssetDto : PropertyAssetDto
{
    public override sealed required PropertyTypesEnum PropertyType { get; set; } = PropertyTypesEnum.EnergyEfficient;
    public string GlassType { get; set; }  // Type of energy-efficient glass (e.g., Low-E, IGU)
    public string? CoatingType { get; set; }  // Type of coating (e.g., reflective, low-emissivity)
    public bool HasInsulatedGlass { get; set; }  // Does the property use insulated glass units (IGUs)?
}
