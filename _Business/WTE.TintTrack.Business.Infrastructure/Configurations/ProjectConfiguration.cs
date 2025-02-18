using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations;

public class ProjectConfiguration(string schema = "dbo")
    : EntityConfiguration<Project, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Project> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);
        entityBuilder.DefineDbField(p => p.ProjectName, true, FieldLengths.General.Name);
        entityBuilder.DefineDbField(p => p.Description, true, FieldLengths.General.LENGTH500);
        entityBuilder.DefineDbField(p => p.StartDate, true);
        entityBuilder.DefineDbField(p => p.EndDate, false);
        entityBuilder.DefineDbField(p => p.TaxExemptionReason, false);

        entityBuilder.DefineDbField(p => p.EstimatedCost, true, "decimal(18,2)", p => p.HasColumnType("decimal(18, 2)").HasPrecision(18, 2));
        entityBuilder.DefineDbField(p => p.ActualCost, true, "decimal(18,2)", p => p.HasColumnType("decimal(18, 2)").HasPrecision(18, 2));

        entityBuilder.HasOne(p => p.Quote).WithMany(p => p.Projects).HasForeignKey(p => p.QuoteId);
        entityBuilder.HasOne(p => p.Proposal).WithMany(p => p.Projects).HasForeignKey(p => p.ProposalId);
    }
}
