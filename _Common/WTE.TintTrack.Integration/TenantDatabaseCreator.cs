using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Business.Infrastructure;
using WTE.TintTrack.Common.Interfaces;

namespace WTE.TintTrack.Integration;

/// <summary>
/// Service for creating and provisioning tenant databases
/// </summary>
public class TenantDatabaseCreator : ITenantDatabaseCreator
{
    private readonly ILogger<TenantDatabaseCreator>? _logger;

    public TenantDatabaseCreator(ILogger<TenantDatabaseCreator>? logger = null)
    {
        _logger = logger;
    }

    /// <summary>
    /// Creates a tenant database and applies all migrations
    /// </summary>
    /// <param name="connectionString">The connection string for the tenant database</param>
    /// <exception cref="InvalidOperationException">Thrown when database creation fails</exception>
    public async Task CreateDatabaseAsync(string connectionString, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Connection string cannot be null or empty", nameof(connectionString));

        var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        using var context = new TenantDbContext(optionsBuilder.Options);

        try
        {
            _logger?.LogInformation("Creating tenant database...");

            // Ensure database exists (creates if it doesn't exist)
            var databaseCreated = await context.Database.EnsureCreatedAsync(cancellationToken).ConfigureAwait(false);
            
            if (databaseCreated)
            {
                _logger?.LogInformation("Database created successfully. Applying migrations...");
            }
            else
            {
                _logger?.LogInformation("Database already exists. Checking for pending migrations...");
            }

            // Apply all migrations to ensure schema is up to date
            // This is safe even if EnsureCreatedAsync was called, as MigrateAsync will only apply pending migrations
            var pendingMigrations = context.Database.GetPendingMigrations().ToList();
            
            if (pendingMigrations.Any())
            {
                _logger?.LogInformation(
                    "Applying {Count} migration(s): {Migrations}",
                    pendingMigrations.Count,
                    string.Join(", ", pendingMigrations));

                await context.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);

                _logger?.LogInformation(
                    "Successfully applied {Count} migration(s) to tenant database",
                    pendingMigrations.Count);
            }
            else
            {
                _logger?.LogInformation("No pending migrations. Database schema is up to date.");
            }

            _logger?.LogInformation("Tenant database provisioning completed successfully.");
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to create or migrate tenant database: {Error}", ex.Message);
            throw new InvalidOperationException(
                $"Failed to create or migrate tenant database: {ex.Message}",
                ex);
        }
    }
}