using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.TintMaterialConfig;

public class TintMaterialPriceScheduleConfiguration(string schema = "dbo")
    : EntityConfiguration<TintMaterialPriceSchedule, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<TintMaterialPriceSchedule> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.UnitCost, true, "decimal(18,2)");
        entityBuilder.DefineDbField(p => p.MarkupPercentage, true, "decimal(18,2)");
        
        //entityBuilder.DefineDbField(p => p.FinalPrice, true, "decimal(18,2)");
        entityBuilder.Ignore(p => p.FinalPrice);

        entityBuilder.DefineDbField(p => p.EffectiveFrom, true);
        entityBuilder.DefineDbField(p => p.EffectiveTo, false);
        entityBuilder.DefineDbField(p => p.IsCurrent, true);
        
        entityBuilder.DefineDbField(p => p.CalculationType, true);
        entityBuilder.DefineDbField(p => p.CustomFormula, true, FieldLengths.General.LENGTH130);

        entityBuilder.HasMany(p => p.TintMaterialPriceTiers)
                        .WithOne(p => p.TintMaterialPriceSchedule)
                        .HasForeignKey(p => p.TintMaterialPriceScheduleId)
                        .OnDelete(DeleteBehavior.Cascade);

        entityBuilder.HasMany(p => p.TintMaterialPriceOverrides)
                        .WithOne(p => p.TintMaterialPriceSchedule)
                        .HasForeignKey(p => p.TintMaterialPriceScheduleId)
                        .OnDelete(DeleteBehavior.Cascade);
    }
}
