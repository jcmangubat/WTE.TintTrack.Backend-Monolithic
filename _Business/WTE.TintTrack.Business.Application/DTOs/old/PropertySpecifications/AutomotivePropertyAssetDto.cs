using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.old.PropertySpecifications;

public class AutomotivePropertyAssetDto : PropertyAssetDto
{
    public override sealed required PropertyTypesEnum PropertyType { get; set; } = PropertyTypesEnum.Automotive;
    public required int Year { get; set; }
    public required string Make { get; set; }
    public required string Model { get; set; }
    public required string Color { get; set; }
    public string? LicensePlate { get; set; }
    public string? Trim { get; set; }
    public string? VIN { get; set; }
    public double? Mileage { get; set; }
    public TintTypesEnum? TintType { get; set; }  // Type of tint (e.g., ceramic, carbon, etc.)
    public bool? HasDefrostLines { get; set; }  // Does the vehicle have defrost lines on the windshield?
}
