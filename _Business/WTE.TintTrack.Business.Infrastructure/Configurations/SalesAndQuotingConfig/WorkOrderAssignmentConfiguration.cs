using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.SalesAndQuotingConfig;

public class WorkOrderAssignmentConfiguration(string schema = "dbo")
    : EntityConfiguration<WorkOrderAssignment, Guid>(
        prefixEntityNameToId: false,
        prefixAltTblNameToEntity: false,
        schema: schema, pluralizeTblName: true
    )
{
    public override void OnModelCreating(EntityTypeBuilder<WorkOrderAssignment> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(p => p.UserCode, true, FieldLengths.General.CODE);
        builder.DefineDbField(p => p.Role, true, FieldLengths.General.LENGTH100);
    }
}
