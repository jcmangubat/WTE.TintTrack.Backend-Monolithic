using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

namespace WTE.TintTrack.Core.Application.Interfaces;

public interface ISubscriptionPlanFeatureService : IMappedLoggingService<ISubscriptionPlanFeatureService>
{
    /// <summary>
    /// Retrieves all subscription plan features associated with a specific subscription plan.
    /// </summary>
    /// <param name="planCode">The code of the subscription plan.</param>
    /// <returns>A collection of subscription plan feature DTOs.</returns>
    Task<IEnumerable<SubscriptionPlanFeatureDto>> GetFeaturesBySubscriptionPlanAsync(string planCode);

    /// <summary>
    /// Retrieves a subscription plan feature by its ID.
    /// </summary>
    /// <param name="featureId">The ID of the subscription plan feature.</param>
    /// <returns>The subscription plan feature DTO if found; otherwise, null.</returns>
    Task<SubscriptionPlanFeatureDto?> GetSubscriptionPlanFeatureAsync(Guid planFeatureId);

    Task<SubscriptionPlanFeatureDto?> GetSubscriptionPlanFeatureAsync(string planFeatureCode);

    /// <summary>
    /// Deletes a subscription plan feature by its ID.
    /// </summary>
    /// <param name="planFeatureId">The ID of the subscription plan feature to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteFeatureAsync(Guid planFeatureId);

    /// <summary>
    /// Deletes a subscription plan feature by its ID.
    /// </summary>
    /// <param name="planFeatureCode">The code of the subscription plan feature to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteFeatureAsync(string planFeatureCode);

    Task RemoveFeatureFromPlan(string planCode, string planFeatureCode);
    Task<SubscriptionPlanFeatureAssociationDto> FindPlanFeatureAssociation(string planCode, string planFeatureCode);
    Task AddFeatureToPlan(string planCode, string featureCode);
}
