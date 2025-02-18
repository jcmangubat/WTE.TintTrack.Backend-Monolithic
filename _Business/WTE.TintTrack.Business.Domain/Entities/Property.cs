using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using System.Runtime.Serialization;
using WTE.TintTrack.Business.Domain.PropertySpecifications;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities;

[KnownType(typeof(ArchitecturalProperty))]
[KnownType(typeof(AutomotiveProperty))]
[KnownType(typeof(ResidentialProperty))]
[KnownType(typeof(CommercialProperty))]
[KnownType(typeof(SpecialtyProperty))]
[KnownType(typeof(GlassFilmProperty))]
[KnownType(typeof(EnergyEfficientProperty))]
[KnownType(typeof(CustomProperty))]
[KnownType(typeof(SignageProperty))]
[KnownType(typeof(OutdoorProperty))]
[KnownType(typeof(OtherProperty))]
public abstract class Property : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }

    public required string Name { get; set; }
    public string? Description { get; set; }
    public required virtual PropertyTypesEnum PropertyType { get; set; }

    // Navigation property to Customer
    public required Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}
