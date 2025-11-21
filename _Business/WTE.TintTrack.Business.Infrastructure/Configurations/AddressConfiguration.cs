using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations;

public class AddressConfiguration(string schema = "dbo")
    : EntityConfiguration<Address, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Address> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);

        entityBuilder.DefineDbField(p => p.Street, true, FieldLengths.Address.Street);
        entityBuilder.DefineDbField(p => p.AdditionalInfo, false, FieldLengths.Address.AdditionalInfo);
        entityBuilder.DefineDbField(p => p.City, true, FieldLengths.Address.City);
        entityBuilder.DefineDbField(p => p.StateOrRegion, true, FieldLengths.Address.StateOrRegion);
        entityBuilder.DefineDbField(p => p.PostalCode, true, FieldLengths.Address.PostalCode);
        entityBuilder.DefineDbField(p => p.Country, true, FieldLengths.Address.Country);
        entityBuilder.DefineDbField(p => p.CountryISOCode, true, FieldLengths.Address.CountryISOCode);
        entityBuilder.DefineDbField(p => p.Latitude, false, FieldLengths.Address.Latitude);
        entityBuilder.DefineDbField(p => p.Longitude, false, FieldLengths.Address.Longitude);
        entityBuilder.DefineDbField(p => p.AddressType, true);
    }
}
