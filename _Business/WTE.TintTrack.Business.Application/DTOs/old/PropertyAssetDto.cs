using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using System.Runtime.Serialization;
using WTE.TintTrack.Business.Domain.PropertySpecifications;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.old;

[KnownType(typeof(ArchitecturalPropertyAsset))]
[KnownType(typeof(AutomotivePropertyAsset))]
[KnownType(typeof(ResidentialPropertyAsset))]
[KnownType(typeof(CommercialPropertyAsset))]
[KnownType(typeof(SpecialtyPropertyAsset))]
[KnownType(typeof(GlassFilmPropertyAsset))]
[KnownType(typeof(EnergyEfficientPropertyAsset))]
[KnownType(typeof(CustomPropertyAsset))]
[KnownType(typeof(SignagePropertyAsset))]
[KnownType(typeof(OutdoorPropertyAsset))]
[KnownType(typeof(OtherPropertyAsset))]
public abstract class PropertyAssetDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }

    public required string Name { get; set; }
    public string? Description { get; set; }
    public required virtual PropertyTypesEnum PropertyType { get; set; }

    // Navigation property to Customer
    public string CustomerCode { get; set; }
    public virtual CustomerDto Customer { get; set; }
}
