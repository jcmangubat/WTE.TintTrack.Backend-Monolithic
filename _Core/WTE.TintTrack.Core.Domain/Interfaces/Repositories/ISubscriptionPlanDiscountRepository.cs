using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Domain.Interfaces.Repositories;

public interface ISubscriptionPlanDiscountRepository : IRepositoryForKeyedEntity<SubscriptionPlanDiscount, Guid>
{
    Task<IEnumerable<SubscriptionPlanDiscount>> GetBySubscriptionPlanAsync(Guid planId);
    Task<SubscriptionPlanDiscount?> GetByIdAsync(Guid discountId);
    Task DeleteAsync(Guid discountId);
}
