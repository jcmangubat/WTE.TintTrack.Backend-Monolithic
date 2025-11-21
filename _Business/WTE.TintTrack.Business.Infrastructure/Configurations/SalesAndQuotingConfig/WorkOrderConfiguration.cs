using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.SalesAndQuotingConfig;

public class WorkOrderConfiguration(string schema = "dbo")
    : EntityConfiguration<WorkOrder, Guid>(
        prefixEntityNameToId: false,
        prefixAltTblNameToEntity: false,
        schema: schema, pluralizeTblName: true
    )
{
    public override void OnModelCreating(EntityTypeBuilder<WorkOrder> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(p => p.Title, true, FieldLengths.General.Name);
        builder.DefineDbField(p => p.Description, false, FieldLengths.General.SummaryParagraph);
        builder.DefineDbField(p => p.ScheduledDate, true, "datetime2");
        builder.DefineDbField(p => p.CompletionDate, false, "datetime2");
        builder.DefineDbField(p => p.WorkOrderStatus, true);

        builder.HasMany(p => p.WorkOrderAssignments)
            .WithOne(p => p.WorkOrder)
            .HasForeignKey(p => p.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.WorkOrderLogs)
            .WithOne(p => p.WorkOrder)
            .HasForeignKey(p => p.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.WorkOrderItems)
            .WithOne(p => p.WorkOrder)
            .HasForeignKey(p => p.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
