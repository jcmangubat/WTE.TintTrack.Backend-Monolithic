using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WTE.TintTrack.Business.Infrastructure;
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Core.Application.Interfaces;

namespace WTE.TintTrack.Api.Helpers;

/// <summary>
/// Service for managing database migrations across all tenant databases
/// </summary>
public class TenantMigrationService
{
    private readonly ITenantService _tenantService;
    private readonly ApplicationSettings _appSettings;
    private readonly ILogger<TenantMigrationService> _logger;

    public TenantMigrationService(
        ITenantService tenantService,
        IOptions<ApplicationSettings> appSettings,
        ILogger<TenantMigrationService> logger)
    {
        _tenantService = tenantService ?? throw new ArgumentNullException(nameof(tenantService));
        _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Migrates all tenant databases dynamically discovered from ApplicationDbContext
    /// </summary>
    /// <param name="continueOnError">If true, continues migrating other tenants if one fails</param>
    /// <returns>Migration result summary</returns>
    public async Task<MigrationResult> MigrateAllTenantsAsync(bool continueOnError = true)
    {
        _logger.LogInformation("Starting migration for all tenant databases");

        var result = new MigrationResult
        {
            StartTime = DateTime.UtcNow
        };

        try
        {
            // Fetch all tenants dynamically from ApplicationDbContext
            var tenants = await _tenantService.GetAllAsync();

            if (tenants == null || !tenants.Any())
            {
                _logger.LogWarning("No tenants found in database. Skipping tenant migrations.");
                result.Message = "No tenants found in database";
                result.EndTime = DateTime.UtcNow;
                return result;
            }

            _logger.LogInformation("Found {TenantCount} tenants to migrate", tenants.Count());

            foreach (var tenant in tenants)
            {
                try
                {
                    var tenantResult = await MigrateTenantAsync(tenant.TenantCode);
                    result.AddTenantResult(tenant.TenantCode, tenantResult);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Migration failed for tenant: {TenantCode}", tenant.TenantCode);
                    
                    var errorResult = new TenantMigrationResult
                    {
                        TenantCode = tenant.TenantCode,
                        Success = false,
                        ErrorMessage = ex.Message,
                        Exception = ex
                    };
                    
                    result.AddTenantResult(tenant.TenantCode, errorResult);

                    if (!continueOnError)
                    {
                        _logger.LogCritical(
                            "Stopping migration process due to error in tenant {TenantCode}",
                            tenant.TenantCode);
                        throw;
                    }
                }
            }

            result.EndTime = DateTime.UtcNow;
            result.Success = result.TenantResults.Values.All(r => r.Success);

            var successCount = result.TenantResults.Values.Count(r => r.Success);
            var failureCount = result.TenantResults.Values.Count(r => !r.Success);

            _logger.LogInformation(
                "Migration completed. Success: {SuccessCount}, Failed: {FailureCount}, Duration: {Duration}ms",
                successCount,
                failureCount,
                (result.EndTime - result.StartTime).TotalMilliseconds);

            if (failureCount > 0)
            {
                var failedTenants = result.TenantResults
                    .Where(kvp => !kvp.Value.Success)
                    .Select(kvp => $"{kvp.Key}: {kvp.Value.ErrorMessage}");
                
                result.Message = $"Completed with {failureCount} failure(s). Failed tenants: {string.Join("; ", failedTenants)}";
            }
            else
            {
                result.Message = $"Successfully migrated {successCount} tenant(s)";
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fatal error during tenant migration process");
            result.EndTime = DateTime.UtcNow;
            result.Success = false;
            result.Message = $"Fatal error: {ex.Message}";
            throw;
        }
    }

    /// <summary>
    /// Migrates a specific tenant database
    /// </summary>
    /// <param name="tenantCode">The tenant code to migrate</param>
    /// <returns>Migration result for the tenant</returns>
    public async Task<TenantMigrationResult> MigrateTenantAsync(string tenantCode)
    {
        if (string.IsNullOrWhiteSpace(tenantCode))
            throw new ArgumentException("Tenant code cannot be null or empty", nameof(tenantCode));

        _logger.LogInformation("Starting migration for tenant: {TenantCode}", tenantCode);

        var result = new TenantMigrationResult
        {
            TenantCode = tenantCode,
            StartTime = DateTime.UtcNow
        };

        try
        {
            // Build tenant-specific connection string
            if (string.IsNullOrEmpty(_appSettings.TenantConnStrTemplate))
            {
                throw new InvalidOperationException(
                    "TenantConnStrTemplate is not configured in ApplicationSettings");
            }

            var connectionString = _appSettings.TenantConnStrTemplate
                .Replace("{TENANTCODE}", tenantCode);

            // Create DbContext with tenant-specific connection
            var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using var context = new TenantDbContext(optionsBuilder.Options);

            // Check database connectivity and create if it doesn't exist
            if (!await context.Database.CanConnectAsync(CancellationToken.None).ConfigureAwait(false))
            {
                _logger.LogInformation(
                    "Database for tenant {TenantCode} does not exist. Creating database...",
                    tenantCode);

                try
                {
                    // Create database if it doesn't exist
                    var databaseCreated = await context.Database.EnsureCreatedAsync(CancellationToken.None).ConfigureAwait(false);
                    
                    if (databaseCreated)
                    {
                        _logger.LogInformation(
                            "Successfully created database for tenant {TenantCode}",
                            tenantCode);
                    }
                    else
                    {
                        // Database might exist but connection failed for another reason
                        var error = $"Cannot connect to database for tenant: {tenantCode}. Database may exist but connection failed.";
                        _logger.LogWarning(error);
                        result.Success = false;
                        result.ErrorMessage = error;
                        result.EndTime = DateTime.UtcNow;
                        return result;
                    }
                }
                catch (Exception createEx)
                {
                    var error = $"Failed to create database for tenant {tenantCode}: {createEx.Message}";
                    _logger.LogError(createEx, error);
                    result.Success = false;
                    result.ErrorMessage = error;
                    result.Exception = createEx;
                    result.EndTime = DateTime.UtcNow;
                    return result;
                }
            }

            // Get pending migrations
            var pendingMigrations = context.Database.GetPendingMigrations().ToList();

            if (pendingMigrations.Any())
            {
                _logger.LogInformation(
                    "Applying {Count} pending migration(s) to tenant {TenantCode}: {Migrations}",
                    pendingMigrations.Count,
                    tenantCode,
                    string.Join(", ", pendingMigrations));

                // Apply migrations
                await context.Database.MigrateAsync(CancellationToken.None).ConfigureAwait(false);

                result.Success = true;
                result.AppliedMigrations = pendingMigrations;
                result.Message = $"Successfully applied {pendingMigrations.Count} migration(s)";

                _logger.LogInformation(
                    "Successfully migrated tenant {TenantCode}. Applied migrations: {Migrations}",
                    tenantCode,
                    string.Join(", ", pendingMigrations));
            }
            else
            {
                result.Success = true;
                result.AppliedMigrations = new List<string>();
                result.Message = "No pending migrations. Database is up to date.";

                _logger.LogDebug(
                    "Tenant {TenantCode} is already up to date. No migrations applied.",
                    tenantCode);
            }

            result.EndTime = DateTime.UtcNow;
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error migrating tenant {TenantCode}: {Error}", tenantCode, ex.Message);
            result.Success = false;
            result.ErrorMessage = ex.Message;
            result.Exception = ex;
            result.EndTime = DateTime.UtcNow;
            throw;
        }
    }

    /// <summary>
    /// Gets the migration status for a specific tenant
    /// </summary>
    /// <param name="tenantCode">The tenant code</param>
    /// <returns>List of pending migrations</returns>
    public async Task<IEnumerable<string>> GetPendingMigrationsAsync(string tenantCode)
    {
        if (string.IsNullOrWhiteSpace(tenantCode))
            throw new ArgumentException("Tenant code cannot be null or empty", nameof(tenantCode));

        var connectionString = _appSettings.TenantConnStrTemplate
            .Replace("{TENANTCODE}", tenantCode);

        var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        using var context = new TenantDbContext(optionsBuilder.Options);

        if (!await context.Database.CanConnectAsync())
        {
            throw new InvalidOperationException(
                $"Cannot connect to database for tenant: {tenantCode}");
        }

        return context.Database.GetPendingMigrations();
    }
}

/// <summary>
/// Result of migration operation for a single tenant
/// </summary>
public class TenantMigrationResult
{
    public string TenantCode { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public Exception? Exception { get; set; }
    public List<string> AppliedMigrations { get; set; } = new();
    public string? Message { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration => EndTime - StartTime;
}

/// <summary>
/// Result of migration operation for all tenants
/// </summary>
public class MigrationResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public Dictionary<string, TenantMigrationResult> TenantResults { get; set; } = new();
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration => EndTime - StartTime;

    public int SuccessCount => TenantResults.Values.Count(r => r.Success);
    public int FailureCount => TenantResults.Values.Count(r => !r.Success);
    public int TotalTenants => TenantResults.Count;

    public void AddTenantResult(string tenantCode, TenantMigrationResult result)
    {
        TenantResults[tenantCode] = result;
    }
}

