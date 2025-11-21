using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Common.Constants;
using Microsoft.EntityFrameworkCore;

namespace WTE.TintTrack.Business.Infrastructure.Configurations;

public class CustomerConfiguration(string schema = "dbo")
    : EntityConfiguration<Customer, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Customer> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.Customer.Code);
        entityBuilder.DefineDbField(p => p.Name, true, FieldLengths.Customer.Name);
        entityBuilder.DefineDbField(p => p.IndustryType, false, FieldLengths.Customer.IndustryType);
        entityBuilder.DefineDbField(p => p.MainPhone, false, FieldLengths.Customer.MainPhone);
        entityBuilder.DefineDbField(p => p.GeneralEmail, false, FieldLengths.General.EmailAddress);
        entityBuilder.DefineDbField(p => p.Website, false, FieldLengths.Customer.Website);
        entityBuilder.DefineDbField(p => p.CustomerStatus, true);

        entityBuilder.DefineDbField(p => p.Notes, false, FieldLengths.Customer.Notes);
        entityBuilder.DefineDbField(p => p.Tags, false, FieldLengths.General.ExtraLong);

        entityBuilder.DefineDbField(p => p.IsImported, false);
        entityBuilder.DefineDbField(p => p.TaxExemptionReason, false);

        entityBuilder.HasMany(p => p.Addresses)
                        .WithOne(p => p.Customer)
                        .HasForeignKey(p => p.CustomerId)
                        .OnDelete(DeleteBehavior.SetNull);

        entityBuilder.HasMany(p => p.PropertyAssets)
                        .WithOne(p => p.Customer)
                        .HasForeignKey(p => p.CustomerId)
                        .OnDelete(DeleteBehavior.Cascade);

        entityBuilder.HasMany(p => p.TintMaterialPriceOverrides)
                        .WithOne(p => p.Customer)
                        .HasForeignKey(p => p.CustomerId)
                        .OnDelete(DeleteBehavior.Cascade);

        entityBuilder.HasMany(p => p.CustomerContacts)
                        .WithOne(p => p.Customer)
                        .HasForeignKey(p => p.CustomerId)
                        .OnDelete(DeleteBehavior.Cascade);
    }
}
