using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.PropertySpecifications;
using WTE.TintTrack.Common.Constants;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Infrastructure.Configurations;

public class PropertyConfiguration(string schema = "dbo")
    : EntityConfiguration<Property, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Property> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);
        entityBuilder.DefineDbField(p => p.Name, true, FieldLengths.General.Name);
        entityBuilder.DefineDbField(p => p.Description, false, FieldLengths.General.LENGTH150);
        entityBuilder.DefineDbField(p => p.PropertyType, true);

        entityBuilder.HasOne(p => p.Customer)
                 .WithMany(p => p.CustomerProperties)
                 .HasForeignKey(p => p.CustomerId)
                 .OnDelete(DeleteBehavior.Cascade);

        entityBuilder.UseTphMappingStrategy()
                .HasDiscriminator(p => p.PropertyType)
                .HasValue<ArchitecturalProperty>(PropertyTypesEnum.Architectural)
                .HasValue<AutomotiveProperty>(PropertyTypesEnum.Automotive)
                .HasValue<ResidentialProperty>(PropertyTypesEnum.Residential)
                .HasValue<CommercialProperty>(PropertyTypesEnum.Commercial)
                .HasValue<SpecialtyProperty>(PropertyTypesEnum.Specialty)
                .HasValue<GlassFilmProperty>(PropertyTypesEnum.GlassFilm)
                .HasValue<EnergyEfficientProperty>(PropertyTypesEnum.EnergyEfficient)
                .HasValue<CustomProperty>(PropertyTypesEnum.Custom)
                .HasValue<SignageProperty>(PropertyTypesEnum.Signage)
                .HasValue<OutdoorProperty>(PropertyTypesEnum.Outdoor)
                .HasValue<OtherProperty>(PropertyTypesEnum.Other);
    }
}
