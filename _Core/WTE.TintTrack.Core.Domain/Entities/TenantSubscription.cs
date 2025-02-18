using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Domain.Entities;

/// <summary>
/// Represents a subscription of a tenant to a specific plan within the application, 
/// including details about billing frequency, status, and associated payments.
/// </summary>
public class TenantSubscription : GuidKeyedAuditableEntity
{
    /// <summary>
    /// Gets or sets the current status of the subscription, 
    /// such as Active, Cancelled, or Suspended.
    /// </summary>
    public required SubscriptionStatusEnum SubscriptionStatus { get; set; } = SubscriptionStatusEnum.ForReview;

    /// <summary>
    /// Gets or sets the unique identifier of the tenant associated with this subscription.
    /// </summary>
    public required Guid TenantId { get; set; }
    public virtual Tenant Tenant { get; set; }


    /// <summary>
    /// Gets or sets the unique identifier of the subscription plan 
    /// to which the tenant has subscribed.
    /// </summary>
    public required Guid SubscriptionPlanId { get; set; }
    public virtual SubscriptionPlan SubscriptionPlan { get; set; }

    /// <summary>
    /// Gets or sets the collection of payment invoices associated with this subscription. 
    /// This includes all recorded payments related to the subscription's billing cycles.
    /// </summary>
    public virtual ICollection<TenantSubscriptionInvoice> TenantSubscriptionInvoices { get; set; }

}
