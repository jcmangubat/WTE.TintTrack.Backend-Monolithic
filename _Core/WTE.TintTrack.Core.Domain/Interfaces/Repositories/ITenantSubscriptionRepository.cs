using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using WTE.TintTrack.Core.Domain.Entities;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Domain.Interfaces.Repositories;

public interface ITenantSubscriptionRepository : IRepositoryForKeyedEntity<TenantSubscription, Guid>
{
    /// <summary>
    /// Gets the active subscription for the given tenant.
    /// </summary>
    /// <param name="tenantCode">The tenant's code.</param>
    /// <returns>List of TenantSubscriptions.</returns>
    Task<IEnumerable<TenantSubscription>?> GetByTenantAsync(string tenantCode, SubscriptionStatusEnum? subscriptionStatus= null);

    Task<IEnumerable<TenantSubscription>?> GetBySubscriptionPlanAsync(Guid subscriptionId);

    /// <summary>
    /// Gets a subscription by its unique identifier.
    /// </summary>
    /// <param name="subscriptionId">The subscription's ID.</param>
    /// <returns>The TenantSubscription if found; otherwise, null.</returns>
    Task<TenantSubscription?> GetByIdAsync(Guid subscriptionId);

    /// <summary>
    /// Gets a subscription by its unique identifier with invoices and payments included.
    /// </summary>
    /// <param name="subscriptionId">The subscription's ID.</param>
    /// <returns>The TenantSubscription with invoices and payments if found; otherwise, null.</returns>
    Task<TenantSubscription?> GetByIdWithInvoicesAndPaymentsAsync(Guid subscriptionId);

    /// <summary>
    /// Deletes a TenantSubscription by its ID.
    /// </summary>
    /// <param name="subscriptionId">The subscription's ID.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task DeleteAsync(Guid subscriptionId);
}

