using WTE.TintTrack.Common.Interfaces;

namespace WTE.TintTrack.Api.Middlewares;

/// <summary>
/// Middleware to resolve tenant context early in the request pipeline
/// </summary>
public class TenantContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TenantContextMiddleware> _logger;

    public TenantContextMiddleware(RequestDelegate next, ILogger<TenantContextMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, ITenantContext tenantContext)
    {
        try
        {
            // Resolve tenant context early in the pipeline
            await tenantContext.ResolveAsync();

            // Continue to next middleware
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in tenant context middleware");
            throw;
        }
    }
}

