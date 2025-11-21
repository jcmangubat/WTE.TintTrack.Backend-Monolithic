using System.Net;
using System.Security.Claims;
using WTE.TintTrack.Common.Interfaces;

namespace WTE.TintTrack.Api.Middlewares;

/// <summary>
/// Middleware for rate limiting API requests
/// </summary>
public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RateLimitingMiddleware> _logger;
    private const int DefaultMaxRequests = 100;
    private static readonly TimeSpan DefaultWindow = TimeSpan.FromMinutes(1);

    public RateLimitingMiddleware(
        RequestDelegate next,
        ILogger<RateLimitingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip rate limiting for health checks and Swagger
        if (context.Request.Path.StartsWithSegments("/health") ||
            context.Request.Path.StartsWithSegments("/swagger") ||
            context.Request.Path.Value == "/")
        {
            await _next(context).ConfigureAwait(false);
            return;
        }

        // Resolve IRateLimiter from request scope (not constructor injection, as it's scoped)
        var rateLimiter = context.RequestServices.GetService<IRateLimiter>();
        
        if (rateLimiter == null)
        {
            // Rate limiter not available, skip rate limiting
            await _next(context).ConfigureAwait(false);
            return;
        }

        // Get rate limit key (user ID, IP address, or tenant code)
        var rateLimitKey = GetRateLimitKey(context);

        if (!string.IsNullOrEmpty(rateLimitKey))
        {
            var isRateLimited = await rateLimiter.IsRateLimitedAsync(rateLimitKey, DefaultMaxRequests, DefaultWindow).ConfigureAwait(false);

            if (isRateLimited)
            {
                var remaining = await rateLimiter.GetRemainingRequestsAsync(rateLimitKey, DefaultMaxRequests, DefaultWindow).ConfigureAwait(false);
                
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                context.Response.Headers["X-RateLimit-Limit"] = DefaultMaxRequests.ToString();
                context.Response.Headers["X-RateLimit-Remaining"] = remaining.ToString();
                context.Response.Headers["X-RateLimit-Reset"] = DateTime.UtcNow.Add(DefaultWindow).ToString("R");
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
                {
                    Success = false,
                    Message = "Rate limit exceeded. Please try again later.",
                    StatusCode = (int)HttpStatusCode.TooManyRequests,
                    CorrelationId = context.Items["CorrelationId"]?.ToString()
                })).ConfigureAwait(false);

                return;
            }

            // Add rate limit headers
            var remainingRequests = await rateLimiter.GetRemainingRequestsAsync(rateLimitKey, DefaultMaxRequests, DefaultWindow).ConfigureAwait(false);
            context.Response.Headers["X-RateLimit-Limit"] = DefaultMaxRequests.ToString();
            context.Response.Headers["X-RateLimit-Remaining"] = remainingRequests.ToString();
        }

        await _next(context).ConfigureAwait(false);
    }

    private static string? GetRateLimitKey(HttpContext context)
    {
        // Try to get user ID from claims
        var userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrEmpty(userId))
            return $"user:{userId}";

        // Try to get tenant code
        var tenantContext = context.RequestServices.GetService<ITenantContext>();
        if (tenantContext != null && !string.IsNullOrEmpty(tenantContext.TenantCode))
            return $"tenant:{tenantContext.TenantCode}";

        // Fallback to IP address
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();
        if (!string.IsNullOrEmpty(ipAddress))
            return $"ip:{ipAddress}";

        return null;
    }
}

