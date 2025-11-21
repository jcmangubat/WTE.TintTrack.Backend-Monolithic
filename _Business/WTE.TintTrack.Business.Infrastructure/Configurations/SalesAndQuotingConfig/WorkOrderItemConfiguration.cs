using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.SalesAndQuotingConfig;

public class WorkOrderItemConfiguration(string schema = "dbo")
    : EntityConfiguration<WorkOrderItem, Guid>(
        prefixEntityNameToId: false,
        prefixAltTblNameToEntity: false,
        schema: schema, pluralizeTblName: true
    )
{
    public override void OnModelCreating(EntityTypeBuilder<WorkOrderItem> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(p => p.Description, false, FieldLengths.General.SummaryParagraph);
        builder.DefineDbField(p => p.Quantity, true, "decimal(18,2)", p => p.HasColumnType("decimal(18, 2)").HasPrecision(18, 2));
        builder.DefineDbField(p => p.Rate, true, "decimal(18,2)", p => p.HasColumnType("decimal(18, 2)").HasPrecision(18, 2));
    }
}
