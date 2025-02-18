using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

namespace WTE.TintTrack.Core.Application.Interfaces;

public interface ITenantSubscriptionPaymentService : IMappedLoggingService<ITenantSubscriptionPaymentService>
{
    /// <summary>
    /// Retrieves all payment records for a specific tenant subscription.
    /// </summary>
    /// <param name="tenantSubscriptionId">The ID of the tenant subscription.</param>
    /// <returns>A collection of tenant subscription payment DTOs.</returns>
    Task<IEnumerable<TenantSubscriptionPaymentDto>> GetPaymentsByTenantSubscriptionAsync(Guid tenantSubscriptionId);

    /// <summary>
    /// Retrieves a specific payment record by its ID.
    /// </summary>
    /// <param name="paymentId">The ID of the payment.</param>
    /// <returns>The tenant subscription payment DTO if found; otherwise, null.</returns>
    Task<TenantSubscriptionPaymentDto?> GetPaymentByIdAsync(Guid paymentId);

    /// <summary>
    /// Deletes a payment record by its ID.
    /// </summary>
    /// <param name="paymentId">The ID of the payment to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeletePaymentAsync(Guid paymentId);
}
