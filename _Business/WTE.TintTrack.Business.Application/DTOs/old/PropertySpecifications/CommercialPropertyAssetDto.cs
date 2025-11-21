using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.old.PropertySpecifications;

public class CommercialPropertyAssetDto : PropertyAssetDto
{
    public override sealed required PropertyTypesEnum PropertyType { get; set; } = PropertyTypesEnum.Commercial;
    public string BusinessType { get; set; }  // Type of business (e.g., retail, office, restaurant)
    public bool HasSecurityGlass { get; set; }  // Does the property use security-grade glass?
    public bool HasUVProtection { get; set; }  // Does the property have UV protection film on windows?
    public bool HasSoundproofing { get; set; }  // Does the property have soundproof glass or film?
}
