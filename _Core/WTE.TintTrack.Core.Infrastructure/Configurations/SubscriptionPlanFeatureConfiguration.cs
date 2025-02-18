using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Infrastructure.Configurations;

public class SubscriptionPlanFeatureConfiguration(string schema = "dbo")
    : EntityConfiguration<SubscriptionPlanFeature, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<SubscriptionPlanFeature> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(feature => feature.FeatureCode, true, FieldLengths.SubscriptionPlanFeature.Code, null, "nvarchar");
        builder.DefineDbField(feature => feature.Name, true, FieldLengths.SubscriptionPlanFeature.Name, null, "nvarchar");
        builder.DefineDbField(feature => feature.Description, true, FieldLengths.SubscriptionPlanFeature.Description, null, "nvarchar");
    }
}
