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
using WTE.TintTrack.Core.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Core;

[ApiController]
[Route("api/[controller]")]
//[ApiExplorerSettings(GroupName = "coremodules")]
[Produces(MediaTypeNames.Application.Json)]
public class SubscriptionPlanFeatureController(ILogger<SubscriptionPlanFeatureController> logger, IMapper mapper, IMessageProviderService messageProviderService,
                                                IValidator<SubscriptionPlanFeatureRequest> subscriptionPlanFeatureRequestValidator,
ISubscriptionPlanFeatureService subscriptionPlanFeatureService)

    : LoggingMappedControllerBase<SubscriptionPlanFeatureController>(logger, mapper, messageProviderService)
{
    private readonly IValidator<SubscriptionPlanFeatureRequest> _subscriptionPlanFeatureRequestValidator = subscriptionPlanFeatureRequestValidator;
    private readonly ISubscriptionPlanFeatureService _subscriptionPlanFeatureService = subscriptionPlanFeatureService;

    /// <summary>
    /// Retrieves all features associated with a specific subscription plan.
    /// </summary>
    /// <param name="planCode">The code of the subscription plan.</param>
    /// <returns>A collection of subscription plan features.</returns>
    [HttpGet("plan/{planCode}")]
    [ProducesResponseType<DefaultApiResponse<IEnumerable<SubscriptionPlanFeatureResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFeaturesBySubscriptionPlan(string planCode)
    {
        var planFeatures = await _subscriptionPlanFeatureService.GetFeaturesBySubscriptionPlanAsync(planCode);
        var featuresResponse = Mapper.Map<IEnumerable<SubscriptionPlanFeatureResponse>>(planFeatures);
        var successResponse = new DefaultApiResponse<IEnumerable<SubscriptionPlanFeatureResponse>>(featuresResponse, "Success");
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Retrieves a specific subscription plan feature by its code.
    /// </summary>
    /// <param name="planFeatureCode">The code of the subscription plan feature.</param>
    /// <returns>A subscription plan feature DTO if found.</returns>
    [HttpGet("feature/{planFeatureCode}")]
    [ProducesResponseType<DefaultApiResponse<SubscriptionPlanFeatureResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFeature(string planFeatureCode)
    {
        var planFeatureDto = await _subscriptionPlanFeatureService.GetSubscriptionPlanFeatureAsync(planFeatureCode);
        var planFeatureResponse = Mapper.Map<SubscriptionPlanFeatureResponse>(planFeatureDto);
        var successResponse = new DefaultApiResponse<SubscriptionPlanFeatureResponse>(planFeatureResponse, "Success");
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Deletes a subscription plan feature by its code.
    /// </summary>
    /// <param name="planFeatureCode">The code of the subscription plan feature to delete.</param>
    /// <returns>No content if the feature was successfully deleted.</returns>
    [HttpDelete("feature/{planFeatureCode}")]
    [Authorize(Policy = Consts.AuthPoliciesEnum.GlobalAdminPolicy)]
    [ProducesResponseType<DefaultApiResponse<string>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteFeature(string planFeatureCode)
    {
        await _subscriptionPlanFeatureService.DeleteFeatureAsync(planFeatureCode);
        var successResponse = new DefaultApiResponse<string>(planFeatureCode, "Success");
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Removes feature from a subscription plan.
    /// </summary>
    /// <param name="subscriptionPlanFeatureRequest">The data containing the plan and feature codes for the request.</param>
    /// <returns>No content if the feature was successfully deleted.</returns>
    [HttpDelete("plan")]
    [Authorize(Policy = Consts.AuthPoliciesEnum.GlobalAdminPolicy)]
    [ProducesResponseType<DefaultApiResponse<object>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationFailureApiResponse<SubscriptionPlanFeatureRequest>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveFeatureFromPlan([FromBody] SubscriptionPlanFeatureRequest subscriptionPlanFeatureRequest)
    {
        var validationResult = await _subscriptionPlanFeatureRequestValidator.ValidateAsync(subscriptionPlanFeatureRequest);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<SubscriptionPlanFeatureRequest>(subscriptionPlanFeatureRequest, validationResult);
            return CreateApiResponse(validationResponse);
        }

        await _subscriptionPlanFeatureService.RemoveFeatureFromPlan(subscriptionPlanFeatureRequest.PlanCode, subscriptionPlanFeatureRequest.FeatureCode);
        var successResponse = new DefaultApiResponse<dynamic>(new { subscriptionPlanFeatureRequest.PlanCode, subscriptionPlanFeatureRequest.FeatureCode }, "Successfuly removed.");
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Add feature to a subscription plan.
    /// </summary>
    /// <param name="subscriptionPlanFeatureRequest">The data containing the plan and feature codes for the request.</param>
    /// <returns>No content if the feature was successfully deleted.</returns>
    [HttpPost]
    [Authorize(Policy = Consts.AuthPoliciesEnum.GlobalAdminPolicy)]
    [ProducesResponseType<DefaultApiResponse<object>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationFailureApiResponse<SubscriptionPlanFeatureRequest>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddFeatureToPlan([FromBody] SubscriptionPlanFeatureRequest subscriptionPlanFeatureRequest)
    {
        var validationResult = await _subscriptionPlanFeatureRequestValidator.ValidateAsync(subscriptionPlanFeatureRequest);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<SubscriptionPlanFeatureRequest>(subscriptionPlanFeatureRequest, validationResult);
            return CreateApiResponse(validationResponse);
        }

        await _subscriptionPlanFeatureService.AddFeatureToPlan(subscriptionPlanFeatureRequest.PlanCode, subscriptionPlanFeatureRequest.FeatureCode);
        var successResponse = new DefaultApiResponse<dynamic>(new { subscriptionPlanFeatureRequest.PlanCode, featureCode = subscriptionPlanFeatureRequest.FeatureCode }, "Successfully added");
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Retrieves subscription plan and feature association.
    /// </summary>
    /// <returns>Object of the plan feature association.</returns>
    [HttpGet("plan/{planCode}/feature/{planFeatureCode}")]
    [ProducesResponseType<DefaultApiResponse<object>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationFailureApiResponse<SubscriptionPlanFeatureRequest>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> FindPlanFeatureAssociation(string planCode, string featureCode)
    {
        var subscriptionPlanFeatureRequest = new SubscriptionPlanFeatureRequest() { FeatureCode = featureCode, PlanCode = planCode };
        var validationResult = await _subscriptionPlanFeatureRequestValidator.ValidateAsync(subscriptionPlanFeatureRequest);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<SubscriptionPlanFeatureRequest>(subscriptionPlanFeatureRequest, validationResult);
            return CreateApiResponse(validationResponse);
        }

        var planFeatureAssociation = await _subscriptionPlanFeatureService.FindPlanFeatureAssociation(subscriptionPlanFeatureRequest.PlanCode, subscriptionPlanFeatureRequest.FeatureCode);
        var successResponse = new DefaultApiResponse<dynamic>(new { subscriptionPlanFeatureRequest.PlanCode, subscriptionPlanFeatureRequest.FeatureCode }, "Successfully found");
        return CreateApiResponse(successResponse);
    }
}
