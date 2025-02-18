using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Domain.Entities;

public class SubscriptionPlan : GuidKeyedAuditableEntity
{
    /// <summary>
    /// Gets or sets the level of the subscription plan.
    /// </summary>
    /// <remarks>
    /// The <c>Level</c> property indicates the hierarchy or tier of the subscription plan.
    /// Higher values represent more advanced or premium plans.
    /// </remarks>
    /// <value>
    /// An integer representing the level of the subscription plan.
    /// </value>
    public required int Level { get; set; }

    /// <summary>
    /// Name of the pricing plan(e.g., Basic, Premium)
    /// </summary>
    public required string Name { get; set; }

    public  required string PlanCode { get; set; }

    /// <summary>
    /// Gets or sets the billing frequency of the subscription, 
    /// indicating how often the tenant is billed (e.g., Monthly, Annually).
    /// </summary>
    public required BillingCyclesEnum BillingCycle { get; set; }

    /// <summary>
    /// Monthly fee for the pricing plan
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Maximum number of users allowed in the plan
    /// </summary>
    public int? MaxUsers { get; set; }

    public virtual ICollection<TenantSubscription> TenantSubscriptions { get; set; } = new HashSet<TenantSubscription>();

    public virtual ICollection<SubscriptionPlanDiscount> SubscriptionPlanDiscounts { get; set; } = new HashSet<SubscriptionPlanDiscount>();

    //public virtual ICollection<SubscriptionPlanFeature> SubscriptionPlanFeatures { get; set; } = new HashSet<SubscriptionPlanFeature>();

    public virtual ICollection<SubscriptionPlanFeatureAssociation> SubscriptionPlanFeatureAssociations { get; set; }
}
