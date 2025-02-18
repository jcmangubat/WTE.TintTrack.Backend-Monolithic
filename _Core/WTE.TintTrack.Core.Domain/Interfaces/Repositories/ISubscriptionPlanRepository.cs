using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Domain.Interfaces.Repositories;

public interface ISubscriptionPlanRepository : IRepositoryForKeyedEntity<SubscriptionPlan, Guid>
{
    /// <summary>
    /// Gets all subscription plans.
    /// </summary>
    /// <returns>A list of subscription plans.</returns>
    Task<IEnumerable<SubscriptionPlan>> GetAllAsync();

    /// <summary>
    /// Finds a subscription plan by its ID.
    /// </summary>
    /// <param name="id">The ID of the subscription plan.</param>
    /// <returns>The subscription plan if found; otherwise, null.</returns>
    Task<SubscriptionPlan?> GetByIdAsync(Guid id);

    /*/// <summary>
    /// Deletes a subscription plan by its ID.
    /// </summary>
    /// <param name="id">The ID of the subscription plan.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task DeleteAsync(Guid id);*/
}
