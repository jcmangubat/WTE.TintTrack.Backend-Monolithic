using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Infrastructure.Configurations;

public class TenantSubscriptionConfiguration(string schema = "dbo")
    : EntityConfiguration<TenantSubscription, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<TenantSubscription> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(p => p.SubscriptionStatus, true);

        builder.HasOne(subscription => subscription.Tenant)
               .WithMany(tenant => tenant.TenantSubscriptions)
               .HasForeignKey(subscription => subscription.TenantId);

        builder.HasOne(subscription => subscription.SubscriptionPlan)
               .WithMany(plan => plan.TenantSubscriptions)
               .HasForeignKey(subscription => subscription.SubscriptionPlanId);

        builder.HasMany(subscription => subscription.TenantSubscriptionInvoices)
               .WithOne(invoice => invoice.TenantSubscription)
               .HasForeignKey(invoice => invoice.TenantSubscriptionId);
    }
}
