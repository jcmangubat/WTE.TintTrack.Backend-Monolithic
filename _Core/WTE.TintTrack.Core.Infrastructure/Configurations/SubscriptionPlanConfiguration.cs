using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Infrastructure.Configurations;

public class SubscriptionPlanConfiguration(string schema = "dbo")
    : EntityConfiguration<SubscriptionPlan, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<SubscriptionPlan> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(plan => plan.Level, true);
        builder.DefineDbField(plan => plan.Name, true, FieldLengths.SubscriptionPlan.Name);
        builder.DefineDbField(plan => plan.PlanCode, true, FieldLengths.SubscriptionPlan.PlanCode);

        builder.DefineDbField(plan => plan.Price, true, "decimal(18,2)");
        builder.DefineDbField(p => p.BillingCycle, true);

        builder.HasMany(plan => plan.TenantSubscriptions)
               .WithOne(subscription => subscription.SubscriptionPlan)
               .HasForeignKey(subscription => subscription.SubscriptionPlanId);

        builder.HasMany(plan => plan.SubscriptionPlanDiscounts)
               .WithOne(discount => discount.SubscriptionPlan)
               .HasForeignKey(discount => discount.SubscriptionPlanId);

        /*builder.HasMany(plan => plan.SubscriptionPlanFeatures)
               .WithMany(feature => feature.SubscriptionPlans);*/
    }
}
