using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Messaging.Core.Responses;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Core;

/// <summary>
/// Controller for managing tenant subscriptions.
/// </summary>
/// <remarks>
/// Initializes a new instance of the TenantSubscriptionController class.
/// </remarks>
/// <param name="logger"></param>
/// <param name="mapper"></param>
/// <param name="tenantService"></param>
/// <param name="tenantSubscriptionService">The service responsible for tenant subscriptions.</param>
/// <param name="subscriptionPlanService"></param>
[ApiController]
[Route("api/[controller]")]
//[ApiExplorerSettings(GroupName = "coremodules")]
[Produces(MediaTypeNames.Application.Json)]
public class TenantSubscriptionController(ILogger<TenantSubscriptionController> logger, IMapper mapper, IMessageProviderService messageProviderService,
                                            ITenantService tenantService,
                                            ITenantSubscriptionService tenantSubscriptionService,
                                            ISubscriptionPlanService subscriptionPlanService)
    : LoggingMappedControllerBase<TenantSubscriptionController>(logger, mapper, messageProviderService)
{
    private readonly ITenantService _tenantService = tenantService;
    private readonly ITenantSubscriptionService _tenantSubscriptionService = tenantSubscriptionService;
    private readonly ISubscriptionPlanService _subscriptionPlanService = subscriptionPlanService;

    /// <summary>
    /// Retrieves all subscriptions for a specific tenant.
    /// </summary>
    /// <param name="tenantCode">The code of the tenant.</param>
    /// <returns>A list of TenantSubscription DTOs.</returns>
    [Authorize]
    [HttpGet("{tenantCode}")]
    [ProducesResponseType<DefaultApiResponse<IEnumerable<TenantSubscriptionResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSubscriptionsByTenantId(string tenantCode)
    {
        var subscriptionsDto = await _tenantSubscriptionService.GetSubscriptionsByTenantAsync(tenantCode);
        var subscriptionsResponse = Mapper.Map<IEnumerable<TenantSubscriptionResponse>>(subscriptionsDto);

        var successResponse = new DefaultApiResponse<IEnumerable<TenantSubscriptionResponse>>(subscriptionsResponse, "Success");
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Retrieves a specific subscription by its ID.
    /// </summary>
    /// <param name="tenantCode">The code of the tenant.</param>
    /// <returns>The TenantSubscription DTO if found.</returns>
    [Authorize]
    [HttpGet("subscription/{tenantCode}")]
    [ProducesResponseType<DefaultApiResponse<IEnumerable<TenantSubscriptionResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActiveSubscription(string tenantCode)
    {
        var tenantSubscriptionDto = await _tenantSubscriptionService.GetActiveSubscriptionByTenantAsync(tenantCode);
        var tenantSubscriptionResponse = Mapper.Map<TenantSubscriptionResponse>(tenantSubscriptionDto);

        var successResponse = new DefaultApiResponse<TenantSubscriptionResponse>(tenantSubscriptionResponse, "Success");
        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Deletes a subscription
    /// </summary>
    /// <param name="tenantCode">The code of the tenant plan to delete.</param>
    /// <param name="planCode">The code of the subscription plan to delete.</param>
    /// <returns>No content result if the operation is successful.</returns>
    [HttpDelete]
    [Authorize(Policy = "GlobalAdminPolicy")]
    [Route("{tenantCode}/{planCode}")]
    [ProducesResponseType(typeof(DefaultApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteSubscription([FromQuery] string tenantCode, [FromQuery] string planCode)
    {
        await _tenantSubscriptionService.DeleteSubscriptionAsync(tenantCode, planCode);
        var successResponse = new DefaultApiResponse<bool>(true);
        return CreateApiResponse(successResponse);
    }

    [HttpPost]
    [Authorize(Policy = "GlobalAdminPolicy")]
    [Route("{planCode}")]
    [ProducesResponseType<DefaultApiResponse<TenantSubscriptionResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterSubscription([FromBody] string planCode)
    {
        var userClaimsInfo = GetUserClaimsInfo();
        var newSubscriptionPlan = await _subscriptionPlanService.GetByPlanCodeAsync(planCode);
        var tenantDto = await _tenantService.GetTenantByCodeAsync(userClaimsInfo.TenantCode);

        if (!string.IsNullOrEmpty(userClaimsInfo.SubscriptionPlanCode) &&
            !string.IsNullOrEmpty(userClaimsInfo.TenantCode))
            await _tenantSubscriptionService.DeactivateActiveSubscriptionAsync(userClaimsInfo.TenantCode);

        var newTenantSubscriptionDto = new TenantSubscriptionDto
        {
            Id = Guid.NewGuid(),
            SubscriptionPlanId = newSubscriptionPlan.Id,
            TenantId = tenantDto.Id,
            SubscriptionStatus = Common.Constants.Consts.SubscriptionStatusEnum.ForReview
        };

        newTenantSubscriptionDto = await _tenantSubscriptionService.RegisterTenantSubscriptionAsync(newTenantSubscriptionDto);
        var newTenantSubscriptionResponse = Mapper.Map<TenantSubscriptionResponse>(newTenantSubscriptionDto);
        var successResponse = new DefaultApiResponse<TenantSubscriptionResponse>(newTenantSubscriptionResponse);
        return CreateApiResponse(successResponse);
    }
}