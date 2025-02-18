using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations;

public class ContactConfiguration(string schema = "dbo")
    : EntityConfiguration<Contact, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Contact> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);
        entityBuilder.DefineDbField(p => p.FirstName, true, FieldLengths.General.LENGTH30);
        entityBuilder.DefineDbField(p => p.LastName, false, FieldLengths.General.LENGTH30);
        entityBuilder.DefineDbField(p => p.Phone, false, FieldLengths.General.PhoneNumber);
        entityBuilder.DefineDbField(p => p.Mobile, false, FieldLengths.General.PhoneNumber);
        entityBuilder.DefineDbField(p => p.AltPhone, false, FieldLengths.General.PhoneNumber);
        entityBuilder.DefineDbField(p => p.Email, false, FieldLengths.General.EmailAddress);

        entityBuilder.DefineDbField(p => p.StreetAddress, false, FieldLengths.GeneralAddress.StreetAddress);
        entityBuilder.DefineDbField(p => p.AddressLine2, false, FieldLengths.GeneralAddress.AddressLine2);
        entityBuilder.DefineDbField(p => p.City, false, FieldLengths.GeneralAddress.City);
        entityBuilder.DefineDbField(p => p.StateOrRegion, false, FieldLengths.GeneralAddress.StateOrRegionOrProvince);
        entityBuilder.DefineDbField(p => p.PostalCode, false, FieldLengths.GeneralAddress.PostalOrZIPCode);
        entityBuilder.DefineDbField(p => p.CountryISOCode, false, FieldLengths.GeneralAddress.CountryISOCode);

        entityBuilder.DefineDbField(p => p.IsImported, false);
        entityBuilder.DefineDbField(p => p.Tags, false, FieldLengths.General.ExtraLong);
        entityBuilder.DefineDbField(p => p.DateOfBirth, false);
        entityBuilder.DefineDbField(p => p.Website, false, FieldLengths.General.URL);
        entityBuilder.DefineDbField(p => p.ContactType, true);
        entityBuilder.DefineDbField(p => p.JobTitle, false, FieldLengths.General.LENGTH30);
        entityBuilder.DefineDbField(p => p.Notes, false, FieldLengths.General.SummaryParagraph);
    }
}
