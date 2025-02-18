using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations;

public class AuditLogConfiguration(string schema = "dbo") : EntityConfiguration<AuditLog, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<AuditLog> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(al => al.UserId, true);
        builder.DefineDbField(al => al.ActionDate, true);
        builder.DefineDbField(al => al.Action, true, FieldLengths.General.LENGTH150);
    }
}
