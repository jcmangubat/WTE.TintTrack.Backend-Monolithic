using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Entities.PropertySpecifications;
using WTE.TintTrack.Common.Constants;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Infrastructure.Configurations;

public class PropertyAssetConfiguration(string schema = "dbo")
    : EntityConfiguration<PropertyAsset, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<PropertyAsset> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);
        entityBuilder.DefineDbField(p => p.Name, true, FieldLengths.General.Name);
        entityBuilder.DefineDbField(p => p.Description, false, FieldLengths.General.LENGTH150);
        entityBuilder.DefineDbField(p => p.PropertyType, true);

        entityBuilder.HasOne(p => p.Customer)
                 .WithMany(p => p.PropertyAssets)
                 .HasForeignKey(p => p.CustomerId)
                 .OnDelete(DeleteBehavior.Cascade);

        entityBuilder.UseTphMappingStrategy()
                .HasDiscriminator(p => p.PropertyType)
                .HasValue<ArchitecturalPropertyAsset>(PropertyTypesEnum.Architectural)
                .HasValue<AutomotivePropertyAsset>(PropertyTypesEnum.Automotive)
                .HasValue<ResidentialPropertyAsset>(PropertyTypesEnum.Residential)
                .HasValue<CommercialPropertyAsset>(PropertyTypesEnum.Commercial)
                .HasValue<SpecialtyPropertyAsset>(PropertyTypesEnum.Specialty)
                .HasValue<GlassFilmPropertyAsset>(PropertyTypesEnum.GlassFilm)
                .HasValue<EnergyEfficientPropertyAsset>(PropertyTypesEnum.EnergyEfficient)
                .HasValue<CustomPropertyAsset>(PropertyTypesEnum.Custom)
                .HasValue<SignagePropertyAsset>(PropertyTypesEnum.Signage)
                .HasValue<OutdoorPropertyAsset>(PropertyTypesEnum.Outdoor)
                .HasValue<OtherPropertyAsset>(PropertyTypesEnum.Other);
    }
}
