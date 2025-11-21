using System.Text.Json.Serialization;
using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Business.Application.DTOs.PropertySpecificationModels;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Messaging.Business.Requests.PropertyAsset;

[JsonPolymorphic(TypeDiscriminatorPropertyName = nameof(PropertyType))]
[JsonDerivedType(typeof(ArchitecturalPropertyAssetDto), (int)PropertyTypesEnum.Architectural)]
[JsonDerivedType(typeof(AutomotivePropertyAssetDto), (int)PropertyTypesEnum.Automotive)]
[JsonDerivedType(typeof(ResidentialPropertyAssetDto), (int)PropertyTypesEnum.Residential)]
[JsonDerivedType(typeof(CommercialPropertyAssetDto), (int)PropertyTypesEnum.Commercial)]
[JsonDerivedType(typeof(SpecialtyPropertyAssetDto), (int)PropertyTypesEnum.Specialty)]
[JsonDerivedType(typeof(GlassFilmPropertyAssetDto), (int)PropertyTypesEnum.GlassFilm)]
[JsonDerivedType(typeof(EnergyEfficientPropertyAssetDto), (int)PropertyTypesEnum.EnergyEfficient)]
[JsonDerivedType(typeof(CustomPropertyAssetDto), (int)PropertyTypesEnum.Custom)]
[JsonDerivedType(typeof(SignagePropertyAssetDto), (int)PropertyTypesEnum.Signage)]
[JsonDerivedType(typeof(OutdoorPropertyAssetDto), (int)PropertyTypesEnum.Outdoor)]
public class CreatePropertyAssetRequest : IEntityCreateRequest, ICodedModel
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