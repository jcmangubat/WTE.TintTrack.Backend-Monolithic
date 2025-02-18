using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Infrastructure.Configurations;

public class SubscriptionPlanDiscountConfiguration(string schema = "dbo")
    : EntityConfiguration<SubscriptionPlanDiscount, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<SubscriptionPlanDiscount> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(discount => discount.PlanDiscountCode, true, FieldLengths.SubscriptionPlanDiscount.Code);
        builder.DefineDbField(discount => discount.Name, true, FieldLengths.SubscriptionPlanDiscount.Name);
        builder.DefineDbField(discount => discount.Percentage, true, "decimal(18,2)");
        builder.DefineDbField(discount => discount.StartDate, true);
        builder.DefineDbField(discount => discount.EndDate, true);

        builder.HasOne(discount => discount.SubscriptionPlan)
               .WithMany(plan => plan.SubscriptionPlanDiscounts)
               .HasForeignKey(discount => discount.SubscriptionPlanId);
    }
}
