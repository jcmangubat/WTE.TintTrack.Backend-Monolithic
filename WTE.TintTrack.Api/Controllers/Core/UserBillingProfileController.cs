using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMEAppHouse.Core.CodeKits.Extensions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Messaging.Core.Requests;
using WTE.TintTrack.Api.Messaging.Core.Responses;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Core;

/// <summary>
/// Controller for handling user billing profile operations.
/// </summary>
/// <remarks>
/// This controller manages user billing profiles, which store payment and billing information for users. It provides operations for retrieving billing profiles by user code, retrieving available billing profile types, and registering new billing profiles. The controller enables users to manage their payment methods, billing addresses, and billing preferences, supporting the subscription billing and payment processing functionality of the system.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
//[ApiExplorerSettings(GroupName = "coremodules")]
[Produces("application/json")]
public class UserBillingProfileController(ILogger<UserBillingProfileController> logger, IMapper mapper, IMessageProviderService messageProviderService,
                                        IValidator<UserBillingProfileRequest> userBillingProfileRequestValidator,
                                        IUserBillingProfileService userBillingProfileService)
     : LoggingMappedControllerBase<UserBillingProfileController>(logger, mapper, messageProviderService)
{
    private readonly IValidator<UserBillingProfileRequest> _userBillingProfileRequestValidator = userBillingProfileRequestValidator;

    private readonly IUserBillingProfileService _userBillingProfileService = userBillingProfileService;

    /// <summary>
    /// Retrieves the billing profiles for a specific user by their user ID.
    /// </summary>
    /// <param name="userCode">The code of the user.</param>
    /// <returns>The UserBillingProfile DTO if found.</returns>
    [Authorize]
    [HttpGet("user/{userCode}")]
    [ProducesResponseType(typeof(DefaultApiResponse<IEnumerable<UserBillingProfileResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBillingProfilesByUserCode(string userCode)
    {
        var billingProfiles = await _userBillingProfileService.GetBillingProfilesByUserCodeAsync(userCode);
        var billingProfilesResponse = Mapper.Map<IEnumerable<UserBillingProfileResponse>>(billingProfiles);
        var successResponse = new DefaultApiResponse<IEnumerable<UserBillingProfileResponse>>(billingProfilesResponse);
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Retrieves all of the the billing profiles types available in the system
    /// </summary>
    /// <returns>The UserBillingProfileTypes.</returns>
    [HttpGet("types")]
    [ProducesResponseType<DefaultApiResponse<IEnumerable<string>>>(StatusCodes.Status200OK)]
    public Task<IActionResult> GetBillingProfilesTypes()
    {
        var billingProfileTypes = EnumExt.GetAllItems<Consts.BillingProfileTypesEnum>().Select(e => e.ToString());
        var successResponse = new DefaultApiResponse<IEnumerable<string>>(billingProfileTypes);
        return Task.FromResult(CreateApiResponse(successResponse));
    }

    /// <summary>
    /// Registers a new billing profile.
    /// </summary>
    /// <param name="userBillingProfileRequest">The object containing billing profile details.</param>
    /// <returns>The created UserBillingProfile DTO.</returns>
    [HttpPost]
    [ProducesResponseType<ValidationFailureApiResponse<UserBillingProfileRequest>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationFailureApiResponse<UserBillingProfileRequest>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterBillingProfile([FromBody] UserBillingProfileRequest userBillingProfileRequest)
    {
        var validationResult = await _userBillingProfileRequestValidator.ValidateAsync(userBillingProfileRequest);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<UserBillingProfileRequest>(userBillingProfileRequest, validationResult);
            return CreateApiResponse(validationResponse);
        }

        var userBillingProfileDto = Mapper.Map<UserBillingProfileDto>(userBillingProfileRequest);
        userBillingProfileDto = await _userBillingProfileService.RegisterBillingProfileAsync(userBillingProfileDto);

        var createdProfileResponse = Mapper.Map<UserBillingProfileResponse>(userBillingProfileDto);

        var apiResponse = new DefaultApiResponse<UserBillingProfileResponse>(createdProfileResponse) { StatusCode = StatusCodes.Status201Created };
        Response.StatusCode = apiResponse.StatusCode;

        return CreateApiResponse(apiResponse);
    }
}