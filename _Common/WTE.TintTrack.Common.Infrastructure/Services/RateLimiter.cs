using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Common.Interfaces;

namespace WTE.TintTrack.Infrastructure.Shared.Services;

/// <summary>
/// In-memory rate limiter implementation
/// </summary>
public class RateLimiter : IRateLimiter
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<RateLimiter> _logger;

    public RateLimiter(IMemoryCache cache, ILogger<RateLimiter> logger)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<bool> IsRateLimitedAsync(string key, int maxRequests, TimeSpan window)
    {
        var cacheKey = $"ratelimit:{key}";
        var requestCount = _cache.Get<int?>(cacheKey) ?? 0;

        if (requestCount >= maxRequests)
        {
            _logger.LogWarning("Rate limit exceeded for key: {Key}, Count: {Count}, Max: {Max}", key, requestCount, maxRequests);
            return Task.FromResult(true);
        }

        // Increment counter
        requestCount++;
        _cache.Set(cacheKey, requestCount, window);

        return Task.FromResult(false);
    }

    public Task<int> GetRemainingRequestsAsync(string key, int maxRequests, TimeSpan window)
    {
        var cacheKey = $"ratelimit:{key}";
        var requestCount = _cache.Get<int?>(cacheKey) ?? 0;
        var remaining = Math.Max(0, maxRequests - requestCount);
        return Task.FromResult(remaining);
    }
}

