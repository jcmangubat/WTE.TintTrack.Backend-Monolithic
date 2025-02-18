using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers;

/// <summary>
/// Controller for handling tenant subscription payments.
/// </summary>
/// <remarks>
/// Constructor for the TenantSubscriptionPaymentController.
/// </remarks>
/// <param name="paymentService">The payment service.</param>
[ApiController]
[Route("api/[controller]")]
//[ApiExplorerSettings(GroupName = "Core Modules")]
[Produces("application/json")]
public class TenantSubscriptionPaymentController(ILogger<TenantSubscriptionPaymentController> logger, IMapper mapper, 
                                                ITenantSubscriptionPaymentService paymentService)
    : LoggingMappedControllerBase<TenantSubscriptionPaymentController>(logger, mapper)
{
    private readonly ITenantSubscriptionPaymentService _paymentService = paymentService;

    /// <summary>
    /// Retrieves all payment records for a given tenant subscription.
    /// </summary>
    /// <param name="tenantSubscriptionId">The ID of the tenant subscription.</param>
    /// <returns>A collection of tenant subscription payment DTOs.</returns>
    [HttpGet("subscription/{tenantSubscriptionId}")]
    public async Task<IEnumerable<TenantSubscriptionPaymentDto>> GetPaymentsByTenantSubscriptionAsync(Guid tenantSubscriptionId)
    {
        return await _paymentService.GetPaymentsByTenantSubscriptionAsync(tenantSubscriptionId);
    }

    /// <summary>
    /// Retrieves a specific payment record by its ID.
    /// </summary>
    /// <param name="paymentId">The ID of the payment.</param>
    /// <returns>The tenant subscription payment DTO if found; otherwise, null.</returns>
    [HttpGet("{paymentId}")]
    public async Task<TenantSubscriptionPaymentDto?> GetPaymentByIdAsync(Guid paymentId)
    {
        return await _paymentService.GetPaymentByIdAsync(paymentId);
    }

    /// <summary>
    /// Deletes a payment record by its ID.
    /// </summary>
    /// <param name="paymentId">The ID of the payment to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [HttpDelete("{paymentId}")]
    public async Task DeletePaymentAsync(Guid paymentId)
    {
        await _paymentService.DeletePaymentAsync(paymentId);
    }
}