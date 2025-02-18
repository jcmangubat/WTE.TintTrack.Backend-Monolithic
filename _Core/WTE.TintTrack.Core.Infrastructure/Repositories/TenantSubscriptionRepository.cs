using Microsoft.EntityFrameworkCore;
using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Infrastructure.Repositories;

public class TenantSubscriptionRepository(ApplicationDbContext dbContext)
    : RepositoryForGuidKeyedEntity<TenantSubscription>(dbContext), ITenantSubscriptionRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    /// <summary>
    /// Gets the active subscription for a specific tenant.
    /// </summary>
    /// <param name="tenantCode">The tenant's code.</param>
    /// <param name="subscriptionStatus">The status of the subscription. Active by default.</param>
    /// <returns>TenantSubscription</returns>
    public async Task<IEnumerable<TenantSubscription>?> GetByTenantAsync(string tenantCode, SubscriptionStatusEnum? subscriptionStatus = null)
    {
        var qry = _dbContext.TenantSubscriptions
            .Include(ts => ts.Tenant)
            .Include(ts => ts.SubscriptionPlan);

        var results = await qry.Where(ts => ts.Tenant.TenantCode == tenantCode &&
                                        (subscriptionStatus == null || subscriptionStatus != null && ts.SubscriptionStatus == subscriptionStatus))
                                .ToListAsync();

        return results;
    }

    /// <summary>
    /// Gets a list of Tenant Subscription by its subscription plan.
    /// </summary>
    /// <param name="subscriptionId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<TenantSubscription>?> GetBySubscriptionPlanAsync(Guid subscriptionId)
    {
        return await _dbContext.TenantSubscriptions
                    .Include(ts => ts.Tenant)
                    .Include(ts => ts.SubscriptionPlan)
                    .Where(ts => ts.Id == subscriptionId)
                    .ToListAsync();
    }

    /// <summary>
    /// Gets a subscription by its unique identifier.
    /// </summary>
    /// <param name="subscriptionId">The subscription's ID.</param>
    /// <returns>The TenantSubscription if found; otherwise, null.</returns>
    public async Task<TenantSubscription?> GetByIdAsync(Guid tenantSubscriptionId)
    {
        return await _dbContext.TenantSubscriptions.Include(ts => ts.SubscriptionPlan)
            .FirstOrDefaultAsync(ts => ts.Id == tenantSubscriptionId);
    }


    /// <summary>
    /// Deletes a TenantSubscription by its ID.
    /// </summary>
    /// <param name="subscriptionId">The subscription's ID.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    public async Task DeleteAsync(Guid subscriptionId)
    {
        var entity = await GetByIdAsync(subscriptionId);
        if (entity != null)
            _dbContext.TenantSubscriptions.Remove(entity);
    }
}