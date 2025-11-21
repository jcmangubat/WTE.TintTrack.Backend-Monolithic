using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SMEAppHouse.Core.CodeKits;
using SMEAppHouse.Core.CodeKits.Extensions;
using System.Data;
using System.Net.Mime;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Messaging.Core.Requests;
using WTE.TintTrack.Api.Messaging.Core.Responses;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Common.Helpers;
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Core.Application.DTOs;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Controllers.Core;

/// <summary>
/// Controller for handling user account operations.
/// </summary>
/// <remarks>
/// This controller provides comprehensive user account management functionality, including user registration, profile management, email confirmation, tenant joining, and user administration. It supports retrieving user profiles by code or email, updating user information, managing user-tenants associations, uploading profile images, and performing administrative operations such as user deletion and role management. The controller integrates with user services, tenant services, and messaging services to provide a complete account management solution.
/// </remarks>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
//[ApiExplorerSettings(GroupName = "coremodules")]
[Produces(MediaTypeNames.Application.Json)]
public class AccountController(ILogger<AccountController> logger, IMapper mapper,
                                IMessageProviderService messageProviderService,
                                IOptions<ApplicationSettings> appSettings,
                                IValidator<UserRegisterRequest> registerUserValidator,
                                IValidator<UpdateUserProfileRequest> updateUserProfileRequestValidator,
                                IValidator<UserProfileImageRequest> userProfileImageRequestValidator,
                                IUserService userService)
        : LoggingMappedControllerBase<AccountController>(logger, mapper, messageProviderService)
{
    private readonly ApplicationSettings _appSettings = appSettings.Value;
    private readonly IMessageProviderService _messageProviderService = messageProviderService;
    private readonly IValidator<UserRegisterRequest> _registerUserValidator = registerUserValidator;
    private readonly IValidator<UpdateUserProfileRequest> _updateUserProfileRequestValidator = updateUserProfileRequestValidator;
    private readonly IValidator<UserProfileImageRequest> _userProfileImageRequestValidator = userProfileImageRequestValidator;

    private readonly IUserService _userService = userService;

    /// <summary>
    /// Retrieves a user profile based on the specified user code.
    /// </summary>
    /// <param name="userCode">A unique code identifying the user.</param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
    /// Returns a 200 OK response containing the user's profile information if the user is found.
    /// </returns>
    /// <remarks>
    /// This endpoint fetches a user's profile data by their unique user code.
    /// The user profile information is mapped to a <see cref="UserResponse"/> object and returned in the response.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{UserResponse}"/> containing the user profile details.
    /// </response>
    [HttpGet("{userCode}")]
    [Authorize]
    [ProducesResponseType(typeof(DefaultApiResponse<UserResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserByCode(string userCode)
    {
        var user = await _userService.GetByUserCodeAsync(userCode);

        var successResponse = new DefaultApiResponse<UserResponse>(Mapper.Map<UserResponse>(user));
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Retrieves a user profile based on the specified email address.
    /// </summary>
    /// <param name="email">The email address associated with the user.</param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
    /// Returns a 200 OK response containing the user's profile information if the user is found.
    /// </returns>
    /// <remarks>
    /// This endpoint fetches a user's profile data by their email address.
    /// The user profile information is mapped to a <see cref="UserResponse"/> object and returned in the response.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{UserResponse}"/> containing the user profile details.
    /// </response>
    [HttpGet("email")]
    [Authorize]
    [ProducesResponseType(typeof(DefaultApiResponse<UserResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await _userService.GetByEmailAsync(email);

        var successResponse = new DefaultApiResponse<UserResponse>(Mapper.Map<UserResponse>(user));
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Registers a new user based on the provided registration data.
    /// </summary>
    /// <param name="request">
    /// A <see cref="UserRegisterRequest"/> object containing the user's registration details, including required fields like
    /// username, password, and email.
    /// </param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the result of the asynchronous operation, returning a 200 OK response 
    /// upon successful registration with a confirmation message in the response body.
    /// </returns>
    /// <remarks>
    /// This endpoint is designed for user registration, requiring a complete and valid set of registration data.
    /// Validation of the registration data occurs before processing the request.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{T}"/> with a success message if the registration completes successfully.
    /// </response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(DefaultApiResponse<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequest request)
    {
        request.Email = request.Email.Trim();
        request.Password = request.Password.Trim();
        request.TenantCode = (request.TenantCode ?? string.Empty).Trim();
        request.TenantName = (request.TenantName ?? string.Empty).Trim();

        var validationResult = await _registerUserValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<UserRegisterRequest>(request, validationResult);
            return CreateApiResponse(validationResponse);
        }

        var userDto = Mapper.Map<ApplicationUserDto>(request);

        var tenantEntry = new TenantEntryDto()
        {
            TenantCode = request.TenantCode,
            TenantName = request.TenantName
        };

        var regUserResult = await _userService.RegisterUserAsync(userDto, tenantEntry, request.Password);

        var success = await _userService.SendEmailConfirmationAsync(regUserResult.User, Request);
        if (!success)
            return CreateApiResponse(new ServiceFailureApiResponse<UserRegisterRequest>(request, "Failed to send email confirmation."));

        return CreateApiResponse(new DefaultApiResponse<dynamic>(new
        {
            User = Mapper.Map<UserResponse>(regUserResult.User),
            Tenant = Mapper.Map<TenantResponse>(regUserResult.Tenant)
        }, "Success"));
    }

    /// <summary>
    /// Allows a user to join a tenant specified by the provided tenant code.
    /// </summary>
    /// <remarks>
    /// This endpoint is used by authenticated users to join a tenant within the system. The user's code
    /// is retrieved from the current user's claims, and the tenant code is provided in the request body.
    /// </remarks>
    /// <param name="tenantCode">The unique code identifying the tenant that the user wants to join.</param>
    /// <returns>
    /// Returns an <see cref="IActionResult"/> with a <see cref="DefaultApiResponse{T}"/> containing a success message
    /// and a 200 OK status if the user was successfully joined to the tenant.
    /// </returns>
    /// <response code="200">The user has successfully joined the specified tenant.</response>
    /// <response code="401">The user is unauthorized to perform this action.</response>
    /// <response code="400">The request is invalid or missing required information.</response>
    [Authorize]
    [HttpPost("join-tenant")]
    [ProducesResponseType(typeof(DefaultApiResponse<dynamic>), StatusCodes.Status200OK)]
    public async Task<IActionResult> JoinTenant([FromBody] string tenantCode)
    {
        var claimsInfo = GetUserClaimsInfo();
        await _userService.JoinUserToATenantAsync(claimsInfo.UserCode, tenantCode);
        return CreateApiResponse(new DefaultApiResponse<dynamic>(new
        {
            userCode = claimsInfo.UserCode,
            tenantCode
        }, $"Request to join tenant {tenantCode} was successfully sent."));
    }

    /// <summary>
    /// Initiates an email confirmation request for the specified email address.
    /// </summary>
    /// <param name="email">
    /// A <see cref="string"/> representing the email address to which the confirmation email will be sent.
    /// </param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the result of the asynchronous operation.
    /// Returns a 200 OK response if the email confirmation request is processed successfully, 
    /// or an error response if issues are encountered.
    /// </returns>
    /// <remarks>
    /// This endpoint is used to request a confirmation email for verifying an account associated with the specified email address.
    /// It checks for the email's existence and may return a conflict or bad request status if issues are detected.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{T}"/> with a success message if the email confirmation request is successful.
    /// </response>
    /// <response code="400">
    /// Returns a <see cref="ServiceFailureApiResponse{T}"/> if the request fails due to a bad request.
    /// </response>
    /// <response code="409">
    /// Returns a <see cref="ServiceFailureApiResponse{T}"/> if a conflict is detected, such as if the email is already confirmed.
    /// </response>
    [HttpPost("request-email-confirmation")]
    [ProducesResponseType(typeof(DefaultApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceFailureApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceFailureApiResponse<string>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RequestEmailConfirmation([FromBody] string email)
    {
        if (!CodeKit.IsValidEmail(email))
        {
            var apiMsg = _messageProviderService.GetMessage("ERR023");
            var failureResponse = new ServiceFailureApiResponse<string>(email, apiMsg.Code, apiMsg.Message, statusCode: StatusCodes.Status400BadRequest);
            return CreateApiResponse(failureResponse);
        }

        // Get the user by email
        var userDto = await _userService.GetByEmailAsync(email);
        if (userDto == null)
        {
            var apiMsg = _messageProviderService.GetMessage("ERR024");
            var failureResponse = new ServiceFailureApiResponse<string>(email, apiMsg.Code, apiMsg.Message, statusCode: StatusCodes.Status400BadRequest);
            return CreateApiResponse(failureResponse);
        }
        else if (userDto.EmailConfirmed)
        {
            var apiMsg = _messageProviderService.GetMessage("ERR025");
            var failureResponse = new ServiceFailureApiResponse<string>(email, apiMsg.Code, apiMsg.Message, statusCode: StatusCodes.Status409Conflict);
            return CreateApiResponse(failureResponse);
        }

        // Send email with confirmation link 
        var success = await _userService.SendEmailConfirmationAsync(userDto, Request);

        if (!success)
        {
            var apiMsg = _messageProviderService.GetMessage("ERR026");
            var failureResponse = new ServiceFailureApiResponse<string>(email, apiMsg.Code, apiMsg.Message);
            return CreateApiResponse(failureResponse);
        }

        var infApiMsg = _messageProviderService.GetMessage("INF006");
        return CreateApiResponse(new DefaultApiResponse<string>(email, infApiMsg.Message));
    }

    /// <summary>
    /// Confirms a user's email address using a specified token and email.
    /// </summary>
    /// <param name="request">
    /// A <see cref="ConfirmEmailRequest"/> object containing the confirmation token and email address to confirm.
    /// </param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the result of the asynchronous operation.
    /// Returns a 200 OK response if the email confirmation is successful, or a 400 Bad Request response if the confirmation fails.
    /// </returns>
    /// <remarks>
    /// This endpoint allows users to confirm their email address by providing a confirmation token and email.
    /// It verifies the provided token and updates the user's account status upon successful confirmation.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{T}"/> with a success message if the email is confirmed successfully.
    /// </response>
    /// <response code="400">
    /// Returns a <see cref="ServiceFailureApiResponse{T}"/> if the confirmation process fails due to an invalid token or email.
    /// </response>
    [HttpPost("confirm-email")]
    [ProducesResponseType(typeof(DefaultApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceFailureApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
    {
        // https://emn178.github.io/online-tools/url_decode.html
        var token = Uri.UnescapeDataString(request.Token);
        var email = Uri.UnescapeDataString(request.Email);
        await _userService.ConfirmEmailAsync(token, email);
        var infApiMsg = _messageProviderService.GetMessage("INF007");
        return CreateApiResponse(new DefaultApiResponse<string>(string.Empty, infApiMsg.Message));
    }

    /// <summary>
    /// Updates a user's profile information based on the provided details in the request.
    /// </summary>
    /// <param name="request">The request object containing updated profile details for the user.</param>
    /// <param name="userCode">An optional unique user code for the user to be updated. 
    /// If not specified, the user's own code will be used unless the current user is a tenant admin or internal user.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> representing the outcome of the operation:
    /// <list type="bullet">
    /// <item>
    /// <description><see cref="StatusCodes.Status200OK"/> if the user profile is updated successfully.</description>
    /// </item>
    /// <item>
    /// <description><see cref="StatusCodes.Status400BadRequest"/> if the request is invalid, including validation errors.</description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This endpoint allows authorized users to update their own profile or the profile of other users, subject to role-based restrictions.
    /// <para>If the user role is below tenant admin, they can only update their own profile, identified by <paramref name="userCode"/>.</para>
    /// </remarks>
    [Authorize]
    [HttpPut("{userCode}")]
    [Consumes("application/json")]
    [ProducesResponseType<DefaultApiResponse<string>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ServiceFailureApiResponse<string>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ServiceFailureApiResponse<UpdateUserProfileRequest>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserProfileRequest request, string? userCode = null)
    {
        var errors = new List<ValidationFailure>();
        var userRoleIsBelowTenantAdminRole = UserRoleIsBelowTenantAdminRole();
        var userClaimsInfo = GetUserClaimsInfo();

        if (userRoleIsBelowTenantAdminRole)
        {
            if (!string.IsNullOrEmpty(userCode) &&
                userClaimsInfo.UserCode != userCode)
            {
                var apiMsg = _messageProviderService.GetMessage("ERR027");
                errors.Add(new ValidationFailure()
                {
                    PropertyName = nameof(userCode),
                    ErrorMessage = apiMsg.Message
                });
                return CreateApiResponse(new ValidationFailureApiResponse<string>(string.Empty, new ValidationResult(errors)));
            }
        }
        else
        {
            if (string.IsNullOrEmpty(userCode))
                userCode = userClaimsInfo.UserCode;
        }

        var existingUser = await _userService.GetByUserCodeAsync(userCode);
        if (existingUser == null)
        {
            var apiMsg = _messageProviderService.GetMessage("ERR028");
            errors.Add(new ValidationFailure() { PropertyName = nameof(userCode), ErrorMessage = apiMsg.Message });
            return CreateApiResponse(new ValidationFailureApiResponse<string>(userCode, new ValidationResult(errors)));
        }

        var validationResult = await _updateUserProfileRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<UpdateUserProfileRequest>(request, validationResult);
            return CreateApiResponse(validationResponse);
        }

        if (!string.IsNullOrEmpty(request.PhoneNumber))
            existingUser.PhoneNumber = request.PhoneNumber;
        if (!string.IsNullOrEmpty(request.FirstName))
            existingUser.FirstName = request.FirstName;
        if (!string.IsNullOrEmpty(request.LastName))
            existingUser.LastName = request.LastName;
        if (!string.IsNullOrEmpty(request.CompanyRole))
            existingUser.JobTitle = request.CompanyRole;
        if (!string.IsNullOrEmpty(request.StreetAddress))
            existingUser.StreetAddress = request.StreetAddress;
        if (!string.IsNullOrEmpty(request.AddressLine2))
            existingUser.AddressLine2 = request.AddressLine2;
        if (!string.IsNullOrEmpty(request.City))
            existingUser.City = request.City;
        if (!string.IsNullOrEmpty(request.StateOrRegion))
            existingUser.StateOrRegion = request.StateOrRegion;
        if (!string.IsNullOrEmpty(request.PostalCode))
            existingUser.PostalCode = request.PostalCode;
        if (!string.IsNullOrEmpty(request.CountryISOCode))
            existingUser.CountryISOCode = request.CountryISOCode;
        if (!string.IsNullOrEmpty(request.TimeZone))
            existingUser.TimeZone = request.TimeZone;

        if (request.ProfileImageUrl != null && existingUser.ProfileImageUrl != request.ProfileImageUrl)
            existingUser.ProfileImageUrl = request.ProfileImageUrl;

        if (UserRoleIsInternal())
        {
            if (request.LockoutEnabled.HasValue)
                existingUser.LockoutEnabled = request.LockoutEnabled.Value;

            if (request.LockoutEnd.HasValue)
                existingUser.LockoutEnd = request.LockoutEnd.Value;

            if (request.IsActive.HasValue)
                existingUser.IsActive = request.IsActive.Value;
        }

        await _userService.UpdateAsync(existingUser);
        var infApiMsg = _messageProviderService.GetMessage("INF008");
        return CreateApiResponse(new DefaultApiResponse<string>(string.Empty, infApiMsg.Message));
    }

    /// <summary>
    /// Deletes a user identified by the specified user code.
    /// </summary>
    /// <param name="userCode">
    /// A string representing the unique code of the user to be deleted.
    /// </param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
    /// Returns a 200 OK response upon successful deletion of the user.
    /// </returns>
    /// <remarks>
    /// This endpoint allows only users with the Global Admin role to delete a user from the system.
    /// It returns a success message upon completion.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{T}"/> with a success message if the user is deleted successfully.
    /// </response>
    [HttpDelete("{userCode}")]
    [Authorize(Policy = AuthPoliciesEnum.GlobalAdminPolicy)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteUser(string userCode)
    {
        await _userService.DeleteByUserCodeAsync(userCode);
        var infApiMsg = _messageProviderService.GetMessage("INF009");
        return CreateApiResponse(new DefaultApiResponse<string>(string.Empty, infApiMsg.Message));
    }

    /// <summary>
    /// Retrieves all active tenants associated with the specified user.
    /// </summary>
    /// <param name="userCode">The unique identifier of the user whose associated tenants are being retrieved.</param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
    /// Returns a 200 OK response containing the list of active tenants associated with the specified user.
    /// </returns>
    /// <remarks>
    /// Intended for use only by users with internal or global roles, this endpoint checks for tenants linked to the specified user 
    /// and only returns those that are active. The user must have a global admin role to access this information.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{List{TenantResponse}}"/> containing the list of active tenants for the user.
    /// </response>
    /// <response code="403">
    /// Returns a <see cref="ServiceFailureApiResponse{T}"/> if the user is not authorized to access the tenants for the specified user.
    /// </response>
    [HttpGet("{userCode}/tenants")]
    [Authorize(Policy = AuthPoliciesEnum.GlobalAdminPolicy)]
    [ProducesResponseType(typeof(DefaultApiResponse<List<TenantResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTenantsForUser(string userCode)
    {
        var tenantDtos = await _userService.GetTenantsForUserAsync(userCode, activesOnly: true);
        var tenantsResponse = Mapper.Map<List<TenantResponse>>(tenantDtos);
        return CreateApiResponse(new DefaultApiResponse<List<TenantResponse>>(tenantsResponse));
    }

    /// <summary>
    /// Retrieves the list of active tenants for the currently authenticated user.
    /// </summary>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
    /// Returns a 200 OK response containing the list of active tenants for the user.
    /// </returns>
    /// <remarks>
    /// This endpoint fetches all active tenants associated with the currently authenticated user, based on the user's claims information.
    /// The list of tenants is mapped to a <see cref="TenantResponse"/> object and returned in the response.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{List{TenantResponse}}"/> containing the active tenants for the authenticated user.
    /// </response>
    [HttpGet("tenants")]
    [Authorize]
    [ProducesResponseType(typeof(DefaultApiResponse<List<TenantResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTenants()
    {
        var claimsInfo = GetUserClaimsInfo();
        var tenantDtos = await _userService.GetTenantsForUserAsync(claimsInfo.UserCode, activesOnly: true);
        var tenantsResponse = Mapper.Map<List<TenantResponse>>(tenantDtos);
        return CreateApiResponse(new DefaultApiResponse<List<TenantResponse>>(tenantsResponse));
    }

    /// <summary>
    /// Retrieves a list of all users associated with a specific tenant, identified by the tenant code.
    /// </summary>
    /// <param name="tenantCode">
    /// A string representing the unique code of the tenant for whom to retrieve associated users.
    /// </param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation. 
    /// Returns a 200 OK response with a list of user information associated with the specified tenant.
    /// </returns>
    /// <remarks>
    /// This endpoint requires authorization and returns all users associated with the specified tenant.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{List{UserResponse}}"/> containing the list of users associated with the tenant.
    /// </response>
    [HttpGet("tenant-users/{tenantCode}")]
    [Authorize(Policy = AuthPoliciesEnum.GlobalAdminPolicy)]
    [ProducesResponseType(typeof(DefaultApiResponse<List<UserResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsersByTenantCode(string tenantCode)
    {
        var users = await _userService.GetAllByTenantAsync(tenantCode);
        var usersResponse = Mapper.Map<List<UserResponse>>(users);
        return CreateApiResponse(new DefaultApiResponse<List<UserResponse>>(usersResponse));
    }

    /// <summary>
    /// If current user is internal administrative, resource retrieves all users irregardless of the tenant association.
    /// If tenant related, resource retrieves all users related to the current tenant the user is associated with.
    /// </summary>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
    /// Returns a 200 OK response containing the list of users for the current tenant.
    /// </returns>
    /// <remarks>
    /// This endpoint checks if the current session is associated with a tenant. If the tenant code is missing from the claims information,
    /// a 400 Bad Request response will be returned indicating the requirement to be logged into a tenant session. If the session is valid,
    /// a list of users belonging to the tenant is fetched and returned in the response.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{List{UserResponse}}"/> containing the list of users for the tenant.
    /// </response>
    /// <response code="400">
    /// Returns a <see cref="ServiceFailureApiResponse{T}"/> indicating that a tenant session is required to perform the action.
    /// </response>
    [HttpGet("users")]
    [Authorize]
    [ProducesResponseType<DefaultApiResponse<List<UserResponse>>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ServiceFailureApiResponse<string>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllUsers()
    {
        var claimsInfo = GetUserClaimsInfo();
        var roleIsInternalAdmin = claimsInfo.Roles.Any(r => r.IsRoleInternal());

        if (!roleIsInternalAdmin && string.IsNullOrEmpty(claimsInfo.TenantCode))
        {
            var apiMsg = _messageProviderService.GetMessage("ERR029");
            var failureResponse = new ServiceFailureApiResponse<string>(string.Empty, apiMsg.Code, apiMsg.Message, statusCode: StatusCodes.Status400BadRequest);
            return CreateApiResponse(failureResponse);
        }

        IEnumerable<ApplicationUserDto> users;

        if (roleIsInternalAdmin)
            users = await _userService.GetAllAsync(u => u.IsActive == true && u.UserCode != claimsInfo.UserCode);
        else
        {
            //users = await _userService.GetAllAsync(u => u.IsActive == true && u.UserCode != claimsInfo.UserCode );
            users = await _userService.GetAllByTenantAsync(claimsInfo.TenantCode, activeOnly: true);
        }

        var usersResponse = Mapper.Map<List<UserResponse>>(users);
        return CreateApiResponse(new DefaultApiResponse<List<UserResponse>>(usersResponse));
    }

    /// <summary>
    /// Retrieves a list of all available user roles as a collection of strings.
    /// </summary>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation. 
    /// Returns a 200 OK response containing a collection of user role names.
    /// </returns>
    /// <remarks>
    /// This endpoint returns all user roles defined in the <see cref="UserRolesEnum"/> enumeration as a collection of strings.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="ServiceFailureApiResponse{T}"/> with the list of user roles.
    /// </response>
    [HttpGet("user-roles")]
    [ProducesResponseType<DefaultApiResponse<IEnumerable<string>>>(StatusCodes.Status200OK)]
    public Task<IActionResult> GetUserRoles()
    {
        var userRoles = EnumExt.GetAllItems<UserRolesEnum>().Select(e => e.ToString());
        var successResponse = new DefaultApiResponse<IEnumerable<string>>(userRoles);
        return Task.FromResult(CreateApiResponse(successResponse));
    }

    /// <summary>
    /// Uploads a user's avatar image to a CDN and returns the URL of the uploaded image.
    /// </summary>
    /// <param name="userCode">The unique identifier of the user whose avatar is being uploaded.</param>
    /// <param name="avatarFormFile">The image file to be uploaded as the user's avatar.</param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
    /// Returns a <see cref="DefaultApiResponse{T}"/> containing the CDN URL of the uploaded image upon success, or a <see cref="ValidationFailureApiResponse{T}"/> in case of validation failure.
    /// </returns>
    /// <remarks>
    /// This method validates that both the user code and avatar file are provided. If valid, the avatar is uploaded, and a URL to the uploaded resource is returned.
    /// If either parameter is missing, a validation error response is generated.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{T}"/> with the CDN URL if the upload is successful.
    /// </response>
    /// <response code="400">
    /// Returns a <see cref="ValidationFailureApiResponse{T}"/> with validation errors if the user code or avatar file is missing or invalid.
    /// </response>
    [Authorize]
    [HttpPost("UserProfileImage")]
    [ProducesResponseType<DefaultApiResponse<string>>(StatusCodes.Status200OK)]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadUserProfileImage([FromForm] UserProfileImageRequest request)
    {
        var validationResult = await _userProfileImageRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<UserProfileImageRequest>(request, validationResult);
            return CreateApiResponse(validationResponse);
        }

        var claimsInfo = GetUserClaimsInfo();
        if (!string.IsNullOrEmpty(request.UserCode))
        {
            if (!claimsInfo.Roles.Any(r => r.IsRoleInternal() ||
                                        r != UserRolesEnum.TenantOwner ||
                                        r != UserRolesEnum.TenantSystemAdmin))
            {
                var apiMsg = _messageProviderService.GetMessage("ERR030");
                var errors = new List<ValidationFailure>
                {
                    new(nameof(UserProfileImageRequest.UserCode), apiMsg.Message)
                };
                var validationResponse = new ValidationFailureApiResponse<UserProfileImageRequest>(request, new ValidationResult(errors));
                return CreateApiResponse(validationResponse);
            }
        }
        else
        {
            request.UserCode = claimsInfo.UserCode;
        }

        var cdnUrl = await _userService.UploadUserProfileImage(request.UserCode, request.UserImage);

        var successResponse = new DefaultApiResponse<string>(cdnUrl);
        return CreateApiResponse(successResponse);
    }
}
