using System.Text.Json.Serialization;
using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Business.Application.DTOs.PropertySpecifications;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Messaging.Business.Request;

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
public class CreatePropertyRequest : IEntityCreateRequest, ICodedModel
{
    [Required]
    public required string Code { get; set; }

    [Required]
    public required string Name { get; set; }


    public string? Description { get; set; }

    [Required]
    public required PropertyTypesEnum PropertyType { get; set; }

    [Required]
    public required string CustomerCode { get; set; }
}