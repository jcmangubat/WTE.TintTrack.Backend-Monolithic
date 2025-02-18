using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Core.Application.Interfaces;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Controllers.Core;

/// <summary>
/// Controller for managing and checking permissions for roles.
/// </summary>
[ApiController]
[Authorize(Policy = AuthPoliciesEnum.GlobalAdminPolicy)]
[Route("api/permissions")]
public class PermissionsController(ILogger<PermissionsController> logger, IMapper mapper, IMessageProviderService messageProviderService,
IRolePermissionService rolePermissionService)
    : LoggingMappedControllerBase<PermissionsController>(logger, mapper, messageProviderService)
{
    private readonly IRolePermissionService _rolePermissionService = rolePermissionService;

    /// <summary>
    /// Retrieves the list of roles associated with a specific permission.
    /// </summary>
    /// <param name="permission">The permission name to fetch associated roles for.</param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
    /// Returns a 200 OK response containing the list of roles associated with the permission.
    /// </returns>
    /// <remarks>
    /// This endpoint retrieves the roles that have access to a specific permission.
    /// It queries the service for the roles linked with the given permission.
    /// </remarks>
    /// <response code="200">
    /// Returns a list of roles associated with the specified permission.
    /// </response>
    [HttpGet("{permission}/roles")]
    [ProducesResponseType(typeof(DefaultApiResponse<IEnumerable<string>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRolesForPermission(string permission)
    {
        var roles = await _rolePermissionService.GetRolesForPermissionAsync(permission);
        return CreateApiResponse(new DefaultApiResponse<IEnumerable<string>>(roles, "Success"));
    }

    /// <summary>
    /// Retrieves the list of permissions associated with the specified roles.
    /// </summary>
    /// <param name="roles">A collection of role names to fetch associated permissions for.</param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
    /// Returns a 200 OK response containing the list of permissions associated with the roles.
    /// </returns>
    /// <remarks>
    /// This endpoint retrieves the permissions that are assigned to the specified roles.
    /// The service returns a collection of permissions corresponding to the provided roles.
    /// </remarks>
    /// <response code="200">
    /// Returns a list of permissions for the specified roles.
    /// </response>
    [HttpGet("roles")]
    [ProducesResponseType(typeof(DefaultApiResponse<IEnumerable<string>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPermissionsForRolesAsync([FromBody] IEnumerable<string> roles)
    {
        var permissions = await _rolePermissionService.GetPermissionsForRolesAsync(roles);
        return CreateApiResponse(new DefaultApiResponse<IEnumerable<string>>(permissions, "Success"));
    }

    /// <summary>
    /// Updates the roles associated with a specific permission.
    /// </summary>
    /// <param name="permissionName">The permission name for which roles need to be updated.</param>
    /// <param name="roles">A collection of role names to assign to the permission.</param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
    /// Returns a 204 No Content response after successfully updating the roles for the permission.
    /// </returns>
    /// <remarks>
    /// This endpoint allows you to update the roles that are associated with a permission.
    /// The provided roles will be linked to the specified permission in the system.
    /// </remarks>
    /// <response code="204">
    /// Indicates that the roles for the specified permission were updated successfully.
    /// </response>
    [HttpPut]
    [ProducesResponseType(typeof(DefaultApiResponse<dynamic>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdatePermissionRoles(string permissionName, [FromBody] IEnumerable<string> roles)
    {
        var permParts = permissionName.Split('.', StringSplitOptions.RemoveEmptyEntries);
        var failures = new List<ValidationFailure>();

        FeaturesEnum feature = default;
        FeatureAccessPermissionsEnum permissionLevel = default;

        if (permParts.Length != 2)
            failures.Add(new ValidationFailure(nameof(permissionName), "Invalid format analysing permissionName param."));
        else
        {
            if (!Enum.TryParse(permParts[0], out feature))
                failures.Add(new ValidationFailure(permParts[0], $"Invalid feature specified in permissionName param: {permParts[0]}"));

            if (!Enum.TryParse(permParts[1], out permissionLevel))
                failures.Add(new ValidationFailure(permParts[0], $"Invalid permission level specified in permissionName param: {permParts[1]}"));
        }

        if (failures.Count != 0)
        {
            var failureResponse = new ValidationFailureApiResponse<string>(permissionName, new ValidationResult(failures), "Validation failure encountered.");
            return CreateApiResponse(failureResponse);
        }

        await _rolePermissionService.UpdatePermissionsAsync(feature, permissionLevel, roles);
        return CreateApiResponse(new DefaultApiResponse<dynamic>(new
        {
            permissionName,
            feature = feature.ToString(),
            permissionLevel = permissionLevel.ToString()
        }, "Success"));
    }

    /// <summary>
    /// Checks if the specified roles have access to a particular permission.
    /// </summary>
    /// <param name="permission">The permission name to check access for.</param>
    /// <param name="roles">A collection of role names to check for permission access.</param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
    /// Returns a 200 OK response with a flag indicating whether access is granted.
    /// </returns>
    /// <remarks>
    /// This endpoint checks if the provided roles have the necessary permission.
    /// It queries the service to verify if the roles are permitted for the specified permission.
    /// </remarks>
    /// <response code="200">
    /// Returns an object indicating whether access is granted or denied for the specified roles and permission.
    /// </response>
    [HttpGet("check")]
    [ProducesResponseType(typeof(DefaultApiResponse<dynamic>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CheckAccess(string permission, [FromBody] IEnumerable<string> roles)
    {
        var hasAccess = await _rolePermissionService.HasPermissionAsync(roles, permission);
        return CreateApiResponse(new DefaultApiResponse<dynamic>(new { accessGranted = hasAccess }, "Success"));
    }
}