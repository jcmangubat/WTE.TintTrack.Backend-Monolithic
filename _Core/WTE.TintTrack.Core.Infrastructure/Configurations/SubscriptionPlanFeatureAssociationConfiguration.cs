using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Infrastructure.Configurations;

public class SubscriptionPlanFeatureAssociationConfiguration
    : IEntityTypeConfiguration<SubscriptionPlanFeatureAssociation>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlanFeatureAssociation> modelBuilder)
    {
        // Configure composite primary key
        modelBuilder.HasKey(spf => new { spf.SubscriptionPlanId, spf.SubscriptionPlanFeatureId });

        // Configure the relationship with SubscriptionPlan
        modelBuilder
            .HasOne(spf => spf.SubscriptionPlan)
            .WithMany(sp => sp.SubscriptionPlanFeatureAssociations)
            .HasForeignKey(spf => spf.SubscriptionPlanId);

        // Configure the relationship with SubscriptionPlanFeature
        modelBuilder
            .HasOne(spf => spf.SubscriptionPlanFeature)
            .WithMany(sf => sf.SubscriptionPlanFeatureAssociations)
            .HasForeignKey(spf => spf.SubscriptionPlanFeatureId);
    }
}
