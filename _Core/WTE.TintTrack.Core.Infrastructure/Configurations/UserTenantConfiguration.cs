using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Infrastructure.Configurations;

public class UserTenantConfiguration(string schema = "dbo")
    : EntityConfiguration<UserTenant, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<UserTenant> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(p => p.IsDefault, false);
        builder.DefineDbField(p => p.UserIsOwner, false);

        builder.HasOne(userTenant => userTenant.User)
               .WithMany(user => user.UserTenants)
               .HasForeignKey(userTenant => userTenant.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(userTenant => userTenant.Tenant)
               .WithMany(tenant => tenant.UserTenants)
               .HasForeignKey(userTenant => userTenant.TenantId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(userTenant => userTenant.UserTenantRoles)
               .WithOne(role => role.UserTenant)
               .HasForeignKey(role => role.UserTenantId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
