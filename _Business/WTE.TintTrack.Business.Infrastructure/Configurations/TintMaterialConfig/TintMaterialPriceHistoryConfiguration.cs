using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.TintMaterialConfig;

public class TintMaterialPriceHistoryConfiguration(string schema = "dbo")
    : EntityConfiguration<TintMaterialPriceHistory, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<TintMaterialPriceHistory> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.OldPrice, true);
        entityBuilder.DefineDbField(p => p.NewPrice, true);
        entityBuilder.DefineDbField(p => p.ChangedOn, true);
    }
}
