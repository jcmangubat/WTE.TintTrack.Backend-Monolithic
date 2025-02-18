using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Infrastructure.Repositories;

public class SubscriptionPlanRepository(ApplicationDbContext dbContext)
    : RepositoryForGuidKeyedEntity<SubscriptionPlan>(dbContext), ISubscriptionPlanRepository
{
    /// <summary>
    /// Gets all subscription plans.
    /// </summary>
    /// <returns>A list of subscription plans.</returns>
    public async Task<IEnumerable<SubscriptionPlan>> GetAllAsync()
    {
        return await GetListAsync(p => p.IsActive == true);
    }

    /// <summary>
    /// Finds a subscription plan by its ID.
    /// </summary>
    /// <param name="id">The ID of the subscription plan.</param>
    /// <returns>The subscription plan if found; otherwise, null.</returns>
    public async Task<SubscriptionPlan?> GetByIdAsync(Guid id)
    {
        return await GetSingleAsync(sp => sp.Id == id);
    }

}
