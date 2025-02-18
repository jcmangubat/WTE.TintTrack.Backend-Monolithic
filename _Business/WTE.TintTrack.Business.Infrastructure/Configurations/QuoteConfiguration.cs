using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations;

public class QuoteConfiguration(string schema = "dbo")
    : EntityConfiguration<Quote, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Quote> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);
        entityBuilder.DefineDbField(p => p.QuoteNumber, true, FieldLengths.General.LENGTH15);
        entityBuilder.DefineDbField(p => p.QuoteDate, true);
        entityBuilder.DefineDbField(p => p.TotalAmount, true, "decimal(18,2)", p => p.HasColumnType("decimal(18, 2)").HasPrecision(18, 2));
        entityBuilder.DefineDbField(p => p.Description, true, FieldLengths.General.SummaryParagraph);
        entityBuilder.DefineDbField(p => p.IsAccepted, false);

        entityBuilder.HasOne(p => p.Customer)
                        .WithMany(p => p.Quotes)
                        .HasForeignKey(p => p.CustomerId)
                        .OnDelete(DeleteBehavior.Cascade);
    }
}