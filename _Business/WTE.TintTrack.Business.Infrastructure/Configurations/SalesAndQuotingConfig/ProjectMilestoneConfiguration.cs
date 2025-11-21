using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.SalesAndQuotingConfig;

public class ProjectMilestoneConfiguration(string schema = "dbo")
    : EntityConfiguration<ProjectMilestone, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<ProjectMilestone> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);
        entityBuilder.DefineDbField(p => p.Name, true, FieldLengths.General.Name);
        entityBuilder.DefineDbField(p => p.Description, false, FieldLengths.General.SummaryParagraph);
        entityBuilder.DefineDbField(p => p.ExpectedStartDate, false, "datetime2");
        entityBuilder.DefineDbField(p => p.ExpectedEndDate, false, "datetime2");
        entityBuilder.DefineDbField(p => p.EstimatedAmount, false, "decimal(18,2)");
        entityBuilder.DefineDbField(p => p.IsCompleted, true);

        /*entityBuilder.HasMany(p => p.WorkOrders)
                        .WithOne(p => p.ProjectMilestone)
                        .HasForeignKey(p => p.ProjectMilestoneId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired(false);*/
    }
}