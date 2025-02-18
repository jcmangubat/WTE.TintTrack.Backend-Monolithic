using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Infrastructure.Configurations;

public class TokenConfiguration(string schema = "dbo")
    : EntityConfiguration<Token, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false,
        schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Token> builder)
    {
        base.OnModelCreating(builder);
        builder.DefineDbField(tenant => tenant.RefreshToken, true, FieldLengths.Token.RefreshToken, null, "nvarchar");
        builder.DefineDbField(tenant => tenant.RefreshTokenExpiration, true);

        builder.HasOne(token => token.User)
               .WithMany(user => user.Tokens)
               .HasForeignKey(token => token.UserId);

        builder.HasOne(token => token.Tenant)
               .WithMany(user => user.Tokens)
               .HasForeignKey(token => token.TenantId);
    }
}
