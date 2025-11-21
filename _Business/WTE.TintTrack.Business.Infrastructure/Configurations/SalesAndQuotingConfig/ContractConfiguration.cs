using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;
using WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.SalesAndQuotingConfig;

public class ContractConfiguration(string schema = "dbo")
    : EntityConfiguration<Contract, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Contract> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);
        entityBuilder.DefineDbField(p => p.BillingType, true);
        entityBuilder.DefineDbField(p => p.FixedAmount, false, "decimal(18,2)");
        entityBuilder.DefineDbField(p => p.HourlyRate, false, "decimal(18,2)");
        entityBuilder.DefineDbField(p => p.PaymentTerm, true);
        entityBuilder.DefineDbField(p => p.IsPaidInFull, true);
        entityBuilder.DefineDbField(p => p.StartDate, true, "datetime2");
        entityBuilder.DefineDbField(p => p.EndDate, false, "datetime2");
        entityBuilder.DefineDbField(p => p.Notes, false, FieldLengths.General.SummaryParagraph);
        entityBuilder.DefineDbField(p => p.IsViewed, true);
        entityBuilder.DefineDbField(p => p.IsApproved, false);
        entityBuilder.DefineDbField(p => p.SignatureType, true);
        entityBuilder.DefineDbField(p => p.IsSigned, true);
        entityBuilder.DefineDbField(p => p.SignedDate, false, "datetime2");
        entityBuilder.DefineDbField(p => p.SignatureUrl, false, FieldLengths.General.URL);
        entityBuilder.DefineDbField(p => p.SignedBy, false, FieldLengths.General.Name);
        entityBuilder.DefineDbField(p => p.SignatureProvider, false, FieldLengths.General.Name);
        entityBuilder.DefineDbField(p => p.SignatureEnvelopeId, false, FieldLengths.General.Name);

        entityBuilder.HasMany(p => p.ContractMilestones)
                        .WithOne(p => p.Contract)
                        .HasForeignKey(p => p.ContractId)
                        .OnDelete(DeleteBehavior.Cascade);

        entityBuilder.HasOne(p => p.Proposal).WithOne(p => p.Contract)
                        .HasForeignKey<Proposal>(p => p.ContractId)
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired(false);

        entityBuilder.HasOne(p => p.Quote).WithOne(p => p.Contract)
                        .HasForeignKey<Quote>(p => p.ContractId)
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired(false);

        entityBuilder.HasOne(p => p.Estimate).WithOne(p => p.Contract)
                        .HasForeignKey<Estimate>(p => p.ContractId)
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired(false);

        /// One-to-One link to the Project (which has WorkOrders, Milestones, etc.)
        /// When a project is deleted, the contract is not deleted but instead, is set to null
        entityBuilder.HasOne(c => c.Project)
                        .WithOne(p => p.Contract)
                        .HasForeignKey<Contract>(c => c.ProjectId)
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired(false);
    }
}
