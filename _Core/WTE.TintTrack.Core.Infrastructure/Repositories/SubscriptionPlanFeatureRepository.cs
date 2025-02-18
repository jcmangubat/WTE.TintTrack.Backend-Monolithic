using Microsoft.EntityFrameworkCore;
using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Infrastructure.Repositories;

public class SubscriptionPlanFeatureRepository(ApplicationDbContext dbContext)
    : RepositoryForGuidKeyedEntity<SubscriptionPlanFeature>(dbContext), ISubscriptionPlanFeatureRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<IEnumerable<SubscriptionPlanFeature>> GetBySubscriptionPlanAsync(Guid subscriptionPlanId)
    {
        var results = await _dbContext.SubscriptionPlanFeatures
            .Include(p => p.SubscriptionPlanFeatureAssociations).ThenInclude(p => p.SubscriptionPlan)
            .Where(f => f.SubscriptionPlanFeatureAssociations.Any(x => x.SubscriptionPlan.Id == subscriptionPlanId)).ToListAsync();

        return results;
    }

    public async Task<SubscriptionPlanFeature?> GetByFeatureIdAsync(Guid featureId)
    {
        return await _dbContext.SubscriptionPlanFeatures
            .FirstOrDefaultAsync(f => f.Id == featureId);
    }
}
