using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations;

public class CustomerConfiguration(string schema = "dbo")
    : EntityConfiguration<Customer, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Customer> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.Customer.Code);
        entityBuilder.DefineDbField(p => p.Name, true, FieldLengths.General.Name);
        entityBuilder.DefineDbField(p => p.Company, false, FieldLengths.General.Name);
        entityBuilder.DefineDbField(p => p.Phone, false, FieldLengths.General.PhoneNumber);
        entityBuilder.DefineDbField(p => p.Phone2, false, FieldLengths.General.PhoneNumber);
        entityBuilder.DefineDbField(p => p.Email, false, FieldLengths.General.EmailAddress);
        entityBuilder.DefineDbField(p => p.CustomerStatus, true);
        entityBuilder.DefineDbField(p => p.Tags, false, FieldLengths.General.ExtraLong);

        entityBuilder.DefineDbField(p => p.StreetAddress, false, FieldLengths.GeneralAddress.StreetAddress);
        entityBuilder.DefineDbField(p => p.AddressLine2, false, FieldLengths.GeneralAddress.AddressLine2);
        entityBuilder.DefineDbField(p => p.City, false, FieldLengths.GeneralAddress.City);
        entityBuilder.DefineDbField(p => p.StateOrRegion, false, FieldLengths.GeneralAddress.StateOrRegionOrProvince);
        entityBuilder.DefineDbField(p => p.PostalCode, false, FieldLengths.GeneralAddress.PostalOrZIPCode);
        entityBuilder.DefineDbField(p => p.CountryISOCode, false, FieldLengths.GeneralAddress.CountryISOCode);

        entityBuilder.DefineDbField(p => p.IsImported, false);
        entityBuilder.DefineDbField(p => p.TaxExemptionReason, false);
        entityBuilder.DefineDbField(p => p.CreatedBy, false, FieldLengths.General.LENGTH120);

        /*// Navigation properties for related entities
        public virtual ICollection<CustomerContact> CustomerContacts { get; set; } = new HashSet<CustomerContact>();
        public virtual ICollection<Quote> Quotes { get; set; } = new HashSet<Quote>();
        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
        public virtual ICollection<CustomerProperty> CustomerProperties { get; set; } = new HashSet<CustomerProperty>();*/
    }
}
