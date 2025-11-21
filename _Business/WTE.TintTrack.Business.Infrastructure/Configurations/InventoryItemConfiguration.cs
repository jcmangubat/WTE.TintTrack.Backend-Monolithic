using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities;

namespace WTE.TintTrack.Business.Infrastructure.Configurations;

public class InventoryItemConfiguration(string schema = "dbo")
    : EntityConfiguration<InventoryItem, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<InventoryItem> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.QuantityInStock, true, "decimal(18,2)");
        entityBuilder.DefineDbField(p => p.ReservedQuantity, true, "decimal(18,2)");
        entityBuilder.DefineDbField(p => p.ReorderLevel, true, "decimal(18,2)");
        entityBuilder.DefineDbField(p => p.UnitOfMeasure, true);
    }
}
