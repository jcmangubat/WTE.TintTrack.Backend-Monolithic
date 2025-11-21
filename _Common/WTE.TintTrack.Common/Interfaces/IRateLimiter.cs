namespace WTE.TintTrack.Common.Interfaces;

/// <summary>
/// Rate limiting interface for API protection
/// </summary>
public interface IRateLimiter
{
    /// <summary>
    /// Checks if a request should be rate limited
    /// </summary>
    Task<bool> IsRateLimitedAsync(string key, int maxRequests, TimeSpan window);

    /// <summary>
    /// Gets remaining requests for a key
    /// </summary>
    Task<int> GetRemainingRequestsAsync(string key, int maxRequests, TimeSpan window);
}

