using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities.PropertySpecifications;

public class CustomPropertyAsset : PropertyAsset
{
    public override sealed required PropertyTypesEnum PropertyType { get; set; } = PropertyTypesEnum.Custom;
    public string CustomGlassType { get; set; }  // Description of the custom glass (e.g., colored, curved)
    public string CustomizationDetails { get; set; }  // Details about how the glass is customized
}
