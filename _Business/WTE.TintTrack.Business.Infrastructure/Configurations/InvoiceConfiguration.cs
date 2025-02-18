using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations;

public class InvoiceConfiguration(string schema = "dbo")
    : EntityConfiguration<Invoice, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Invoice> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);

        entityBuilder.DefineDbField(p => p.InvoiceNumber, true, FieldLengths.General.LENGTH15);
        entityBuilder.DefineDbField(p => p.InvoiceDate, true);

        entityBuilder.DefineDbField(p => p.TotalAmount, true, "decimal(18,2)", p => p.HasColumnType("decimal(18, 2)").HasPrecision(18, 2));
        entityBuilder.DefineDbField(p => p.AmountPaid, false, "decimal(18,2)", p => p.HasColumnType("decimal(18, 2)").HasPrecision(18, 2));

        entityBuilder.DefineDbField(p => p.PaymentDate, false);
    }
}