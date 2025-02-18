using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Domain.Interfaces.Repositories;

public interface ITenantRepository : IRepositoryForKeyedEntity<Tenant, Guid>
{
    Task<Tenant?> GetByIdAsync(Guid id);
    Task<Tenant?> GetByTenantCodeAsync(string tenantCode);
    Task<IEnumerable<Tenant>> GetByIdsAsync(IEnumerable<Guid> tenantIds);
    Task<IEnumerable<Tenant>> GetTenantsForUserAsync(Guid userId, bool? activeOnly = null);
    Task<IEnumerable<Tenant>> GetTenantsForUserEmailAddressAsync(string emailAddress);
}
