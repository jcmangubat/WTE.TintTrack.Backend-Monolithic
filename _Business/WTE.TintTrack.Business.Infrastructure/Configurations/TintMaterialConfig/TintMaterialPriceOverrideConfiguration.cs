using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.TintMaterialConfig;

public class TintMaterialPriceOverrideConfiguration(string schema = "dbo")
    : EntityConfiguration<TintMaterialPriceOverride, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<TintMaterialPriceOverride> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.CustomPrice, true, "decimal(18,2)");
    }
}
