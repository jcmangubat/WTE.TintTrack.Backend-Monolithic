using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Application.Interfaces;

public interface IUserBillingProfileService : IMappedLoggingService<IUserBillingProfileService>
{
    /// <summary>
    /// Retrieves the billing profiles for a specific user.
    /// </summary>
    /// <param name="userCode">The code of the user.</param>
    /// <returns>The user's billing profile if found; otherwise, null.</returns>
    Task<IEnumerable<UserBillingProfileDto>> GetBillingProfilesByUserCodeAsync(string userCode,
                                                    BillingProfileTypesEnum? billingProfileType = null, 
                                                    ActiveInclusionOptionsEnum? activeInclusionOption = null);

    /// <summary>
    /// Retrieves the active billing profile for a specified user.
    /// </summary>
    /// <param name="userCode">
    /// A unique identifier for the user whose billing profile is being retrieved.
    /// </param>
    /// <param name="billingProfileType">
    /// The type of billing profile to retrieve, if specified. 
    /// Defaults to null, in which case the method will retrieve the user's active billing profile 
    /// without filtering by type.
    /// </param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, 
    /// with a result of <see cref="UserBillingProfileDto"/> containing the user's active billing profile data.
    /// </returns>
    /// <remarks>
    /// This method fetches the active billing profile for a user by their unique user code. 
    /// It optionally filters the result based on the provided billing profile type.
    /// </remarks>
    Task<UserBillingProfileDto> GetActiveBillingProfileByUserCodeAsync(string userCode, BillingProfileTypesEnum? billingProfileType = null);

    /// <summary>
    /// Retrieves the billing profile for a specific user.
    /// </summary>
    /// <param name="userBillingProfileId">The Id of the user Billing Profile.</param>
    /// <returns>The user's billing profile if found; otherwise, null.</returns>
    Task<UserBillingProfileDto?> GetBillingProfileByIdAsync(Guid userBillingProfileId);

    /// <summary>
    /// Deletes a billing profile by its ID.
    /// </summary>
    /// <param name="profileId">The ID of the billing profile to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteBillingProfileAsync(Guid profileId);

    /// <summary>
    /// Creates a billing profile
    /// </summary>
    /// <param name="billingProfileDto">The data of the billing profile to create.</param>
    /// <returns>A task representing the asynchronous operation with the created user billing Profile.</returns>
    Task<UserBillingProfileDto> RegisterBillingProfileAsync(UserBillingProfileDto billingProfileDto);
}