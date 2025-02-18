namespace WTE.TintTrack.Api.Messaging.Core.Responses;

/// <summary>
/// Represents the response containing user and tenant details extracted from an access token.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UserFromAccessTokenResponse"/> class.
/// </remarks>
/// <param name="user">The user details retrieved from the access token.</param>
/// <param name="tenant">The tenant details retrieved from the access token, or null if no tenant is associated.</param>
/// <param name="roles"></param>
/// <param name="permissions"></param>
public class UserFromAccessTokenResponse(UserResponse user, TenantResponse? tenant, IEnumerable<string> roles, IEnumerable<string> permissions)
{

    /// <summary>
    /// Gets the user details extracted from the access token.
    /// </summary>
    public UserResponse User { get; } = user;

    /// <summary>
    /// Gets the tenant details extracted from the access token, or null if no tenant is associated.
    /// </summary>
    public TenantResponse? Tenant { get; } = tenant;

    public IEnumerable<string> Roles { get; } = roles;
    public IEnumerable<string> Permissions { get; } = permissions;

    /// <summary>
    /// Gets or sets a value indicating whether the token is valid.
    /// </summary>
    //public bool TokenIsValid { get; set; }
}