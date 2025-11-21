using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.TintServiceEntities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.TintServiceConfig;

public class TintServiceConfiguration(string schema = "dbo")
    : EntityConfiguration<TintService, Guid>(
        prefixEntityNameToId: false,
        prefixAltTblNameToEntity: false,
        schema: schema, pluralizeTblName: true
    )
{
    public override void OnModelCreating(EntityTypeBuilder<TintService> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);
        builder.DefineDbField(p => p.Name, true, FieldLengths.General.Name);
        builder.DefineDbField(p => p.Description, true, FieldLengths.General.SummaryParagraph);
        builder.DefineDbField(p => p.Price, true, "decimal(18,2)", p => p.HasColumnType("decimal(18, 2)").HasPrecision(18, 2));
        builder.DefineDbField(p => p.ServiceType, true);
        builder.DefineDbField(p => p.EstimatedDurationMinutes, true);
        builder.DefineDbField(p => p.AdditionalFeatures, true, FieldLengths.General.SummaryParagraph);

        builder.HasMany(p => p.QuoteItems)
            .WithOne(p => p.TintService)
            .HasForeignKey(p => p.TintServiceId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasMany(p => p.EstimateItems)
            .WithOne(p => p.TintService)
            .HasForeignKey(p => p.TintServiceId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasMany(p => p.ProposalItems)
            .WithOne(p => p.TintService)
            .HasForeignKey(p => p.TintServiceId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasMany(p => p.WorkOrderItems)
            .WithOne(p => p.TintService)
            .HasForeignKey(p => p.TintServiceId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}
