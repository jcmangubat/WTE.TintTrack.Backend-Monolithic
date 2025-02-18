using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Domain.Interfaces.Repositories;

public interface IUserTenantRepository
    : IRepositoryForKeyedEntity<UserTenant, Guid>
{
    // Get a UserTenant by User ID and Tenant ID
    Task<UserTenant> GetByUserAndTenantAsync(Guid userId, Guid tenantId, bool includeUserTenantRoles = false);

    Task<UserTenant?> GetByEmailAndTenantCodeAsync(string email, string tenantCode);

    // Get all tenants for a specific user
    Task<IEnumerable<UserTenant>> GetTenantsForUserAsync(Guid userId);

    // Get all users for a specific tenant
    Task<IEnumerable<UserTenant>> GetUsersForTenantAsync(Guid tenantId);

    // Check if a user belongs to a specific tenant
    Task<bool> IsUserInTenantAsync(Guid userId, Guid tenantId);

    // Add a user to a tenant
    Task AddUserToTenantAsync(UserTenant userTenant);

    // Remove a user from a tenant
    Task RemoveUserFromTenantAsync(Guid userId, Guid tenantId);

    // Update the user-tenant relationship
    Task UpdateUserTenantAsync(UserTenant userTenant);

    // Get all roles for a specific user in a tenant
    Task<IEnumerable<UserTenantRole>> GetUserRolesInTenantAsync(Guid userId, Guid tenantId);

    // Assign a role to a user in a tenant
    Task AssignRoleToUserInTenantAsync(Guid userId, Guid tenantId, Guid roleId);

    // Remove a role from a user in a tenant
    Task RemoveRoleFromUserInTenantAsync(Guid userId, Guid tenantId, Guid roleId);
    Task<bool> UserExistsInTenantAsync(Guid userId, Guid tenantId);
    Task<bool> UserExistsInTenantAsync(Guid userId, string tenantCode);
}