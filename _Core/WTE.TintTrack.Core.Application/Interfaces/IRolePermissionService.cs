using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Application.Interfaces;

/// <summary>
/// Defines the contract for managing role-based permissions within the application.
/// Provides methods for retrieving, checking, and updating permissions associated with roles.
/// </summary>
public interface IRolePermissionService
{
    /// <summary>
    /// Retrieves a list of permissions assigned to the specified roles.
    /// </summary>
    /// <param name="roles">A collection of role names for which permissions are to be retrieved.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of permissions associated with the specified roles.</returns>
    Task<IEnumerable<string>> GetPermissionsForRolesAsync(IEnumerable<string> roles);

    /// <summary>
    /// Retrieves a list of roles associated with the permission.
    /// </summary>
    /// <param name="roles">The permission given to a role.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of roles associated with the specified permission.</returns>
    Task<IEnumerable<string>> GetRolesForPermissionAsync(string permission);

    /// <summary>
    /// Checks if any of the specified roles have the given permission.
    /// </summary>
    /// <param name="roles">A collection of role names to check against the specified permission.</param>
    /// <param name="permission">The permission to check for.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether any of the specified roles have the given permission.</returns>
    Task<bool> HasPermissionAsync(IEnumerable<string> roles, string permission);

    Task UpdatePermissionsAsync(FeaturesEnum feature, FeatureAccessPermissionsEnum permissionLevel, IEnumerable<string> roles);
}
