using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.PropertySpecificationModels;

public class SignagePropertyAssetDto : PropertyAssetDto
{
    public override sealed required PropertyTypesEnum PropertyType { get; set; } = PropertyTypesEnum.Signage;
    public string? SignageType { get; set; }  // Type of signage (e.g., storefront, display, logo)
    public string? BrandingDetails { get; set; }  // Optional: Details related to branding or logo application
    public bool IsBacklit { get; set; }  // Is the signage glass backlit for illumination?
}
