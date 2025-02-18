using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.PropertySpecifications;

public class OtherPropertyDto : PropertyDto
{
    public override sealed required PropertyTypesEnum PropertyType { get; set; } = PropertyTypesEnum.Other;
    public string OtherDetails { get; set; }  // Any other details that don't fit into the predefined categories
}