using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities;

namespace WTE.TintTrack.Business.Infrastructure.Configurations;

public class CustomerContactConfiguration(string schema = "dbo")
    : EntityConfiguration<CustomerContact, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<CustomerContact> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.RelationshipType, true);

        entityBuilder.HasOne(p => p.Customer)
                        .WithMany(p => p.CustomerContacts)
                        .HasForeignKey(p => p.CustomerId)
                        .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

        entityBuilder.HasOne(p => p.Contact)
                        .WithMany(p => p.CustomerContacts)
                        .HasForeignKey(p => p.ContactId)
                        .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
    }
}
