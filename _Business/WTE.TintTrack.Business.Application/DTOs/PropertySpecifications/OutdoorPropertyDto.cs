using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.PropertySpecifications;

public class OutdoorPropertyDto : PropertyDto
{
    public override sealed required PropertyTypesEnum PropertyType { get; set; } = PropertyTypesEnum.Outdoor;
    public string OutdoorType { get; set; }  // Type of outdoor use (e.g., pool fence, railing)
    public bool IsWeatherResistant { get; set; }  // Is the glass/weather-resistant?
    public bool HasSafetyFeatures { get; set; }  // Does it have safety features like shatter resistance?
}
