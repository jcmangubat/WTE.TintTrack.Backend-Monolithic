using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Messaging.Core.Request;
using WTE.TintTrack.Api.Messaging.Core.Responses;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Core;

[ApiController]
[Route("api/[controller]")]
//[ApiExplorerSettings(GroupName = "coremodules")]
[Produces(MediaTypeNames.Application.Json)]
public class SubscriptionPlanController(ILogger<SubscriptionPlanController> logger,
                                                IMapper mapper, IMessageProviderService messageProviderService,
                                                ISubscriptionPlanService subscriptionPlanService,
IValidator<SubscriptionPlanRequest> subscriptionPlanRequestValidator)
    : LoggingMappedControllerBase<SubscriptionPlanController>(logger, mapper, messageProviderService)
{
    private readonly ISubscriptionPlanService _subscriptionPlanService = subscriptionPlanService;

    private readonly IValidator<SubscriptionPlanRequest> _subscriptionPlanRequestValidator = subscriptionPlanRequestValidator;

    /// <summary>
    /// Retrieves all subscription plans.
    /// </summary>
    /// <returns>A collection of subscription plans.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(DefaultApiResponse<List<SubscriptionPlanResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(bool activesOnly = true)
    {
        var subscriptionPlans = await _subscriptionPlanService.GetAllAsync(activesOnly);
        var subscriptionPlansResponse = Mapper.Map<List<SubscriptionPlanResponse>>(subscriptionPlans);

        var successResponse = new DefaultApiResponse<List<SubscriptionPlanResponse>>(subscriptionPlansResponse, "Success");
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Retrieves a specific subscription plan by its ID.
    /// </summary>
    /// <param name="planCode">The code of the subscription plan.</param>
    /// <returns>The subscription plan DTO if found; otherwise, NotFound.</returns>
    [HttpGet("{planCode}")]
    [ProducesResponseType(typeof(DefaultApiResponse<SubscriptionPlanResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByPlan(string planCode)
    {
        var subscriptionPlan = await _subscriptionPlanService.GetByPlanCodeAsync(planCode);
        var subscriptionPlanResponse = Mapper.Map<SubscriptionPlanResponse>(subscriptionPlan);

        var successResponse = new DefaultApiResponse<SubscriptionPlanResponse>(subscriptionPlanResponse, "Success");
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Creates a new subscription plan.
    /// </summary>
    /// <param name="subscriptionPlanRequest">The subscription plan data to create.</param>
    /// <returns>The created subscription plan in response.</returns>
    [HttpPost]
    [Authorize(Policy = "GlobalAdminPolicy")]
    [ProducesResponseType(typeof(DefaultApiResponse<SubscriptionPlanResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] SubscriptionPlanRequest subscriptionPlanRequest)
    {
        ValidationResult? validationResult = await _subscriptionPlanRequestValidator.ValidateAsync(subscriptionPlanRequest);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<SubscriptionPlanRequest>(subscriptionPlanRequest, validationResult);
            return CreateApiResponse(validationResponse);  // Return 400 Bad Request with validation errors
        }

        var subscriptionPlanDto = Mapper.Map<SubscriptionPlanDto>(subscriptionPlanRequest);
        subscriptionPlanDto.Id = Guid.NewGuid();

        var createdPlan = await _subscriptionPlanService.CreateAsync(subscriptionPlanDto);
        return CreatedAtAction(nameof(GetByPlan), new { planCode = createdPlan.PlanCode }, createdPlan);
    }

    /// <summary>
    /// Updates an existing subscription plan.
    /// </summary>
    /// <param name="planCode">The code of the subscription plan to update.</param>
    /// <param name="subscriptionPlanRequest">The updated subscription plan data.</param>
    /// <returns>The updated subscription plan DTO.</returns>
    [HttpPut("{planCode}")]
    [Authorize(Policy = "GlobalAdminPolicy")]
    [ProducesResponseType(typeof(DefaultApiResponse<SubscriptionPlanResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(string planCode, [FromBody] SubscriptionPlanRequest subscriptionPlanRequest)
    {
        ValidationResult? validationResult = await _subscriptionPlanRequestValidator.ValidateAsync(subscriptionPlanRequest);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<SubscriptionPlanRequest>(subscriptionPlanRequest, validationResult);
            return CreateApiResponse(validationResponse);  // Return 400 Bad Request with validation errors
        }

        var subscriptionPlanDto = Mapper.Map<SubscriptionPlanDto>(subscriptionPlanRequest);

        var updatedPlan = await _subscriptionPlanService.UpdateAsync(planCode, subscriptionPlanDto);
        var subscriptionPlanResponse = Mapper.Map<SubscriptionPlanResponse>(updatedPlan);
        var successResponse = new DefaultApiResponse<SubscriptionPlanResponse>(subscriptionPlanResponse, "Success");
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Deletes a subscription plan by its ID.
    /// </summary>
    /// <param name="planCode">The code of the subscription plan to delete.</param>
    /// <returns>No content if the deletion is successful.</returns>
    [HttpDelete("{planCode}")]
    [Authorize(Policy = "GlobalAdminPolicy")]
    [ProducesResponseType(typeof(DefaultApiResponse<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(string planCode)
    {
        SubscriptionPlanDto subscriptionPlan = await _subscriptionPlanService.GetByPlanCodeAsync(planCode);

        await _subscriptionPlanService.DeleteSubscriptionPlanAsync(subscriptionPlan.Id);

        var successResponse = new DefaultApiResponse<string>(planCode, "Success");
        return CreateApiResponse(successResponse);
    }
}