using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Infrastructure.Configurations;

public class PermissionConfiguration(string schema = "dbo")
    : EntityConfiguration<Permission, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Permission> builder)
    {
        base.OnModelCreating(builder);
        
        builder.DefineDbField(p => p.Feature, true);
        builder.DefineDbField(p => p.PermissionLevel, true);

        builder.DefineDbField(p => p.Name, true, FieldLengths.General.LENGTH50);
        builder.DefineDbField(p => p.Description, true, FieldLengths.General.LENGTH120);
    }
}
