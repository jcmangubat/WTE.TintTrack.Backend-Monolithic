using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Messaging.Core.Request;
using WTE.TintTrack.Api.Messaging.Core.Responses;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Common.Exceptions;
using WTE.TintTrack.Core.Application.DTOs;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;
using WTE.TintTrack.Core.Domain.Interfaces.Services;

namespace WTE.TintTrack.Api.Controllers.Core;

/// <summary>
/// AuthController manages authentication and user-related operations.
/// </summary>
[ApiController]
[Route("api/auth")]
//[ApiExplorerSettings(GroupName = "coremodules")]
[Produces(MediaTypeNames.Application.Json)]
public class AuthController(ILogger<AuthController> logger,
                            IMapper mapper,
                            UserManager<ApplicationUser> userManager,
                            IMessageProviderService messageProviderService,

                            IValidator<LoginRequest> loginValidator,
                            IValidator<PasswordResetRequest> passwordResetValidator,
                            IValidator<RefreshTokenRequest> refreshTokenRequestValidator,

                            IUserService userService,
                            ITokenService tokenService,
                            ITenantService tenantService,
                            IUserTenantService userTenantService,
                            ITenantSubscriptionService tenantSubscriptionService,
                            IRolePermissionService rolePermissionService,

                            ITenantSubscriptionRepository tenantSubscriptionRepository,

                            ITokenValidationService tokenValidationService)

    : LoggingMappedControllerBase<AuthController>(logger, mapper, messageProviderService)
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    private readonly IValidator<LoginRequest> _loginValidator = loginValidator;
    private readonly IValidator<PasswordResetRequest> _passwordResetValidator = passwordResetValidator;
    private readonly IValidator<RefreshTokenRequest> _refreshTokenRequestValidator = refreshTokenRequestValidator;

    private readonly IUserService _userService = userService;
    private readonly ITokenService _tokenService = tokenService;
    private readonly ITenantService _tenantService = tenantService;
    private readonly IUserTenantService _userTenantService = userTenantService;
    private readonly ITenantSubscriptionService _tenantSubscriptionService = tenantSubscriptionService;
    private readonly IRolePermissionService _rolePermissionService = rolePermissionService;

    private readonly ITenantSubscriptionRepository _tenantSubscriptionRepository = tenantSubscriptionRepository;

    private readonly ITokenValidationService _tokenValidationService = tokenValidationService;

    /// <summary>
    /// Authenticates a user based on the provided login credentials and generates a token for successful login.
    /// </summary>
    /// <param name="loginRequest">The login request containing the user's email and password.</param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation. 
    /// Returns a 200 OK response containing the login details (token, roles, and tenant associations) on success, 
    /// or a 400 Bad Request response if validation fails or the email is not confirmed, 
    /// or a 401 Unauthorized response if the credentials are invalid.
    /// </returns>
    /// <remarks>
    /// This endpoint first validates the login request and checks if the user's email is confirmed. 
    /// If the credentials are correct, a token is generated, and the user's internal roles and tenant associations are retrieved.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{LoginResponse}"/> containing the authentication token, user roles, and tenant associations.
    /// </response>
    /// <response code="400">
    /// Returns a <see cref="ValidationFailureApiResponse{LoginRequest}"/> if the request fails validation or if the email is not confirmed.
    /// </response>
    /// <response code="401">
    /// Returns a <see cref="ServiceFailureApiResponse{LoginRequest}"/> if the credentials are invalid.
    /// </response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(DefaultApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationFailureApiResponse<LoginRequest>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceFailureApiResponse<LoginRequest>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        loginRequest.Email = loginRequest.Email.Trim();
        loginRequest.Password = loginRequest.Password.Trim();

        var validationResult = await _loginValidator.ValidateAsync(loginRequest);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<LoginRequest>(loginRequest, validationResult);
            return CreateApiResponse(validationResponse);  // Return 400 Bad Request with validation errors
        }

        ApplicationUserDto? user = await _userService.GetByEmailAsync(loginRequest.Email);
        if (user != null && !user.EmailConfirmed)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR003");
            var failureResponse = new ServiceFailureApiResponse<LoginRequest>(loginRequest, apiMsg.Code, apiMsg.Message, statusCode: StatusCodes.Status401Unauthorized); //"Email not confirmed."
            return CreateApiResponse(failureResponse);
        }

        user = await _userService.AuthenticateAsync(loginRequest.Email, loginRequest.Password);
        if (user == null)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR004");
            var failureResponse = new ServiceFailureApiResponse<LoginRequest>(loginRequest, apiMsg.Code, apiMsg.Message, statusCode: StatusCodes.Status401Unauthorized);
            return CreateApiResponse(failureResponse);
        }

        var userTenantAssocStrips = await _userService.GetUserTenantsAssociationsAsync(user);
        var clentTokenResponse = await _tokenService.GenerateTokenAsync(user);

        var usrInternalRoles = await _userService.GetInternalRolesAsync(user);
        var roles = usrInternalRoles.Select(r => r.ToString()).ToList();

        var loginResponse = new LoginResponse(clentTokenResponse, roles, userTenantAssocStrips.ToList());
        var infApiMsg = MessageProviderService.GetMessage("INF000");
        var successResponse = new DefaultApiResponse<LoginResponse>(loginResponse, infApiMsg.Message);

        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Logs out the currently authenticated user, revoking their refresh token and optionally all devices' sessions.
    /// </summary>
    /// <param name="allDevices">A flag indicating whether the logout should apply to all devices (defaults to false).</param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation. 
    /// Returns a 200 OK response with a success message on successful logout, or a 401 Unauthorized response if the user is not authenticated.
    /// </returns>
    /// <remarks>
    /// This endpoint revokes the user's refresh token, terminating the user's session. Optionally, if the `allDevices` flag is set to true, 
    /// it will revoke the refresh token across all devices the user is logged into.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{T}"/> with a success message indicating the user has logged out successfully.
    /// </response>
    /// <response code="401">
    /// Returns a <see cref="ServiceFailureApiResponse{T}"/> with an error message if the request is unauthorized.
    /// </response>
    [Authorize]
    [HttpPost("logout")]
    [ProducesResponseType(typeof(DefaultApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceFailureApiResponse<string>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout(bool? allDevices = false)
    {
        var userClaimsInfo = GetUserClaimsInfo();

        if (string.IsNullOrEmpty(userClaimsInfo.Email))
        {
            var apiMsg = MessageProviderService.GetMessage("ERR005");
            var failureResponse = new ServiceFailureApiResponse<string>("", apiMsg.Code, apiMsg.Message, statusCode: StatusCodes.Status401Unauthorized);
            return CreateApiResponse(failureResponse);
        }

        await _tokenService.RevokeTokenForUserAsync(userClaimsInfo.RefreshToken, allDevices);

        var infApiMsg = MessageProviderService.GetMessage("INF003");
        return CreateApiResponse(new DefaultApiResponse<string>("", infApiMsg.Message));
    }

    /// <summary>
    /// Allows a user to switch to a different tenant by providing the tenant's code. 
    /// The method ensures the user is authorized to access the tenant and handles token revocation and generation for the new tenant.
    /// </summary>
    /// <param name="gotoTenantRequest">The request containing the tenant code to switch to.</param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation. 
    /// Returns a 200 OK response with a new client token if the user is authorized to access the tenant, or appropriate error responses if the user is unauthorized or invalid.
    /// </returns>
    /// <remarks>
    /// This endpoint is used for switching tenants in a multi-tenant application. It performs several checks to ensure:
    /// - The user is authorized to access the requested tenant.
    /// - The tenant is active.
    /// - The user's token is revoked before issuing a new one for the selected tenant.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{GotoTenantResponse}"/> with the new client token for the selected tenant.
    /// </response>
    /// <response code="400">
    /// Returns a <see cref="ValidationFailureApiResponse{GotoTenantRequest}"/> if the input request is invalid.
    /// </response>
    /// <response code="401">
    /// Returns a <see cref="ServiceFailureApiResponse{GotoTenantRequest}"/> if the request is unauthorized (user email not found in claims).
    /// </response>
    /// <response code="403">
    /// Returns a <see cref="ServiceFailureApiResponse{GotoTenantRequest}"/> if the user is forbidden from accessing the tenant (tenant status is not active).
    /// </response>
    [Authorize]
    [HttpPost("goto-tenant")]
    [ProducesResponseType(typeof(DefaultApiResponse<GotoTenantResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationFailureApiResponse<GotoTenantRequest>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceFailureApiResponse<GotoTenantRequest>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ServiceFailureApiResponse<GotoTenantRequest>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GotoTenant([FromBody] GotoTenantRequest gotoTenantRequest)
    {
        var userClaimsInfo = GetUserClaimsInfo();
        if (userClaimsInfo.Email == null)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR005");
            var failureResponse = new ServiceFailureApiResponse<GotoTenantRequest>(gotoTenantRequest, apiMsg.Code, apiMsg.Message, statusCode: StatusCodes.Status401Unauthorized);
            return CreateApiResponse(failureResponse);
        }

        var gotoTenantDto = await _tenantService.GetTenantByCodeAsync(gotoTenantRequest.TenantCode);
        if (gotoTenantDto == null)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR008");
            throw new RecordNotFoundException(apiMsg.Code, apiMsg.Message);
        }

        if (gotoTenantDto.TenantStatus != Common.Constants.Consts.TenantStatusEnum.Active)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR006");
            var failureResponse = new ServiceFailureApiResponse<GotoTenantRequest>(gotoTenantRequest, apiMsg.Code, apiMsg.Message, statusCode: StatusCodes.Status403Forbidden);
            return CreateApiResponse(failureResponse);
        }

        if (string.IsNullOrEmpty(userClaimsInfo.UserCode))
        {
            var apiMsg = MessageProviderService.GetMessage("ERR007");
            var failureResponse = new ServiceFailureApiResponse<GotoTenantRequest>(gotoTenantRequest, apiMsg.Code, apiMsg.Message, statusCode: StatusCodes.Status401Unauthorized);
            return CreateApiResponse(failureResponse);
        }

        if (!await _userTenantService.IsUserInTenantAsync(userClaimsInfo.UserCode, gotoTenantDto.TenantCode))
        {
            var apiMsg = MessageProviderService.GetMessage("ERR009");
            var failureResponse = new ServiceFailureApiResponse<GotoTenantRequest>(gotoTenantRequest, apiMsg.Code, apiMsg.Message, statusCode: StatusCodes.Status401Unauthorized);
            return CreateApiResponse(failureResponse);
        }

        var userDto = await _userService.GetByEmailAsync(userClaimsInfo.Email);
        if (userDto == null)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR010");
            var failureResponse = new ServiceFailureApiResponse<GotoTenantRequest>(gotoTenantRequest, apiMsg.Code, apiMsg.Message, statusCode: StatusCodes.Status401Unauthorized);
            return CreateApiResponse(failureResponse);
        }


        // Validate tenant subscription if needs review for approval.
        var tenantSubscriptions = await _tenantSubscriptionRepository.GetByTenantAsync(gotoTenantRequest.TenantCode);
        if (tenantSubscriptions == null || !tenantSubscriptions.Any())
        {
            var apiMsg = MessageProviderService.GetMessage("ERR011");
            throw new RecordNotFoundException(apiMsg.Code, apiMsg.Message);
        }

        var activeTenantSubscription = tenantSubscriptions.FirstOrDefault(p => p.SubscriptionStatus == Common.Constants.Consts.SubscriptionStatusEnum.Active);
        if (activeTenantSubscription == null)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR012");
            throw new RecordNotFoundException(apiMsg.Code, apiMsg.Message);
        }

        await _tokenService.RevokeTokenForUserAsync(userClaimsInfo.RefreshToken);

        var clientToken = await _tokenService.GenerateTokenAsync(userDto, gotoTenantDto);
        var userTenantRoles = await _userTenantService.GetUserRolesInTenantAsync(userDto.UserCode, gotoTenantRequest.TenantCode);

        var roles = userTenantRoles.Select(utr => utr.Role.Name ?? string.Empty)
                                        .Where(r => !string.IsNullOrEmpty(r))
                                        .ToList();

        var permissions = await _rolePermissionService.GetPermissionsForRolesAsync(roles);
        var gotoTenantResponse = new GotoTenantResponse(clientToken, roles, permissions);
        var infApiMsg = MessageProviderService.GetMessage("INF000");
        var successResponse = new DefaultApiResponse<GotoTenantResponse>(gotoTenantResponse, infApiMsg.Message);

        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Removes the user from the currently selected tenant, revokes the user's refresh token,
    /// and generates a new client token. Returns information about the user’s tenant associations.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> containing a success response with updated token and tenant associations.
    /// If the user is not logged into a tenant, returns a failure response with a 405 status code.</returns>
    /// <response code="200">If the user is successfully removed from the tenant and a new token is generated.</response>
    /// <response code="405">If the user is not currently logged into a tenant.</response>
    /// <exception cref="ServiceOperationException">Thrown when required user information is missing or user needs to re-login.</exception>
    /// <exception cref="RecordNotFoundException">Thrown when no user is found with the provided email address.</exception>
    [Authorize]
    [HttpPost("exit-tenant")]
    [ProducesResponseType(typeof(DefaultApiResponse<GotoTenantResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ExitTenant()
    {
        var userClaimsInfo = GetUserClaimsInfo();
        if (string.IsNullOrEmpty(userClaimsInfo.TenantCode))
        {
            var apiMsg = MessageProviderService.GetMessage("ERR013");
            var failureResponse = new ServiceFailureApiResponse<string>(string.Empty, apiMsg.Code, apiMsg.Message, statusCode: StatusCodes.Status405MethodNotAllowed);
            return CreateApiResponse(failureResponse);
        }

        if (string.IsNullOrEmpty(userClaimsInfo.Email) ||
            string.IsNullOrEmpty(userClaimsInfo.UserCode) ||
            string.IsNullOrEmpty(userClaimsInfo.RefreshToken))
        {
            var apiMsg = MessageProviderService.GetMessage("ERR014");
            throw new ServiceOperationException(apiMsg.Code, apiMsg.Message);
        }

        var userDto = await _userService.GetByEmailAsync(userClaimsInfo.Email);
        if (userDto == null)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR010");
            throw new RecordNotFoundException(apiMsg.Code, apiMsg.Message);
        }

        await _tokenService.RevokeTokenForUserAsync(userClaimsInfo.RefreshToken);

        var clientToken = await _tokenService.GenerateTokenAsync(userDto);

        var userTenantAssocStrips = await _userService.GetUserTenantsAssociationsAsync(userDto);
        var loginResponse = new LoginResponse(clientToken, [], userTenantAssocStrips.ToList());
        var infApiMsg = MessageProviderService.GetMessage("INF000");
        var successResponse = new DefaultApiResponse<LoginResponse>(loginResponse, infApiMsg.Message);
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Refreshes the user's authentication tokens using a provided refresh token.
    /// This method validates the request and refresh token, attempts to issue new tokens, and returns the results.
    /// </summary>
    /// <param name="request">
    /// The request containing the refresh token. Must be provided in the request body.
    /// </param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
    /// Returns a <see cref="DefaultApiResponse{ClientTokenDto}"/> with the refreshed tokens if successful, 
    /// or an error response detailing the failure.
    /// </returns>
    /// <remarks>
    /// This endpoint is designed to refresh authentication tokens without requiring the user to reauthenticate.
    /// It performs the following operations:
    /// - Validates the input request and ensures a refresh token is provided.
    /// - Checks the refresh token for validity and expiration.
    /// - Issues new tokens if the refresh token is valid and not expired.
    /// - Returns appropriate error responses for invalid or expired tokens.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{ClientTokenDto}"/> containing the new access and refresh tokens if the operation succeeds.
    /// </response>
    /// <response code="400">
    /// Returns a <see cref="ValidationFailureApiResponse{RefreshTokenRequest}"/> if the request fails validation.
    /// </response>
    /// <response code="400">
    /// Returns a <see cref="ServiceFailureApiResponse{T}"/> if the refresh token is invalid, expired, or the refresh operation fails.
    /// </response>
    [HttpPost("refresh-access-token")]
    [ProducesResponseType(typeof(DefaultApiResponse<ClientTokenDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationFailureApiResponse<RefreshTokenRequest>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceFailureApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshTokenRequest request)
    {
        var validationResult = await _refreshTokenRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<RefreshTokenRequest>(request, validationResult);
            return CreateApiResponse(validationResponse);  // Return 400 Bad Request with validation errors
        }

        if (!Guid.TryParse(request.RefreshToken, out var refreshToken))
        {
            var apiMsg = MessageProviderService.GetMessage("ERR015");
            var validationResponse = new ServiceFailureApiResponse<string>(request.RefreshToken, apiMsg.Code, apiMsg.Message);
            return CreateApiResponse(validationResponse);
        }

        var token = await _tokenService.GetTokenByRefreshTokenAsync(refreshToken);
        if (token == null || (token != null && token.RefreshTokenExpiration <= DateTime.UtcNow))
        {
            var apiMsg = MessageProviderService.GetMessage("ERR016");
            var validationResponse = new ServiceFailureApiResponse<string>(request.RefreshToken, apiMsg.Code, apiMsg.Message);
            return CreateApiResponse(validationResponse);
        }

        var newToken = await _tokenService.RefreshAccessTokenAsync(refreshToken);
        if (newToken == null)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR017");
            return CreateApiResponse(new ServiceFailureApiResponse<string>(request.RefreshToken, apiMsg.Code, apiMsg.Message));
        }

        var infApiMsg = MessageProviderService.GetMessage("INFO001");
        var successResponse = new DefaultApiResponse<ClientTokenDto>(newToken, infApiMsg.Message);
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Initiates a password reset request by generating a password reset token for the specified email address.
    /// The token will be sent to the user to facilitate password recovery.
    /// </summary>
    /// <param name="email">The email address of the user requesting the password reset.</param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
    /// Returns a <see cref="DefaultApiResponse{T}"/> confirming that the reset token was sent or an error response if the token generation fails.
    /// </returns>
    /// <remarks>
    /// This endpoint triggers the password reset flow by generating a reset token and sending it to the user's email address.
    /// If the email is valid and the token is successfully generated, the user will receive the reset token to reset their password.
    /// If the operation fails, an error message will be returned.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{T}"/> indicating that the password reset token has been sent to the specified email.
    /// </response>
    /// <response code="400">
    /// Returns a <see cref="ServiceFailureApiResponse{T}"/> if there is an error generating the password reset token, such as an invalid email address or failure in the generation process.
    /// </response>
    [HttpPost("request-password-reset")]
    [ProducesResponseType(typeof(DefaultApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceFailureApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RequestPasswordReset(string email)
    {
        var result = await _userService.GeneratePasswordResetTokenAsync(email);
        if (!result.Succeeded)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR018");
            return CreateApiResponse(new ServiceFailureApiResponse<string>(email, apiMsg.Code, apiMsg.Message));
        }

        var infApiMsg = MessageProviderService.GetMessage("INF002");
        return CreateApiResponse(new DefaultApiResponse<string>(email, infApiMsg.Message));
    }

    /// <summary>
    /// Resets a user's password using the provided reset token and new password.
    /// The request must include a valid email, reset token, and the new password to complete the reset process.
    /// </summary>
    /// <param name="passwordResetRequest">The password reset request containing the email, reset token, and new password.</param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
    /// Returns a <see cref="DefaultApiResponse{T}"/> indicating the success of the password reset or a <see cref="ValidationFailureApiResponse{T}"/> in case of validation failure.
    /// </returns>
    /// <remarks>
    /// This method validates the password reset request and checks whether the reset token and new password meet the required criteria.
    /// If the request is valid, the password is reset successfully. If not, appropriate validation errors are returned.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{T}"/> confirming that the password has been reset successfully.
    /// </response>
    /// <response code="400">
    /// Returns a <see cref="ValidationFailureApiResponse{T}"/> containing the validation errors if the request is invalid, such as missing or incorrect fields.
    /// </response>
    [HttpPost("reset-password")]
    [ProducesResponseType(typeof(DefaultApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationFailureApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationFailureApiResponse<PasswordResetRequest>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromBody] PasswordResetRequest passwordResetRequest)
    {
        var validationResult = await _passwordResetValidator.ValidateAsync(passwordResetRequest);

        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<PasswordResetRequest>(passwordResetRequest, validationResult);
            return CreateApiResponse(validationResponse);
        }

        var result = await _userService.ResetPasswordAsync(passwordResetRequest.Email, passwordResetRequest.ResetToken, passwordResetRequest.NewPassword);
        if (!result.Succeeded)
        {
            //return BadRequest(result.Errors); // 400 Bad Request with validation errors
            var errors = new Dictionary<string, string[]>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Code, [error.Description]);
            }
            var validationResponse = new ValidationFailureApiResponse<PasswordResetRequest>(passwordResetRequest, errors);
            return CreateApiResponse(validationResponse);
        }

        var infApiMsg = MessageProviderService.GetMessage("INF004");
        return CreateApiResponse(new DefaultApiResponse<string>("", infApiMsg.Message));
    }

    /// <summary>
    /// Retrieves user details from the provided access token.
    /// </summary>
    /// <param name="accessToken">The access token provided by the user to retrieve their details.</param>
    /// <returns>
    /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation. 
    /// Returns a 200 OK response containing the user details if the token is valid, or a 400 Bad Request response if the token is missing or invalid.
    /// </returns>
    /// <remarks>
    /// This endpoint extracts user, tenant, and subscription details from an access token. 
    /// If the access token is invalid or missing, a 400 response is returned. 
    /// Additionally, a check is performed to validate the token, and the status is returned as part of the response.
    /// </remarks>
    /// <response code="200">
    /// Returns a <see cref="DefaultApiResponse{UserFromAccessTokenResponse}"/> containing user details, tenant information, 
    /// and subscription details retrieved from the access token along with the token validity status.
    /// </response>
    /// <response code="400">
    /// Returns a <see cref="ValidationFailureApiResponse{T}"/> if the access token is missing or invalid.
    /// </response>
    /// <response code="500">
    /// Returns a 500 Internal Server Error response in case of an unexpected failure during the process.
    /// </response>
    [Authorize]
    [HttpPost("validate-token")]
    [ProducesResponseType(typeof(DefaultApiResponse<UserFromAccessTokenResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationFailureApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ValidateTokenAsync([FromBody] string accessToken)
    {
        if (string.IsNullOrEmpty(accessToken))
        {
            var apiMsg = MessageProviderService.GetMessage("ERR019");
            var errorResponse = new ValidationFailureApiResponse<string>(string.Empty, null, apiMsg.Message);
            return CreateApiResponse(errorResponse);
        }

        (ApplicationUserDto User, TenantDto? Tenant, SubscriptionPlanDto? SubscriptionPlanDto, List<string> Roles, Guid RefreshToken)? tupleResult =
            await _tokenService.GetDetailsFromAccessTokenAsync(accessToken);

        _tokenValidationService.ValidateToken(accessToken);
        var token = await _tokenService.GetTokenByRefreshTokenAsync(tupleResult.Value.RefreshToken);

        TenantResponse? tenantResponse = null;
        var userResponse = Mapper.Map<UserResponse>(tupleResult.Value.User);

        if (tupleResult.Value.Tenant != null)
        {
            tenantResponse = Mapper.Map<TenantResponse>(tupleResult.Value.Tenant);
            tenantResponse.SubscriptionPlanCode = tupleResult.Value.SubscriptionPlanDto?.PlanCode;
        }

        var permissions = await _rolePermissionService.GetPermissionsForRolesAsync(tupleResult.Value.Roles);
        var userFromAccessToken = new UserFromAccessTokenResponse(userResponse, tenantResponse, tupleResult.Value.Roles, permissions)
        {
            //TokenIsValid = token != null && tokenIsValid
        };

        return CreateApiResponse(new DefaultApiResponse<UserFromAccessTokenResponse>(userFromAccessToken));
    }
}