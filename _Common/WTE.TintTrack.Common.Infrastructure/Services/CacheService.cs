using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Common.Interfaces;

namespace WTE.TintTrack.Infrastructure.Shared.Services;

/// <summary>
/// In-memory cache service implementation
/// </summary>
public class CacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<CacheService> _logger;

    public CacheService(IMemoryCache memoryCache, ILogger<CacheService> logger)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null, CancellationToken cancellationToken = default) where T : class
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Cache key cannot be null or empty.", nameof(key));

        if (_memoryCache.TryGetValue(key, out T? cachedValue))
        {
            _logger.LogDebug("Cache hit for key: {Key}", key);
            return cachedValue;
        }

        _logger.LogDebug("Cache miss for key: {Key}, executing factory", key);
        var value = await factory().ConfigureAwait(false);

        if (value != null)
        {
            var options = new MemoryCacheEntryOptions();
            if (expiration.HasValue)
            {
                options.AbsoluteExpirationRelativeToNow = expiration.Value;
            }
            else
            {
                // Default expiration: 1 hour
                options.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            }

            options.Priority = CacheItemPriority.Normal;
            _memoryCache.Set(key, value, options);
        }

        return value;
    }

    public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Cache key cannot be null or empty.", nameof(key));

        _memoryCache.TryGetValue(key, out T? value);
        return Task.FromResult(value);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default) where T : class
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Cache key cannot be null or empty.", nameof(key));

        var options = new MemoryCacheEntryOptions();
        if (expiration.HasValue)
        {
            options.AbsoluteExpirationRelativeToNow = expiration.Value;
        }
        else
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
        }

        options.Priority = CacheItemPriority.Normal;
        _memoryCache.Set(key, value, options);

        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Cache key cannot be null or empty.", nameof(key));

        _memoryCache.Remove(key);
        _logger.LogDebug("Removed cache entry for key: {Key}", key);
        return Task.CompletedTask;
    }

    public Task RemoveByPatternAsync(string pattern, CancellationToken cancellationToken = default)
    {
        // Note: IMemoryCache doesn't support pattern-based removal natively
        // This is a limitation - for production, consider using Redis or IDistributedCache
        _logger.LogWarning("Pattern-based cache removal is not fully supported with IMemoryCache. Consider using IDistributedCache with Redis.");
        return Task.CompletedTask;
    }

    public Task ClearAsync(CancellationToken cancellationToken = default)
    {
        // Note: IMemoryCache doesn't support clearing all entries natively
        // This is a limitation - for production, consider using Redis or IDistributedCache
        _logger.LogWarning("Clearing all cache entries is not fully supported with IMemoryCache. Consider using IDistributedCache with Redis.");
        return Task.CompletedTask;
    }
}

