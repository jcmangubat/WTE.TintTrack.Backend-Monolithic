using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using System.Text.Json.Serialization;
using WTE.TintTrack.Business.Application.DTOs.PropertySpecifications;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs;

[JsonPolymorphic(TypeDiscriminatorPropertyName = nameof(PropertyType))]
[JsonDerivedType(typeof(ArchitecturalPropertyDto), (int)PropertyTypesEnum.Architectural)]
[JsonDerivedType(typeof(AutomotivePropertyDto), (int)PropertyTypesEnum.Automotive)]
[JsonDerivedType(typeof(ResidentialPropertyDto), (int)PropertyTypesEnum.Residential)]
[JsonDerivedType(typeof(CommercialPropertyDto), (int)PropertyTypesEnum.Commercial)]
[JsonDerivedType(typeof(SpecialtyPropertyDto), (int)PropertyTypesEnum.Specialty)]
[JsonDerivedType(typeof(GlassFilmPropertyDto), (int)PropertyTypesEnum.GlassFilm)]
[JsonDerivedType(typeof(EnergyEfficientPropertyDto), (int)PropertyTypesEnum.EnergyEfficient)]
[JsonDerivedType(typeof(CustomPropertyDto), (int)PropertyTypesEnum.Custom)]
[JsonDerivedType(typeof(SignagePropertyDto), (int)PropertyTypesEnum.Signage)]
[JsonDerivedType(typeof(OutdoorPropertyDto), (int)PropertyTypesEnum.Outdoor)]
public class PropertyDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }

    public required string Name { get; set; }
    public string? Description { get; set; }
    public required virtual PropertyTypesEnum PropertyType { get; set; }

    // Navigation property to Customer
    public required string CustomerCode { get; set; }
}
