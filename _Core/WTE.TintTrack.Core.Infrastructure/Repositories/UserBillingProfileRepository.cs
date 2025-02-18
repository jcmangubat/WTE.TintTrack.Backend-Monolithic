using Microsoft.EntityFrameworkCore;
using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Infrastructure.Repositories;

public class UserBillingProfileRepository(ApplicationDbContext dbContext)
    : RepositoryForGuidKeyedEntity<UserBillingProfile>(dbContext), IUserBillingProfileRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task RegisterAsync(UserBillingProfile userBillingProfile)
    {
        await _dbContext.UserBillingProfiles.AddAsync(userBillingProfile);
    }

    public async Task<UserBillingProfile?> GetByUserAsync(Guid userId)
    {
        return await _dbContext.UserBillingProfiles
            .FirstOrDefaultAsync(up => up.UserId == userId);
    }

    public async Task<UserBillingProfile?> GetByIdAsync(Guid userId)
    {
        return await _dbContext.UserBillingProfiles
            .FirstOrDefaultAsync(up => up.Id == userId);
    }

    public async Task DeleteAsync(Guid profileId)
    {
        var entity = await GetByIdAsync(profileId);
        if (entity != null)
            _dbContext.UserBillingProfiles.Remove(entity);
    }
}
