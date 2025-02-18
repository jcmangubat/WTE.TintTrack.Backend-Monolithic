using Microsoft.EntityFrameworkCore;
using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Infrastructure.Repositories;

public class TenantRepository(ApplicationDbContext dbContext)
    : RepositoryForGuidKeyedEntity<Tenant>(dbContext), ITenantRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Tenant?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Tenants.FindAsync(id);
    }

    public async Task<Tenant?> GetByTenantCodeAsync(string tenantCode)
    {
        return await _dbContext.Tenants.FirstOrDefaultAsync(p => p.TenantCode == tenantCode);
    }

    public async Task<IEnumerable<Tenant>> GetByIdsAsync(IEnumerable<Guid> tenantIds)
    {
        return await _dbContext.Tenants.Where(t => tenantIds.Contains(t.Id)).ToListAsync();
    }

    public async Task<IEnumerable<Tenant>> GetTenantsForUserAsync(Guid userId, bool? activeOnly = null)
    {
        return await _dbContext.UserTenants
                            .Include(ut => ut.Tenant)
                            .Where(ut => ut.UserId == userId &&
                                            (activeOnly == null ||
                                                activeOnly.HasValue && activeOnly.Value && (ut.Tenant.IsActive ?? false) ||
                                                activeOnly.HasValue && !activeOnly.Value && !(ut.Tenant.IsActive ?? false)
                                            )
                                        )
                            .Select(ut => ut.Tenant)
                            .ToListAsync();
    }


    public async Task<IEnumerable<Tenant>> GetTenantsForUserEmailAddressAsync(string emailAddress)
    {
        var result = await _dbContext.Users.Include(u => u.UserTenants)
                                            .ThenInclude(ut => ut.Tenant)
                                            .Where(u => u.Email == emailAddress)
                                            .SelectMany(p => p.UserTenants.Select(ut => ut.Tenant))
                                            .Distinct()
                                            .ToListAsync();
        return result;
    }

}
