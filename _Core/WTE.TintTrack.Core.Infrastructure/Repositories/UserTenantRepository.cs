using Microsoft.EntityFrameworkCore;
using SMEAppHouse.Core.Patterns.EF.Exceptions;
using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Infrastructure.Repositories;

public class UserTenantRepository(ApplicationDbContext dbContext)
    : RepositoryForGuidKeyedEntity<UserTenant>(dbContext), IUserTenantRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task AddUserToTenantAsync(UserTenant userTenant)
    {
        await _dbContext.AddAsync(userTenant);
    }

    public async Task AssignRoleToUserInTenantAsync(Guid userId, Guid tenantId, Guid roleId)
    {
        var userTenant = await GetByUserAndTenantAsync(userId, tenantId);
        var userTenantRole = new UserTenantRole()
        {
            Id = Guid.NewGuid(),
            RoleId = roleId,
            UserTenantId = userTenant.Id
        };

        await _dbContext.UserTenantRoles.AddAsync(userTenantRole);
    }

    public async Task<UserTenant> GetByUserAndTenantAsync(Guid userId, Guid tenantId, bool includeUserTenantRoles = false)
    {
        var query = includeUserTenantRoles ?
                        _dbContext.UserTenants.Include(p => p.UserTenantRoles).AsQueryable() :
                        _dbContext.UserTenants;

        var userTenant = await query.FirstOrDefaultAsync(p => p.UserId == userId && p.TenantId == tenantId)
            ?? throw new EntityNotFoundException<UserTenant>();

        return userTenant;
    }

    public async Task<UserTenant?> GetByEmailAndTenantCodeAsync(string email, string tenantCode)
    {
        var result = await _dbContext.UserTenants
                                .Include(ut => ut.User)
                                .Include(ut => ut.Tenant)
                                .FirstOrDefaultAsync(p => p.User.Email == email && p.Tenant.TenantCode == tenantCode);
        return result;
    }

    public async Task<IEnumerable<UserTenant>> GetTenantsForUserAsync(Guid userId)
    {
        return await _dbContext.UserTenants.Where(p => p.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<UserTenantRole>> GetUserRolesInTenantAsync(Guid userId, Guid tenantId)
    {
        return await _dbContext.UserTenants
                            .Include(p => p.UserTenantRoles)
                            .ThenInclude(p => p.Role)
                            .Include(p => p.Tenant)
                            .Where(p => p.UserId == userId && p.TenantId == tenantId)
                            .SelectMany(p => p.UserTenantRoles)
                            .ToListAsync();
    }

    public async Task<IEnumerable<UserTenant>> GetUsersForTenantAsync(Guid tenantId)
    {
        return await _dbContext.UserTenants.Where(p => p.TenantId == tenantId).ToListAsync();
    }

    public async Task<bool> IsUserInTenantAsync(Guid userId, Guid tenantId)
    {
        return await _dbContext.UserTenants.AnyAsync(p => p.UserId == userId && p.TenantId == tenantId);
    }

    public async Task RemoveRoleFromUserInTenantAsync(Guid userId, Guid tenantId, Guid roleId)
    {
        var userTenant = await GetByUserAndTenantAsync(userId, tenantId, includeUserTenantRoles: true) ??
                                throw new EntityNotFoundException<UserTenant>();

        var userTenantRoles = await _dbContext.UserTenantRoles.Where(p => p.UserTenantId == userTenant.Id).ToListAsync();

        _dbContext.UserTenantRoles.RemoveRange(userTenantRoles);
    }

    public async Task RemoveUserFromTenantAsync(Guid userId, Guid tenantId)
    {
        var userTenant = await _dbContext.UserTenants.FirstOrDefaultAsync(p => p.UserId == userId && p.TenantId == tenantId) ??
                            throw new EntityNotFoundException<UserTenant>();

        _dbContext.UserTenants.Remove(userTenant);
    }

    public async Task UpdateUserTenantAsync(UserTenant userTenant)
    {
        _dbContext.UserTenants.Update(userTenant);

        // Ensures the method is still asynchronous
        await Task.Yield();
    }

    public async Task<bool> UserExistsInTenantAsync(Guid userId, Guid tenantId)
    {
        return await _dbContext.UserTenants.AnyAsync(p => p.UserId == userId && p.TenantId == tenantId);
    }

    public async Task<bool> UserExistsInTenantAsync(Guid userId, string tenantCode)
    {
        return await _dbContext.UserTenants
                                .Include(ut => ut.Tenant)
                                .AnyAsync(p => p.UserId == userId && p.Tenant.TenantCode == tenantCode);
    }
}