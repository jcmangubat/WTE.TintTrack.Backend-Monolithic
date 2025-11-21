using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.CommercialOffersConfig;

public class ProposalConfiguration(string schema = "dbo")
    : EntityConfiguration<Proposal, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Proposal> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.SolutionDescription, true, FieldLengths.General.SummaryParagraph);
        entityBuilder.DefineDbField(p => p.TotalCost, true, "decimal(18,2)");
        entityBuilder.DefineDbField(p => p.TermsAndConditions, true, FieldLengths.General.SummaryParagraph);
        entityBuilder.DefineDbField(p => p.ProjectTimeline, true, FieldLengths.General.SummaryParagraph);
        entityBuilder.DefineDbField(p => p.Deliverables, true, FieldLengths.General.SummaryParagraph);

        // =============================================
        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);
        entityBuilder.DefineDbField(p => p.ExpiryDate, false, "datetime2");
        entityBuilder.DefineDbField(p => p.IssuanceDate, true, "datetime2");
        entityBuilder.DefineDbField(p => p.SourceDocRef, false, FieldLengths.General.ExtraLong);
        entityBuilder.DefineDbField(p => p.OfferDocumentStatus, true);
        entityBuilder.DefineDbField(p => p.Currency, true, FieldLengths.General.ExtraShort);

        entityBuilder.HasMany(p => p.ProposalItems)
                        .WithOne(p => p.Proposal)
                        .HasForeignKey(p => p.ProposalId)
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired(true);

        entityBuilder.HasMany(p => p.OfferRecipients)
                        .WithOne(p => p.Proposal)
                        .HasForeignKey(p => p.ProposalId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired(false);

        entityBuilder.HasMany(p => p.OfferMilestones)
                        .WithOne(p => p.Proposal)
                        .HasForeignKey(p => p.ProposalId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired(false);
    }
}
