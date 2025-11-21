using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.TintMaterialConfig;

public class TintMaterialPriceTierConfiguration(string schema = "dbo")
    : EntityConfiguration<TintMaterialPriceTier, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<TintMaterialPriceTier> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.MinQuantity, true, "decimal(18,2)");
        entityBuilder.DefineDbField(p => p.DiscountPercentage, true, "decimal(18,2)");
    }
}