using WTE.TintTrack.Business.Domain.Entities;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.PropertySpecifications;

public class ResidentialProperty : Property
{
    public override sealed required PropertyTypesEnum PropertyType { get; set; } = PropertyTypesEnum.Residential;
    public string HomeType { get; set; }  // Type of residential property (e.g., house, apartment, townhouse)
    public int NumberOfFloors { get; set; }  // Number of floors in the property
    public bool HasEnergyEfficientWindows { get; set; }  // Does the property have energy-efficient windows?
    public bool HasPrivacyTint { get; set; }  // Does the property use privacy tinting on windows?
}
