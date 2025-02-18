using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

namespace WTE.TintTrack.Core.Application.Interfaces;

public interface ISubscriptionPlanService : IMappedLoggingService<ISubscriptionPlanService>
{
    /// <summary>
    /// Retrieves all subscription plans.
    /// </summary>
    /// <returns>A collection of subscription plan DTOs.</returns>
    Task<IEnumerable<SubscriptionPlanDto>> GetAllAsync(bool excludeInActives = true);

    /// <summary>
    /// Retrieves a subscription plan by its ID.
    /// </summary>
    /// <param name="id">The ID of the subscription plan.</param>
    /// <returns>The subscription plan DTO if found; otherwise, null.</returns>
    Task<SubscriptionPlanDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Deletes a subscription plan by its ID.
    /// </summary>
    /// <param name="id">The ID of the subscription plan to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteSubscriptionPlanAsync(Guid id);

    Task<SubscriptionPlanDto> CreateAsync(SubscriptionPlanDto subscriptionPlanDto);

    Task<SubscriptionPlanDto> UpdateAsync(Guid id, SubscriptionPlanDto subscriptionPlanDto);
    Task<SubscriptionPlanDto> UpdateAsync(string planCode, SubscriptionPlanDto subscriptionPlanDto);
    Task<SubscriptionPlanDto> GetByPlanCodeAsync(string planCode);
}
