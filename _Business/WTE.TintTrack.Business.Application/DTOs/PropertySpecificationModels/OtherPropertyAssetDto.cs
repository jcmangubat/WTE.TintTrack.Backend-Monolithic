using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.PropertySpecificationModels;

public class OtherPropertyAssetDto : PropertyAssetDto
{
    public override sealed required PropertyTypesEnum PropertyType { get; set; } = PropertyTypesEnum.Other;
    public string OtherDetails { get; set; }  // Any other details that don't fit into the predefined categories
}