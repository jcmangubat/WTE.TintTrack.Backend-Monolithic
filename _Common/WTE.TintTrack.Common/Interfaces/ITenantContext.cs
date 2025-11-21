namespace WTE.TintTrack.Common.Interfaces;

/// <summary>
/// Tenant context interface providing tenant information for the current request
/// </summary>
public interface ITenantContext
{
    /// <summary>
    /// Gets the tenant code for the current request
    /// </summary>
    string? TenantCode { get; }

    /// <summary>
    /// Gets the tenant ID for the current request
    /// </summary>
    Guid? TenantId { get; }

    /// <summary>
    /// Gets the tenant database connection string for the current request
    /// </summary>
    string? TenantConnectionString { get; }

    /// <summary>
    /// Indicates whether tenant context has been resolved
    /// </summary>
    bool IsResolved { get; }

    /// <summary>
    /// Resolves the tenant context from the current HTTP request
    /// </summary>
    Task ResolveAsync();

    /// <summary>
    /// Validates that tenant context is resolved, throws exception if not
    /// </summary>
    void EnsureResolved();
}

