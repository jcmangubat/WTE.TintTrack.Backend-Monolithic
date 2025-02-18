using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Messaging.Core.Responses;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Core.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Core;

[ApiController]
[Route("api/[controller]")]
//[ApiExplorerSettings(GroupName = "coremodules")]
[Produces(MediaTypeNames.Application.Json)]
public class SubscriptionPlanDiscountController(ILogger<SubscriptionPlanDiscountController> logger, IMapper mapper, IMessageProviderService messageProviderService,
ISubscriptionPlanDiscountService subscriptionPlanDiscountService)
    : LoggingMappedControllerBase<SubscriptionPlanDiscountController>(logger, mapper, messageProviderService)
{
    private readonly ISubscriptionPlanDiscountService _subscriptionPlanDiscountService = subscriptionPlanDiscountService;

    /// <summary>
    /// Retrieves all promo discounts associated with a specific subscription plan.
    /// </summary>
    /// <param name="planCode">The code of the subscription plan.</param>
    /// <returns>A collection of subscription promo discount DTOs.</returns>
    [Authorize]
    [HttpGet("subscription-plans/{planCode}/discounts")]
    [ProducesResponseType<IEnumerable<SubscriptionPlanDiscountResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySubscriptionPlan(string planCode)
    {
        var discountsDto = await _subscriptionPlanDiscountService.GetBySubscriptionPlanAsync(planCode);
        var discountsResponse = Mapper.Map<IEnumerable<SubscriptionPlanDiscountResponse>>(discountsDto);

        return CreateApiResponse(new DefaultApiResponse<IEnumerable<SubscriptionPlanDiscountResponse>>(discountsResponse, "Success"));
    }

    /// <summary>
    /// Retrieves a specific promo discount by its ID.
    /// </summary>
    /// <param name="planDiscountCode">The code of the discount.</param>
    /// <returns>The subscription promo discount DTO if found; otherwise, NotFound.</returns>
    [Authorize]
    [HttpGet("discounts/{planDiscountCode}")]
    [ProducesResponseType<SubscriptionPlanDiscountResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByDiscountCode(string planDiscountCode)
    {
        var planDiscountDto = await _subscriptionPlanDiscountService.GetByPlanDiscountCodeAsync(planDiscountCode);
        var planDiscountResponse = Mapper.Map<SubscriptionPlanDiscountResponse>(planDiscountDto);
        return CreateApiResponse(new DefaultApiResponse<SubscriptionPlanDiscountResponse>(planDiscountResponse, "Success"));
    }

    /// <summary>
    /// Deletes a promo discount by its ID.
    /// </summary>
    /// <param name="planDiscountCode">The code of the discount to delete.</param>
    /// <returns>No content if the deletion is successful.</returns>
    [Authorize]
    [HttpDelete("discounts/{planDiscountCode}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(string planDiscountCode)
    {
        await _subscriptionPlanDiscountService.DeleteAsync(planDiscountCode);
        return CreateApiResponse(new DefaultApiResponse<string>(string.Empty, "Promotional subscription plan discount deleted successfully."));
    }
}