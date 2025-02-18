using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Infrastructure.Configurations;

public class UserBillingProfileConfiguration(string schema = "dbo")
    : EntityConfiguration<UserBillingProfile, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<UserBillingProfile> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(profile => profile.BillingAddress, true, FieldLengths.UserBillingProfile.BillingAddress, null, "nvarchar");
        builder.DefineDbField(profile => profile.BillingDetailsJson, true, FieldLengths.UserBillingProfile.BillingDetailsJson, null, "nvarchar");
        builder.DefineDbField(profile => profile.BillingProfileType, true);

        builder.HasOne(profile => profile.User)
               .WithMany(user => user.UserBillingProfiles)
               .HasForeignKey(profile => profile.UserId)
               .IsRequired(true)
               .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
    }
}
