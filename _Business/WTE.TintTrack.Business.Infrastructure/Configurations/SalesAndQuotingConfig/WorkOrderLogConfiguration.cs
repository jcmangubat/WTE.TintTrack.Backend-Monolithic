using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.SalesAndQuotingConfig;

public class WorkOrderLogConfiguration(string schema = "dbo")
    : EntityConfiguration<WorkOrderLog, Guid>(
        prefixEntityNameToId: false,
        prefixAltTblNameToEntity: false,
        schema: schema, pluralizeTblName: true
    )
{
    public override void OnModelCreating(EntityTypeBuilder<WorkOrderLog> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(p => p.UserCode, true, FieldLengths.General.CODE);
        builder.DefineDbField(p => p.WorkDateTime, true, "datetime2");
        builder.DefineDbField(p => p.Duration, true, "time");
        builder.DefineDbField(p => p.Notes, true, FieldLengths.General.SummaryParagraph);

        builder.HasMany(p => p.WorkOrderLogPhotos)
                .WithOne(p => p.WorkOrderLog)
                .HasForeignKey(p => p.WorkOrderLogId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
