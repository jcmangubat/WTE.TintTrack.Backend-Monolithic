using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Domain.Interfaces.Repositories;

public interface ISubscriptionPlanFeatureRepository : IRepositoryForKeyedEntity<SubscriptionPlanFeature, Guid>
{
    Task<IEnumerable<SubscriptionPlanFeature>> GetBySubscriptionPlanAsync(Guid subscriptionPlanId);
    Task<SubscriptionPlanFeature?> GetByFeatureIdAsync(Guid featureId);
    Task DeleteAsync(Guid featureId);
}
