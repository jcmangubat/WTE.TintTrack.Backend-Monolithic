using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Application.Interfaces;

public interface ITenantSubscriptionService : IMappedLoggingService<ITenantSubscriptionService>
{
    Task<TenantSubscriptionDto> GetActiveSubscriptionByTenantAsync(string tenantCode);

    /// <summary>
    /// Retrieves all subscriptions for a specific tenant.
    /// </summary>
    /// <param name="tenantCode">The code of the tenant.</param>
    /// <param name="planCode">(Optional) code of the subscription plan.</param>
    /// <returns>A collection of TenantSubscription DTOs.</returns>
    Task<IEnumerable<TenantSubscriptionDto>> GetSubscriptionsByTenantAsync(string tenantCode, string? planCode = null, SubscriptionStatusEnum? subscriptionStatus = SubscriptionStatusEnum.Active);

    /// <summary>
    /// Retrieves a subscription by its unique identifier.
    /// </summary>
    /// <param name="subscriptionId">The ID of the subscription.</param>
    /// <returns>The TenantSubscription DTO if found; otherwise, null.</returns>
    Task<TenantSubscriptionDto?> GetSubscriptionByIdAsync(Guid subscriptionId);

    /// <summary>
    /// Deletes a TenantSubscription by its ID.
    /// </summary>
    /// <param name="subscriptionId">The ID of the subscription to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteSubscriptionAsync(string tenantCode, string planCode);

    Task<TenantSubscriptionDto> RegisterTenantSubscriptionAsync(TenantSubscriptionDto tenantSubscriptionDto);

    Task DeactivateActiveSubscriptionAsync(string tenantCode);
}
