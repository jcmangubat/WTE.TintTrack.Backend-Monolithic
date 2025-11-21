using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Messaging.Core.Requests;
using WTE.TintTrack.Api.Messaging.Core.Responses;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Common.Exceptions;
using WTE.TintTrack.Common.Helpers;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Controllers.Core;

/// <summary>
/// Controller for handling tenant management operations.
/// </summary>
/// <remarks>
/// This controller provides comprehensive tenant management functionality, including tenant registration, approval, validation, updates, and deletion. It supports retrieving tenants by code, by user, resolving tenants from context, managing tenant logos, and performing administrative operations. The controller integrates with user services and tenant services to provide multi-tenant support, enabling organizations to manage their tenant accounts, configure tenant settings, and control tenant access within the system.
/// </remarks>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
//[ApiExplorerSettings(GroupName = "coremodules")]
[Produces(MediaTypeNames.Application.Json)]
public class TenantController(ILogger<TenantController> logger, IMapper mapper,
                                IMessageProviderService messageProviderService,
                                IUserService userService,
                                ITenantService tenantService,

                                IValidator<RegisterTenantRequest> registerTenantRequestValidator,
                                IValidator<TenantLogoImageRequest> tenantLogoImageRequestValidator)
    : LoggingMappedControllerBase<TenantController>(logger, mapper, messageProviderService)
{
    private readonly IMessageProviderService _messageProviderService = messageProviderService;
    private readonly IValidator<RegisterTenantRequest> _registerTenantRequestValidator = registerTenantRequestValidator;
    private readonly IValidator<TenantLogoImageRequest> _tenantLogoImageRequestValidator = tenantLogoImageRequestValidator;

    private readonly IUserService _userService = userService;
    private readonly ITenantService _tenantService = tenantService;

    /// <summary>
    /// Registers a new tenant asynchronously.
    /// </summary>
    /// <param name="registerTenantRequest">The tenant data transfer object containing tenant information.</param>
    /// <returns>The created tenant details if successful, or an error response.</returns>
    [HttpPost]
    [Authorize(Policy = AuthPoliciesEnum.GlobalAdminPolicy)]
    [Route("register")]
    [ProducesResponseType(typeof(DefaultApiResponse<TenantResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationFailureApiResponse<RegisterTenantRequest>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterTenantAsync([FromBody] RegisterTenantRequest registerTenantRequest)
    {
        var validationResult = await _registerTenantRequestValidator.ValidateAsync(registerTenantRequest);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<RegisterTenantRequest>(registerTenantRequest, validationResult);
            return CreateApiResponse(validationResponse);
        }

        var tenantDto = Mapper.Map<TenantDto>(registerTenantRequest);
        tenantDto = await _tenantService.RegisterTenantAsync(tenantDto);

        var successResponse = new DefaultApiResponse<TenantResponse>(Mapper.Map<TenantResponse>(tenantDto));
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Deletes a tenant by their unique identifier.
    /// </summary>
    /// <param name="tenantCode">The code of the tenant.</param>
    /// <returns>A boolean indicating whether the tenant was successfully deleted.</returns>
    [HttpDelete]
    [Authorize(Policy = AuthPoliciesEnum.GlobalAdminPolicy)]
    [Route("{tenantCode}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(DefaultApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAsync(string tenantCode)
    {
        await ValidateUserClaimMembershipInTenantAsync(tenantCode);
        await _tenantService.DeleteAsync(tenantCode);

        var successResponse = new DefaultApiResponse<bool>(true);
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Validates the existence and integrity of a tenant by their tenant code.
    /// </summary>
    /// <param name="tenantCode">The code of the tenant to validate.</param>
    /// <returns>A boolean indicating whether the tenant is valid.</returns>
    [HttpGet]
    [Authorize]
    [Route("validate/{tenantCode}")]
    [ProducesResponseType(typeof(DefaultApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ValidateTenantAsync(string tenantCode)
    {
        await ValidateUserClaimMembershipInTenantAsync(tenantCode);
        var isValid = await _tenantService.ValidateTenantAsync(tenantCode);
        return CreateApiResponse(new DefaultApiResponse<bool>(isValid, null));
    }

    /// <summary>
    /// Updates the details of an existing tenant by code.
    /// </summary>
    /// <param name="tenantCode">The code of the tenant to be updated.</param>
    /// <param name="tenantUpdateRequest">The tenant data transfer object containing updated tenant information.</param>
    /// <returns>A boolean indicating whether the tenant was successfully updated.</returns>
    [HttpPut]
    [Authorize(Policy = AuthPoliciesEnum.TenantOwnerPolicy)]
    [Route("{tenantCode}")]
    [ProducesResponseType(typeof(DefaultApiResponse<dynamic>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync(string tenantCode, [FromBody] TenantUpdateRequest tenantUpdateRequest)
    {
        ValidateTenantCode(tenantCode);
        await ValidateUserClaimMembershipInTenantAsync(tenantCode);

        var existingTenant = await _tenantService.GetTenantByCodeAsync(tenantCode);

        if (tenantUpdateRequest.TenantStatus.HasValue)
            existingTenant.TenantStatus = tenantUpdateRequest.TenantStatus.Value;
        if (tenantUpdateRequest.Description != null)
            existingTenant.Description = tenantUpdateRequest.Description;
        if (tenantUpdateRequest.Domain != null)
            existingTenant.Domain = tenantUpdateRequest.Domain;
        if (tenantUpdateRequest.Name != null)
            existingTenant.Name = tenantUpdateRequest.Name;

        await _tenantService.UpdateAsync(tenantCode, existingTenant);

        var apiMsg = _messageProviderService.GetMessage("ERR031");
        var successResponse = new DefaultApiResponse<dynamic>(new
        {
            tenantCode,
            data = tenantUpdateRequest
        }, apiMsg.Message);

        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Approves a tenant with the specified tenant code.
    /// </summary>
    /// <param name="tenantCode">The unique code identifying the tenant to be approved.</param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
    /// Returns a 200 OK response containing a confirmation message if the tenant is successfully approved.
    /// </returns>
    /// <remarks>
    /// This endpoint approves a tenant identified by the provided tenant code. 
    /// It validates the tenant code and then processes the approval through the tenant service.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{string}"/> containing the tenant code and a success message.
    /// </response>
    /// <response code="401">
    /// Indicates that the request is unauthorized. This endpoint requires the GlobalAdminAccountPolicy to access.
    /// </response>
    /// <response code="400">
    /// Indicates a validation failure for the provided tenant code.
    /// </response>
    [HttpPost]
    [Authorize(Policy = AuthPoliciesEnum.GlobalAdminAccountPolicy)]
    [Route("approve/{tenantCode}")]
    [ProducesResponseType(typeof(DefaultApiResponse<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ApproveTenantAsync(string tenantCode)
    {
        ValidateTenantCode(tenantCode);

        await _tenantService.ApproveTenantAsync(tenantCode);

        var apiMsg = _messageProviderService.GetMessage("ERR032");
        var successResponse = new DefaultApiResponse<string>(tenantCode, apiMsg.Message);

        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Resolves a tenant from the current HTTP context asynchronously.
    /// </summary>
    /// <returns>The tenant details if resolved successfully, or a not found error.</returns>
    [HttpGet]
    [Authorize]
    [Route("resolve")]
    [ProducesResponseType(typeof(DefaultApiResponse<TenantResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ResolveTenantAsync()
    {
        //TODO: Something's off about this
        var tenantDto = await _tenantService.ResolveTenantAsync(HttpContext);
        var tenantResponse = Mapper.Map<TenantResponse>(tenantDto);

        var successResponse = new DefaultApiResponse<TenantResponse>(tenantResponse);
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Gets all tenants asynchronously associated with the currently logged in user.
    /// </summary>
    /// <returns>A list of all tenants or an empty list if none are found.</returns>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(DefaultApiResponse<IEnumerable<TenantResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var claimsInfo = GetUserClaimsInfo();
        var tenantsDto = new List<TenantDto>();

        if (claimsInfo.Roles.Any(role => role.IsRoleInternal()))
            tenantsDto = (await _tenantService.GetAllAsync())?.ToList();
        else tenantsDto = (await _tenantService.GetTenantsByUserEmailAsync(claimsInfo.Email))?.ToList();

        var tenantsResponse = Mapper.Map<IEnumerable<TenantResponse>>(tenantsDto);
        var apiMsg = _messageProviderService.GetMessage("INF000");
        var successResponse = new DefaultApiResponse<IEnumerable<TenantResponse>>(tenantsResponse, apiMsg.Message);

        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Retrieves a tenant by their unique tenant code.
    /// </summary>
    /// <param name="tenantCode">The unique code of the tenant.</param>
    /// <returns>The tenant details if found, or a not found error.</returns>
    [HttpGet]
    [Authorize]
    [Route("code/{tenantCode}")]
    [ProducesResponseType(typeof(DefaultApiResponse<TenantResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTenantByCodeAsync(string tenantCode)
    {
        ValidateTenantCode(tenantCode);
        await ValidateUserClaimMembershipInTenantAsync(tenantCode);

        var tenantDto = await _tenantService.GetTenantByCodeAsync(tenantCode);
        var tenantResponse = Mapper.Map<TenantResponse>(tenantDto);

        var apiMsg = _messageProviderService.GetMessage("INF000");
        var successResponse = new DefaultApiResponse<TenantResponse>(tenantResponse, apiMsg.Message);

        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Gets a list of tenants owned by a specific user asynchronously.
    /// </summary>
    /// <param name="userCode">The code identifier of the user.</param>
    /// <returns>A list of tenants owned by the user or an empty list if none are found.</returns>
    [HttpGet]
    [Authorize]
    [Route("user/{userCode}")]
    [ProducesResponseType(typeof(DefaultApiResponse<IEnumerable<TenantResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTenantsOwnedByUserAsync(string userCode)
    {
        var tenantsDto = await _tenantService.GetTenantsOwnedByUserAsync(userCode);
        var tenantsResponse = Mapper.Map<IEnumerable<TenantResponse>>(tenantsDto);

        var apiMsg = _messageProviderService.GetMessage("INF000");
        var successResponse = new DefaultApiResponse<IEnumerable<TenantResponse>>(tenantsResponse, apiMsg.Message);

        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Gets a list of tenants owned by a specific user to be queried via their email address.
    /// </summary>
    /// <returns>A list of tenants owned by the user or an empty list if none are found.</returns>
    [HttpGet]
    [Authorize]
    [Route("useremail/{email}")]
    [ProducesResponseType(typeof(DefaultApiResponse<IEnumerable<TenantResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceFailureApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTenantsByUserEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new CustomValidationException("Email address is required.");

        var tenants = await _tenantService.GetTenantsByUserEmailAsync(email);
        if (tenants == null || !tenants.Any())
        {
            //return NoContent();
            var apiMsg = _messageProviderService.GetMessage("ERR033");
            var noContentResponse = new ServiceFailureApiResponse<string>(email, apiMsg.Code, apiMsg.Message, statusCode: StatusCodes.Status400BadRequest);
            return CreateApiResponse(noContentResponse);
        }

        var tenantsReponse = Mapper.Map<List<TenantResponse>>(tenants);
        return CreateApiResponse(new DefaultApiResponse<IEnumerable<TenantResponse>>(tenantsReponse, null));
    }

    [Authorize]
    [HttpPost("LogoImage")]
    [ProducesResponseType<DefaultApiResponse<string>>(StatusCodes.Status200OK)]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadLogoImage([FromForm] TenantLogoImageRequest request)
    {
        var validationResult = await _tenantLogoImageRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<TenantLogoImageRequest>(request, validationResult);
            return CreateApiResponse(validationResponse);
        }

        var claimsInfo = GetUserClaimsInfo();
        if (!string.IsNullOrEmpty(request.TenantCode))
        {
            if (!claimsInfo.Roles.Any(r => r.IsRoleInternal() ||
                                        r != UserRolesEnum.TenantOwner ||
                                        r != UserRolesEnum.TenantSystemAdmin))
            {
                var apiMsg = _messageProviderService.GetMessage("ERR034");
                var errors = new List<ValidationFailure>
                {
                    new(nameof(TenantLogoImageRequest.TenantCode), apiMsg.Message )
                };
                var validationResponse = new ValidationFailureApiResponse<TenantLogoImageRequest>(request, new ValidationResult(errors));
                return CreateApiResponse(validationResponse);
            }
        }
        else
        {
            if (string.IsNullOrEmpty(claimsInfo.TenantCode))
            {
                var apiMsg = _messageProviderService.GetMessage("ERR035");
                var validationResponse = new ServiceFailureApiResponse<TenantLogoImageRequest>(request, apiMsg.Message);
                return CreateApiResponse(validationResponse);
            }
            request.TenantCode = claimsInfo.TenantCode;
        }

        var cdnUrl = await _tenantService.UploadLogoImage(request.TenantCode, request.LogoImage);

        var successResponse = new DefaultApiResponse<string>(cdnUrl);
        return CreateApiResponse(successResponse);
    }

    private async Task ValidateUserClaimMembershipInTenantAsync(string tenantCode)
    {
        var claimsInfo = GetUserClaimsInfo();

        if (claimsInfo.Roles.Any(role => role.IsRoleInternal()))
            return;

        if (!claimsInfo.Roles.Any(role => role.IsRoleInternal()) &&
            !await _userService.IsUserMemberOf(claimsInfo.UserCode, tenantCode))
        {
            var apiMsg = _messageProviderService.GetMessage("ERR036");
            throw new ServiceOperationException(apiMsg.Code, apiMsg.Message);
        }
    }
}