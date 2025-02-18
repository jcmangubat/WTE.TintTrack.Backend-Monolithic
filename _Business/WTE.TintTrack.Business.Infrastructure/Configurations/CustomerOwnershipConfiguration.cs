using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations;

public class CustomerOwnershipConfiguration(string schema = "dbo")
    : EntityConfiguration<CustomerOwnership, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<CustomerOwnership> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.UserCode, true, FieldLengths.ApplicationUser.UserCode);
        entityBuilder.DefineDbField(p => p.UserIsOwner, false);

        entityBuilder.HasOne(p => p.Customer)
                        .WithMany(p => p.CustomerOwnerships)
                        .HasForeignKey(p => p.CustomerId)
                        .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
    }
}
