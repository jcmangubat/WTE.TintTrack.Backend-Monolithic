using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.SalesAndQuotingConfig;

public class WorkOrderLogPhotoConfiguration(string schema = "dbo")
    : EntityConfiguration<WorkOrderLogPhoto, Guid>(
        prefixEntityNameToId: false,
        prefixAltTblNameToEntity: false,
        schema: schema, pluralizeTblName: true
    )
{
    public override void OnModelCreating(EntityTypeBuilder<WorkOrderLogPhoto> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(p => p.FileCode, true, FieldLengths.General.CODE);
        builder.DefineDbField(p => p.FileUrl, true, FieldLengths.General.URL);
    }
}
