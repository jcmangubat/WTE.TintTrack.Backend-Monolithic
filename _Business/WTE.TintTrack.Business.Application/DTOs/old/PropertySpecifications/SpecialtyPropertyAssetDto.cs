using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.old.PropertySpecifications;

public class SpecialtyPropertyAssetDto : PropertyAssetDto
{
    public override sealed required PropertyTypesEnum PropertyType { get; set; } = PropertyTypesEnum.Specialty;
    public string SpecialtyType { get; set; }  // Type of specialty glass (e.g., fire-resistant, smart glass)
    public string ApplicationDetails { get; set; }  // Specific use case or application for the specialty glass
    public bool IsFireResistant { get; set; }  // Is the glass fire-resistant?
    public bool IsSmartGlass { get; set; }  // Is the glass "smart" (e.g., adjustably tinted)?
}
