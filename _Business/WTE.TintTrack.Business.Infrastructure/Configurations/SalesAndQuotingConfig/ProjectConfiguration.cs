using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.SalesAndQuotingConfig;

public class ProjectConfiguration(string schema = "dbo")
    : EntityConfiguration<Project, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Project> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);
        entityBuilder.DefineDbField(p => p.Name, true, FieldLengths.General.Name);
        entityBuilder.DefineDbField(p => p.Description, false, FieldLengths.General.LENGTH100);
        entityBuilder.DefineDbField(p => p.StartDate, true, "datetime2");
        entityBuilder.DefineDbField(p => p.EndDate, false, "datetime2");
        entityBuilder.DefineDbField(p => p.Status, true);

        /// One-to-one link to the Contract (which has WorkOrders, Milestones, etc.)
        /// A contract cannot be deleted when a project is linked to it
        entityBuilder.HasOne(p => p.Contract)
                    .WithOne(c => c.Project)
                    .HasForeignKey<Project>(p => p.ContractId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);

        entityBuilder.HasMany(p => p.ProjectMilestones)
                    .WithOne(p => p.Project)
                    .HasForeignKey(p => p.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

        entityBuilder.HasMany(p => p.WorkOrders)
                    .WithOne(p => p.Project)
                    .HasForeignKey(p => p.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);
    }
}
