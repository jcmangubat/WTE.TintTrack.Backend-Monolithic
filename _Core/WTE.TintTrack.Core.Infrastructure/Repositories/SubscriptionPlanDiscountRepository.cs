using Microsoft.EntityFrameworkCore;
using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Infrastructure.Repositories;

public class SubscriptionPlanDiscountRepository(ApplicationDbContext dbContext)
    : RepositoryForGuidKeyedEntity<SubscriptionPlanDiscount>(dbContext), ISubscriptionPlanDiscountRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<IEnumerable<SubscriptionPlanDiscount>> GetBySubscriptionPlanAsync(Guid planId)
    {
        return await _dbContext.SubscriptionPlanDiscounts
            .Where(d => d.SubscriptionPlanId == planId)
            .ToListAsync();
    }

    public async Task<SubscriptionPlanDiscount?> GetByIdAsync(Guid discountId)
    {
        return await _dbContext.SubscriptionPlanDiscounts
            .FirstOrDefaultAsync(d => d.Id == discountId);
    }

    public async Task DeleteAsync(Guid discountId)
    {
        var entity = await GetByIdAsync(discountId);
        if (entity != null)
            _dbContext.SubscriptionPlanDiscounts.Remove(entity);
    }
}
