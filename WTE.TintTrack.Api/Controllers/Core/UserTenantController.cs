using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Messaging.Core.Request;
using WTE.TintTrack.Api.Messaging.Core.Responses;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Common.Exceptions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Core;

/// <summary>
/// Controller for managing user-tenant associations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
//[ApiExplorerSettings(GroupName = "coremodules")]
[Produces(MediaTypeNames.Application.Json)]
public class UserTenantController(ILogger<UserTenantController> logger, IMapper mapper, IMessageProviderService messageProviderService,
                    IValidator<UserTenantRequest> userTenantRequestValidator,
                    IValidator<UserTenantRoleRequest> userTenantRoleRequestValidator,
                    IUserService userService,
                    ITenantService tenantService,
                    IUserTenantService userTenantService)
    : LoggingMappedControllerBase<UserTenantController>(logger, mapper, messageProviderService)
{
    private readonly IValidator<UserTenantRequest> _userTenantRequestValidator = userTenantRequestValidator;
    private readonly IValidator<UserTenantRoleRequest> _userTenantRoleRequestValidator = userTenantRoleRequestValidator;

    private readonly IUserService _userService = userService;
    private readonly ITenantService _tenantService = tenantService;
    private readonly IUserTenantService _userTenantService = userTenantService;

    /// <summary>
    /// Gets a user-tenant association by user and tenant IDs.
    /// </summary>
    /// <param name="userCode">The code of the user.</param>
    /// <param name="tenantCode">The code of the tenant.</param>
    /// <param name="includeUserTenantRoles">Whether to include user tenant roles.</param>
    /// <returns>A <see cref="Task{UserTenantDto}"/> representing the asynchronous operation.</returns>
    [Authorize]
    [HttpGet("{userCode}/{tenantCode}")]
    [ProducesResponseType<DefaultApiResponse<UserTenantResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserAndTenantAsync(string userCode, string tenantCode, bool includeUserTenantRoles = false)
    {
        var userTenantDto = await _userTenantService.GetByUserAndTenantAsync(userCode, tenantCode, includeUserTenantRoles);
        var userTenantResponse = Mapper.Map<UserTenantResponse>(userTenantDto);

        var successResponse = new DefaultApiResponse<UserTenantResponse>(userTenantResponse);
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Gets a list of tenants associated with a user.
    /// </summary>
    /// <param name="userCode">The code of the user.</param>
    /// <returns>A list of <see cref="UserTenantDto"/> representing the associated tenants.</returns>
    [Authorize]
    [HttpGet("user/{userCode}/tenants")]
    [ProducesResponseType<DefaultApiResponse<IEnumerable<TenantResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTenantsForUserAsync(string userCode)
    {
        var tenantsDto = await _userTenantService.GetTenantsForUserAsync(userCode);
        var tenantsResponse = Mapper.Map<IEnumerable<TenantResponse>>(tenantsDto);

        var successResponse = new DefaultApiResponse<IEnumerable<TenantResponse>>(tenantsResponse);
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Gets a list of users associated with a tenant.
    /// </summary>
    /// <param name="tenantCode">The code of the tenant.</param>
    /// <returns>A list of <see cref="UserTenantDto"/> representing the associated users.</returns>
    [Authorize]
    [HttpGet("tenant/{tenantCode}/users")]
    [ProducesResponseType<IEnumerable<UserResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsersForTenantAsync(string tenantCode)
    {
        var usersDto = await _userTenantService.GetUsersForTenantAsync(tenantCode);
        var usersResponse = Mapper.Map<IEnumerable<UserResponse>>(usersDto);
        var successResponse = new DefaultApiResponse<IEnumerable<UserResponse>>(usersResponse);
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Checks if a user is associated with a tenant.
    /// </summary>
    /// <param name="userCode">The code of the user.</param>
    /// <param name="tenantCode">The code of the tenant.</param>
    /// <returns>A boolean indicating whether the user is in the tenant.</returns>
    [Authorize]
    [HttpGet("user/{userCode}/tenant/{tenantCode}/exists")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    public async Task<IActionResult> IsUserInTenantAsync(string userCode, string tenantCode)
    {
        var exist = await _userTenantService.IsUserInTenantAsync(userCode, tenantCode);
        var successResponse = new DefaultApiResponse<bool>(exist);
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Adds a user to a tenant.
    /// </summary>
    /// <param name="userTenantRequest">The user tenant details.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Authorize(Policy = Consts.AuthPoliciesEnum.TenantSystemAdminPolicy)]
    [HttpPost]
    [ProducesResponseType<ValidationFailureApiResponse<UserTenantRequest>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddUserToTenantAsync([FromBody] UserTenantRequest userTenantRequest)
    {
        var validationResult = await _userTenantRequestValidator.ValidateAsync(userTenantRequest);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<UserTenantRequest>(userTenantRequest, validationResult);
            return CreateApiResponse(validationResponse);
        }

        var userDto = await _userService.GetByUserCodeAsync(userTenantRequest.UserCode)
                            ?? throw new RecordNotFoundException("No user found associated to code provided.");

        var tenantDto = await _tenantService.GetTenantByCodeAsync(userTenantRequest.TenantCode)
                            ?? throw new RecordNotFoundException("No tenant found associated to code provided.");

        UserTenantDto userTenantDto = new()
        {
            Id = Guid.NewGuid(),
            IsActive = true,
            TenantId = tenantDto.Id,
            UserId = userDto.Id,
            IsDefault = userTenantRequest.IsDefault,
            UserIsOwner = userTenantRequest.UserIsOwner
        };
        await _userTenantService.AddUserToTenantAsync(userTenantDto);

        return CreatedAtAction(nameof(GetByUserAndTenantAsync), new { userCode = userTenantRequest.UserCode, tenantCode = userTenantRequest.TenantCode }, userTenantDto);
    }

    /// <summary>
    /// Updates a user tenant association.
    /// </summary>
    /// <param name="userTenantRequest">The user tenant details.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Authorize(Policy = Consts.AuthPoliciesEnum.GlobalAdminPolicy)]
    [HttpPut]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationFailureApiResponse<UserTenantRequest>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUserTenantAsync([FromBody] UserTenantRequest userTenantRequest)
    {
        var validationResult = await _userTenantRequestValidator.ValidateAsync(userTenantRequest);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<UserTenantRequest>(userTenantRequest, validationResult);
            return CreateApiResponse(validationResponse);
        }

        var userTenantDto = Mapper.Map<UserTenantDto>(userTenantRequest);
        var success = await _userTenantService.UpdateUserTenantAsync(userTenantDto);
        return CreateApiResponse(new DefaultApiResponse<bool>(success));
    }

    /// <summary>
    /// Removes a user from a tenant. Error is thrown when action attempts to remove a user that is ownder of the tenant.
    /// </summary>
    /// <param name="userCode">The code of the user.</param>
    /// <param name="tenantCode">The code of the tenant.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Authorize(Policy = Consts.AuthPoliciesEnum.TenantOwnerPolicy)]
    [HttpDelete("{userCode}/{tenantCode}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveUserFromTenantAsync(string userCode, string tenantCode)
    {
        var success = await _userTenantService.RemoveUserFromTenantAsync(userCode, tenantCode);
        return CreateApiResponse(new DefaultApiResponse<bool>(success));
    }

    /// <summary>
    /// Gets the roles of a user in a tenant.
    /// </summary>
    /// <param name="userCode">The code of the user.</param>
    /// <param name="tenantCode">The code of the tenant.</param>
    /// <returns>A list of <see cref="UserTenantRoleDto"/> representing the user roles in the tenant.</returns>
    [Authorize]
    [HttpGet("user/{userCode}/tenant/{tenantCode}/roles")]
    [ProducesResponseType<IEnumerable<UserTenantRoleResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserRolesInTenantAsync(string userCode, string tenantCode)
    {
        IEnumerable<UserTenantRoleDto> userTenantRolesDto = await _userTenantService.GetUserRolesInTenantAsync(userCode, tenantCode);
        var rolesResponse = Mapper.Map<IEnumerable<UserTenantRoleResponse>>(userTenantRolesDto);
        return CreateApiResponse(new DefaultApiResponse<IEnumerable<UserTenantRoleResponse>>(rolesResponse));
    }

    /// <summary>
    /// Assigns a role to a user in a tenant.
    /// </summary>
    /// <param name="userTenantRoleRequest">The values containing the request.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [HttpPost("roles")]
    [Authorize(Policy = Consts.AuthPoliciesEnum.TenantSystemAdminPolicy)]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationFailureApiResponse<UserTenantRoleRequest>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AssignRoleToUserInTenantAsync([FromBody] UserTenantRoleRequest userTenantRoleRequest)
    {
        var validationResult = await _userTenantRoleRequestValidator.ValidateAsync(userTenantRoleRequest);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<UserTenantRoleRequest>(userTenantRoleRequest, validationResult);
            return CreateApiResponse(validationResponse);
        }

        var success = await _userTenantService.AssignRoleToUserInTenantAsync(userTenantRoleRequest.UserCode, userTenantRoleRequest.TenantCode, userTenantRoleRequest.RoleName);
        return CreateApiResponse(new DefaultApiResponse<bool>(success));
    }

    /// <summary>
    /// Removes a role from a user in a tenant.
    /// </summary>
    /// <param name="userTenantRoleRequest">The values containing the request.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [HttpDelete("roles")]
    [Authorize(Policy = Consts.AuthPoliciesEnum.TenantSystemAdminPolicy)]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationFailureApiResponse<UserTenantRoleRequest>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveRoleFromUserInTenantAsync([FromBody] UserTenantRoleRequest userTenantRoleRequest)
    {
        var validationResult = await _userTenantRoleRequestValidator.ValidateAsync(userTenantRoleRequest);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<UserTenantRoleRequest>(userTenantRoleRequest, validationResult);
            return CreateApiResponse(validationResponse);
        }

        var success = await _userTenantService.RemoveRoleFromUserInTenantAsync(userTenantRoleRequest.UserCode, userTenantRoleRequest.TenantCode, userTenantRoleRequest.RoleName);
        return CreateApiResponse(new DefaultApiResponse<bool>(success));
    }
}
