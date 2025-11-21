using Microsoft.EntityFrameworkCore;
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

        entityBuilder.HasMany(p => p.Inquiries)
                        .WithOne(p => p.CustomerContact)
                        .HasForeignKey(p => p.CustomerContactId)
                        .OnDelete(DeleteBehavior.Cascade);

        //entityBuilder.HasMany(p => p.CommercialOfferRecipients)
        //                .WithOne(p => p.CustomerContact)
        //                .HasForeignKey(p => p.CustomerContactId)
        //                .OnDelete(DeleteBehavior.Cascade);

        //entityBuilder.HasMany(p => p.Proposals)
        //                .WithOne(p => p.CustomerContact)
        //                .HasForeignKey(p => p.CustomerContactId)
        //                .OnDelete(DeleteBehavior.Cascade);

        //entityBuilder.HasMany(p => p.Quotes)
        //                .WithOne(p => p.CustomerContact)
        //                .HasForeignKey(p => p.CustomerContactId)
        //                .OnDelete(DeleteBehavior.Cascade);

        //entityBuilder.HasMany(p => p.Estimates)
        //                .WithOne(p => p.CustomerContact)
        //                .HasForeignKey(p => p.CustomerContactId)
        //                .OnDelete(DeleteBehavior.Cascade);
    }
}
