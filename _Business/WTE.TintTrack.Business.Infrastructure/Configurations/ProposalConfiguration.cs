using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations;

public class ProposalConfiguration(string schema = "dbo")
    : EntityConfiguration<Proposal, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Proposal> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);
        entityBuilder.DefineDbField(p => p.ProposalNumber, true, FieldLengths.General.LENGTH15);
        entityBuilder.DefineDbField(p => p.Terms, true, FieldLengths.General.SummaryParagraph);
        entityBuilder.DefineDbField(p => p.ScopeOfWork, true, FieldLengths.General.SummaryParagraph);
        entityBuilder.DefineDbField(p => p.TotalAmount, true, "decimal(18,2)", p => p.HasColumnType("decimal(18, 2)").HasPrecision(18, 2));

        entityBuilder.HasOne(p => p.Quote)
                        .WithMany(p => p.Proposals)
                        .HasForeignKey(p => p.QuoteId);
    }
}
