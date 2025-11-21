using System.Diagnostics;

namespace WTE.TintTrack.Api.Middlewares;

/// <summary>
/// Middleware to add correlation ID to requests for distributed tracing
/// </summary>
public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private const string CorrelationIdHeaderName = "X-Correlation-Id";

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Get correlation ID from header or generate new one
        var correlationId = context.Request.Headers[CorrelationIdHeaderName].FirstOrDefault()
                          ?? Guid.NewGuid().ToString();

        // Add to response headers
        context.Response.Headers[CorrelationIdHeaderName] = correlationId;

        // Add to trace context for logging
        Activity.Current?.SetTag("CorrelationId", correlationId);
        
        // Store in HttpContext.Items for access throughout the request pipeline
        context.Items["CorrelationId"] = correlationId;

        // Add to scope for structured logging
        using (context.RequestServices.GetRequiredService<ILogger<CorrelationIdMiddleware>>()
            .BeginScope(new Dictionary<string, object> { ["CorrelationId"] = correlationId }))
        {
            await _next(context);
        }
    }
}

