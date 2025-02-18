using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Domain.Interfaces.Repositories;

public interface IUserBillingProfileRepository : IRepositoryForKeyedEntity<UserBillingProfile, Guid>
{
    Task RegisterAsync(UserBillingProfile userBillingProfile);
    Task<UserBillingProfile?> GetByUserAsync(Guid userId);
    Task<UserBillingProfile?> GetByIdAsync(Guid userId);
    Task DeleteAsync(Guid profileId);
}
