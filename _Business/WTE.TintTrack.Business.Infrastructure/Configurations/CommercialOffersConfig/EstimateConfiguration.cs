using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.CommercialOffersConfig;

public class EstimateConfiguration(string schema = "dbo")
    : EntityConfiguration<Estimate, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Estimate> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.EstimatedAmount, true, "decimal(18,2)");
        entityBuilder.DefineDbField(p => p.LaborCost, true, "decimal(18,2)");
        entityBuilder.DefineDbField(p => p.MaterialCost, true, "decimal(18,2)");
        entityBuilder.DefineDbField(p => p.AdditionalFees, true, "decimal(18,2)");
        entityBuilder.DefineDbField(p => p.Description, false, FieldLengths.General.SummaryParagraph);
        entityBuilder.DefineDbField(p => p.Notes, false, FieldLengths.General.ExtraLong);

        // =============================================
        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);
        entityBuilder.DefineDbField(p => p.ExpiryDate, false, "datetime2");
        entityBuilder.DefineDbField(p => p.IssuanceDate, true, "datetime2");
        entityBuilder.DefineDbField(p => p.SourceDocRef, false, FieldLengths.General.ExtraLong);
        entityBuilder.DefineDbField(p => p.OfferDocumentStatus, true);
        entityBuilder.DefineDbField(p => p.Currency, true, FieldLengths.General.ExtraShort);

        entityBuilder.HasMany(p => p.EstimateItems)
                        .WithOne(p => p.Estimate)
                        .HasForeignKey(p => p.EstimateId)
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired(true);

        entityBuilder.HasMany(p => p.OfferRecipients)
                        .WithOne(p => p.Estimate)
                        .HasForeignKey(p => p.EstimateId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired(false);

        entityBuilder.HasMany(p => p.OfferMilestones)
                        .WithOne(p => p.Estimate)
                        .HasForeignKey(p => p.EstimateId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired(false);
    }
}