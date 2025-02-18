using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

namespace WTE.TintTrack.Core.Application.Interfaces;

public interface ISubscriptionPlanDiscountService : IMappedLoggingService<ISubscriptionPlanDiscountService>
{
    /// <summary>
    /// Retrieves all subscription promo discounts associated with a specific subscription plan.
    /// </summary>
    /// <param name="subscriptionPlanCode">The code of the subscription plan.</param>
    /// <returns>A collection of subscription promo discount DTOs.</returns>
    Task<IEnumerable<SubscriptionPlanDiscountDto>> GetBySubscriptionPlanAsync(string planCode);

    Task<IEnumerable<SubscriptionPlanDiscountDto>> GetByPlanCodeAsync(string planCode);

    Task<SubscriptionPlanDiscountDto> GetByPlanDiscountCodeAsync(string planDiscountCode);


    /// <summary>
    /// Retrieves a subscription promo discount by its ID.
    /// </summary>
    /// <param name="discountId">The ID of the discount.</param>
    /// <returns>The subscription promo discount DTO if found; otherwise, null.</returns>
    Task<SubscriptionPlanDiscountDto?> GetByIdAsync(Guid discountId);

    /// <summary>
    /// Deletes a subscription promo discount by its ID.
    /// </summary>
    /// <param name="discountId">The code of the plan discount to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteAsync(string planDiscountCode);
}
