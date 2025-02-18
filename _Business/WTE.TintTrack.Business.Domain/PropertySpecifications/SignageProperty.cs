using WTE.TintTrack.Business.Domain.Entities;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.PropertySpecifications;

public class SignageProperty : Property
{
    public override sealed required PropertyTypesEnum PropertyType { get; set; } = PropertyTypesEnum.Signage;
    public string SignageType { get; set; }  // Type of signage (e.g., storefront, display, logo)
    public string? BrandingDetails { get; set; }  // Optional: Details related to branding or logo application
    public bool IsBacklit { get; set; }  // Is the signage glass backlit for illumination?
}
