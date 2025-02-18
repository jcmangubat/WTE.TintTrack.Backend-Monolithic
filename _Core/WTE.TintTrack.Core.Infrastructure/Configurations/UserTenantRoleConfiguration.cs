using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Infrastructure.Configurations;

public class UserTenantRoleConfiguration(string schema = "dbo")
    : EntityConfiguration<UserTenantRole, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<UserTenantRole> builder)
    {
        base.OnModelCreating(builder);

        builder.HasOne(role => role.UserTenant)
               .WithMany(userTenant => userTenant.UserTenantRoles)
               .HasForeignKey(role => role.UserTenantId)
               .IsRequired();

        builder.HasOne(role => role.Role)
               .WithMany()
               .HasForeignKey(role => role.RoleId)
               .IsRequired();
    }
}
