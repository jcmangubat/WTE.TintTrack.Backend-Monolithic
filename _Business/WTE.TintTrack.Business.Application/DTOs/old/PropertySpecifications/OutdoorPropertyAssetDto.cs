using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.old.PropertySpecifications;

public class OutdoorPropertyAssetDto : PropertyAssetDto
{
    public override sealed required PropertyTypesEnum PropertyType { get; set; } = PropertyTypesEnum.Outdoor;
    public string OutdoorType { get; set; }  // Type of outdoor use (e.g., pool fence, railing)
    public bool IsWeatherResistant { get; set; }  // Is the glass/weather-resistant?
    public bool HasSafetyFeatures { get; set; }  // Does it have safety features like shatter resistance?
}
