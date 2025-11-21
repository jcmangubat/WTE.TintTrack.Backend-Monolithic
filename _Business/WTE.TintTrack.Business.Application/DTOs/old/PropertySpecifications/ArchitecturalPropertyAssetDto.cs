using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.old.PropertySpecifications;

public class ArchitecturalPropertyAssetDto : PropertyAssetDto
{
    public override sealed required PropertyTypesEnum PropertyType { get; set; } = PropertyTypesEnum.Architectural;

    public string BuildingType { get; set; }  // Type of building (e.g., office, residential, commercial)
    public string WindowSizeInFeet { get; set; }  // Size of the window (e.g., 3ft x 5ft)
    public string? FrameMaterial { get; set; }  // Frame material type (e.g., wood, aluminum)
    public bool HasSecurityFilm { get; set; }  // Does the window have a security film?
}
