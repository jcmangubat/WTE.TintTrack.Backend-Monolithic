using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

namespace WTE.TintTrack.Core.Application.Interfaces;

/// <summary>
/// Provides functionality to manage user-tenant relationships and roles.
/// </summary>
public interface IUserTenantService : IMappedLoggingService<IUserTenantService>
{
    /// <summary>
    /// Retrieves a specific user-tenant association based on the user code and tenant code.
    /// </summary>
    /// <param name="userCode">The unique identifier for the user.</param>
    /// <param name="tenantCode">The unique identifier for the tenant.</param>
    /// <param name="includeUserTenantRoles">Indicates whether to include the user's roles within the tenant.</param>
    /// <returns>A <see cref="UserTenantDto"/> representing the association, or null if not found.</returns>
    Task<UserTenantDto?> GetByUserAndTenantAsync(string userCode, string tenantCode, bool includeUserTenantRoles = false);

    /// <summary>
    /// Retrieves all tenants associated with a specific user.
    /// </summary>
    /// <param name="userCode">The unique identifier for the user.</param>
    /// <returns>A collection of <see cref="UserTenantDto"/> representing the user's tenants.</returns>
    Task<IEnumerable<UserTenantDto>> GetTenantsForUserAsync(string userCode);

    /// <summary>
    /// Retrieves all users associated with a specific tenant.
    /// </summary>
    /// <param name="tenantCode">The unique identifier for the tenant.</param>
    /// <returns>A collection of <see cref="UserTenantDto"/> representing the users in the tenant.</returns>
    Task<IEnumerable<UserTenantDto>> GetUsersForTenantAsync(string tenantCode);

    /// <summary>
    /// Checks if a user is associated with a specific tenant.
    /// </summary>
    /// <param name="userCode">The unique identifier for the user.</param>
    /// <param name="tenantCode">The unique identifier for the tenant.</param>
    /// <returns>True if the user is in the tenant; otherwise, false.</returns>
    Task<bool> IsUserInTenantAsync(string userCode, string tenantCode);

    /// <summary>
    /// Associates a user with a tenant.
    /// </summary>
    /// <param name="userTenant">The <see cref="UserTenantDto"/> containing details of the user-tenant association.</param>
    Task AddUserToTenantAsync(UserTenantDto userTenant);

    /// <summary>
    /// Removes a user from a specific tenant.
    /// </summary>
    /// <param name="userCode">The unique identifier for the user.</param>
    /// <param name="tenantCode">The unique identifier for the tenant.</param>
    Task<bool> RemoveUserFromTenantAsync(string userCode, string tenantCode);

    /// <summary>
    /// Updates details of a user-tenant association.
    /// </summary>
    /// <param name="userTenant">The <see cref="UserTenantDto"/> with updated details for the user-tenant association.</param>
    Task<bool> UpdateUserTenantAsync(UserTenantDto userTenant);

    /// <summary>
    /// Retrieves the roles assigned to a user within a specific tenant.
    /// </summary>
    /// <param name="userCode">The unique identifier for the user.</param>
    /// <param name="tenantCode">The unique identifier for the tenant.</param>
    /// <returns>A collection of <see cref="UserTenantRoleDto"/> representing the user's roles in the tenant.</returns>
    Task<IEnumerable<UserTenantRoleDto>> GetUserRolesInTenantAsync(string userCode, string tenantCode);

    /// <summary>
    /// Assigns roles to a user within a specific tenant.
    /// </summary>
    /// <param name="userCode">The unique identifier for the user.</param>
    /// <param name="tenantCode">The unique identifier for the tenant.</param>
    /// <param name="userRole">The role to assign, specified by <see cref="Consts.UserRolesEnum"/>.</param>
    Task AssignRolesToUserInTenantAsync(string userCode, string tenantCode, string[] userRoles);


    /// <summary>
    /// Assigns a specified role to a user within a given tenant.
    /// </summary>
    /// <param name="userCode">The unique code identifying the user.</param>
    /// <param name="tenantCode">The unique code identifying the tenant.</param>
    /// <param name="userRole">The name of the role to assign to the user within the tenant.</param>
    /// <returns>A task representing the asynchronous operation of assigning the role to the user.</returns>
    /// <remarks>
    /// Use this method to manage user permissions by assigning a specific role to a user within a multi-tenant environment.
    /// If the user already possesses the specified role, this operation may be idempotent, depending on the implementation.
    /// </remarks>
    Task<bool> AssignRoleToUserInTenantAsync(string userCode, string tenantCode, string userRole);

    /// <summary>
    /// Assigns a specified role to a user within a given tenant.
    /// </summary>
    /// <param name="userCode">The unique code identifying the user.</param>
    /// <param name="tenantCode">The unique code identifying the tenant.</param>
    /// <param name="userRole">The role to assign to the user, represented by an enumeration of user roles.</param>
    /// <returns>A task representing the asynchronous operation of assigning the role to the user.</returns>
    /// <remarks>
    /// Use this method to add specific roles to a user in a multi-tenant environment. 
    /// Roles determine user permissions and access within the tenant. If the user already has the specified role,
    /// this operation may be idempotent, depending on the implementation.
    /// </remarks>
    Task<bool> AssignRoleToUserInTenantAsync(string userCode, string tenantCode, Consts.UserRolesEnum userRole);

    /// <summary>
    /// Removes a specific role from a user within a specific tenant.
    /// </summary>
    /// <param name="userCode">The unique identifier for the user.</param>
    /// <param name="tenantCode">The unique identifier for the tenant.</param>
    /// <param name="userRole">The role to remove, specified by <see cref="Consts.UserRolesEnum"/>.</param>
    Task<bool> RemoveRoleFromUserInTenantAsync(string userCode, string tenantCode, Consts.UserRolesEnum userRole);

    /// <summary>
    /// Removes a specified role from a user within a given tenant.
    /// </summary>
    /// <param name="userCode">The unique code identifying the user.</param>
    /// <param name="tenantCode">The unique code identifying the tenant.</param>
    /// <param name="roleName">The name of the role to be removed from the user in the tenant.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This operation ensures that the specified role is disassociated from the user within the context of the specified tenant.
    /// </remarks>
    Task<bool> RemoveRoleFromUserInTenantAsync(string userCode, string tenantCode, string roleName);

    /// <summary>
    /// Removes one or more specified roles from a user within a given tenant.
    /// </summary>
    /// <param name="userCode">The unique code identifying the user.</param>
    /// <param name="tenantCode">The unique code identifying the tenant.</param>
    /// <param name="userRoles">An array of role names to be removed from the user.</param>
    /// <returns>A task representing the asynchronous operation of removing the specified roles from the user.</returns>
    /// <remarks>
    /// This method can be used to manage user permissions in a multi-tenant environment by removing roles that are no longer
    /// applicable to a user within the specified tenant. If a role does not exist for the user, it will be ignored in the operation.
    /// </remarks>
    Task<bool> RemoveRolesToUserInTenantAsync(string userCode, string tenantCode, string[] userRoles);
}
