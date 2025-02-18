using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Infrastructure.Configurations;

public class UserTenantInvitationConfiguration(string schema = "dbo")
    : EntityConfiguration<UserTenantInvitation, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<UserTenantInvitation> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(ti => ti.EmailAddress, true, FieldLengths.TenantInvitation.EmailAddress, null, "nvarchar");
        builder.DefineDbField(ti => ti.InvitationStatus, true);
        builder.DefineDbField(ti => ti.InvitationSource, true);

        builder.HasOne(ti => ti.Tenant)
               .WithMany(tenant => tenant.UserTenantInvitations)
               .HasForeignKey(ti => ti.TenantId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired(false);

        builder.HasOne(ti => ti.User)
               .WithMany(tenant => tenant.UserTenantInvitations)
               .HasForeignKey(ti => ti.UserId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired(false);
    }
}