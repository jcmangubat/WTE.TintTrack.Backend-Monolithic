using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.TintMaterialConfig;

public class TintMaterialConfiguration(string schema = "dbo")
    : EntityConfiguration<TintMaterial, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<TintMaterial> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);
        entityBuilder.DefineDbField(p => p.Name, true, FieldLengths.General.Name);
        entityBuilder.DefineDbField(p => p.Description, true, FieldLengths.General.SummaryParagraph);

        entityBuilder.DefineDbField(p => p.RollLength, true, "decimal(18,2)");
        entityBuilder.DefineDbField(p => p.RollWidth, true, "decimal(18,2)");

        entityBuilder.DefineDbField(p => p.UnitOfMeasure, true);

        entityBuilder.HasMany(p => p.TintMaterialPriceSchedules)
                        .WithOne(p => p.TintMaterial)
                        .HasForeignKey(p => p.TintMaterialId)
                        .OnDelete(DeleteBehavior.Cascade);

        entityBuilder.HasMany(p => p.InventoryItems)
                        .WithOne(p => p.TintMaterial)
                        .HasForeignKey(p => p.TintMaterialId)
                        .OnDelete(DeleteBehavior.Restrict);

        entityBuilder.HasMany(p => p.QuoteItems)
                        .WithOne(p => p.TintMaterial)
                        .HasForeignKey(p => p.TintMaterialId)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired(false);

        entityBuilder.HasMany(p => p.EstimateItems)
                        .WithOne(p => p.TintMaterial)
                        .HasForeignKey(p => p.TintMaterialId)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired(false);

        entityBuilder.HasMany(p => p.ProposalItems)
                        .WithOne(p => p.TintMaterial)
                        .HasForeignKey(p => p.TintMaterialId)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired(false);

        entityBuilder.HasMany(p => p.WorkOrderItems)
                        .WithOne(p => p.TintMaterial)
                        .HasForeignKey(p => p.TintMaterialId)
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired(false);

        /*        entityBuilder.HasMany(p => p.WorkOrderItems)
                                .WithOne(p => p.TintMaterial)
                                .HasForeignKey(p => p.TintMaterialId)
                                .OnDelete(DeleteBehavior.Restrict)
                                .IsRequired(false);*/
    }
}
