# Tenant Database Migration Strategy

📖 **Documentation Index:** [Return to Solution Items README](../README.md)

---

## Overview

This document describes the migration strategy for managing database migrations across all tenant databases in a one-database-per-tenant architecture.

## Architecture

- **Master Database (ApplicationDbContext):** Single shared database containing tenant metadata
- **Tenant Databases (TenantDbContext):** One database per tenant, dynamically resolved using `TenantConnStrTemplate`

## Migration Service

### TenantMigrationService

A dedicated service (`WTE.TintTrack.Api/Helpers/TenantMigrationService.cs`) handles all tenant database migrations with the following features:

#### Key Features

1. **Dynamic Tenant Discovery**
   - Automatically fetches all tenants from `ApplicationDbContext` using `ITenantService.GetAllAsync()`
   - No hardcoded tenant codes required
   - Scales automatically as new tenants are added

2. **Robust Error Handling**
   - Per-tenant error handling (one failure doesn't stop others)
   - Configurable `continueOnError` flag
   - Detailed error logging and reporting

3. **Comprehensive Logging**
   - Logs migration start/completion for each tenant
   - Tracks applied migrations per tenant
   - Provides summary statistics (success count, failure count, duration)

4. **Result Tracking**
   - `MigrationResult` class tracks overall migration status
   - `TenantMigrationResult` class tracks individual tenant results
   - Includes timing information and applied migration lists

## Usage

### Automatic Migration (Application Startup)

Migrations run automatically during application startup via `DatabaseInitializer`:

```csharp
// In DatabaseInitializer.InitializeAsync()
await InitializeTenantsAsync(serviceProvider, appSettings);
```

This will:
1. Fetch all tenants from `ApplicationDbContext`
2. Migrate each tenant database sequentially
3. Log results and continue even if some tenants fail

### Manual Migration (Programmatic)

You can also use the service programmatically:

```csharp
// Inject TenantMigrationService
var migrationService = serviceProvider.GetRequiredService<TenantMigrationService>();

// Migrate all tenants
var result = await migrationService.MigrateAllTenantsAsync(continueOnError: true);

// Migrate specific tenant
var tenantResult = await migrationService.MigrateTenantAsync("TENANT001");

// Check pending migrations for a tenant
var pending = await migrationService.GetPendingMigrationsAsync("TENANT001");
```

## Migration Flow

```
Application Startup
    ↓
DatabaseInitializer.InitializeAsync()
    ↓
1. Migrate ApplicationDbContext (master database)
    ↓
2. Seed ApplicationDbContext
    ↓
3. InitializeTenantsAsync()
    ↓
   TenantMigrationService.MigrateAllTenantsAsync()
       ↓
   Fetch all tenants from ApplicationDbContext
       ↓
   For each tenant:
       ├─ Build connection string from TenantConnStrTemplate
       ├─ Create TenantDbContext with tenant connection
       ├─ Check database connectivity
       ├─ Get pending migrations
       ├─ Apply migrations (if any)
       └─ Log result
       ↓
   Return MigrationResult with summary
```

## Configuration

### Connection String Template

Configured in `appsettings.json`:

```json
{
  "ApplicationSettings": {
    "TenantConnStrTemplate": "Server=server;Database=WTE.TintTrackCRM.{TENANTCODE}-DEV;..."
  }
}
```

The `{TENANTCODE}` placeholder is replaced with the actual tenant code during migration.

### Dependency Injection

Registered in `DIExtension.cs`:

```csharp
services.AddScoped<TenantMigrationService>();
```

## Error Handling

### Per-Tenant Errors

- If a tenant database cannot be connected, it's logged and skipped
- If migration fails for one tenant, others continue (when `continueOnError: true`)
- Each tenant's result is tracked independently

### Fatal Errors

- If tenant discovery fails, the entire process stops
- If `continueOnError: false` and a tenant fails, the process stops

## Logging

### Log Levels

- **Information:** Migration start, completion, summary statistics
- **Warning:** Tenant connection failures, partial failures
- **Error:** Migration failures for specific tenants
- **Debug:** Detailed migration status (when tenant is up to date)

### Example Log Output

```
[Information] Starting migration for all tenant databases
[Information] Found 5 tenants to migrate
[Information] Starting migration for tenant: TENANT001
[Information] Applying 2 pending migration(s) to tenant TENANT001: Migration1, Migration2
[Information] Successfully migrated tenant TENANT001. Applied migrations: Migration1, Migration2
[Warning] Cannot connect to database for tenant: TENANT002. Skipping.
[Information] Migration completed. Success: 4, Failed: 1, Duration: 1234ms
[Warning] Failed to migrate tenant TENANT002: Cannot connect to database
```

## Best Practices

### 1. Always Use Dynamic Discovery

✅ **Good:**
```csharp
var tenants = await _tenantService.GetAllAsync();
foreach (var tenant in tenants) { ... }
```

❌ **Bad:**
```csharp
var tenantCodes = new List<string> { "TENANT1", "TENANT2" };
```

### 2. Handle Errors Gracefully

✅ **Good:**
```csharp
var result = await migrationService.MigrateAllTenantsAsync(continueOnError: true);
if (!result.Success)
{
    // Log failures but don't crash
    logger.LogWarning("Some tenants failed: {Failures}", result.FailureCount);
}
```

### 3. Log Migration Status

✅ **Good:**
```csharp
logger.LogInformation("Migrated {Count} tenants successfully", result.SuccessCount);
```

### 4. Validate Connection Strings

The service automatically validates:
- Connection string template is configured
- Database connectivity before migration
- Pending migrations exist before applying

## Design-Time Migrations

For creating new migrations using EF Core CLI:

```bash
dotnet ef migrations add MigrationName \
  --context TenantDbContext \
  --startup-project "WTE.TintTrack.Api/WTE.TintTrack.Api.csproj" \
  --project "_Business/WTE.TintTrack.Business.Infrastructure/WTE.TintTrack.Business.Infrastructure.csproj"
```

**Note:** Design-time migrations use the default connection string from `appsettings.json` (`TintTrackCRMTenantConnection`). This is fine for creating migrations, but you must apply them to all tenant databases separately.

## CI/CD Integration

### Azure DevOps / GitHub Actions

```yaml
- task: DotNetCoreCLI@2
  displayName: 'Run Tenant Migrations'
  inputs:
    command: 'run'
    projects: '**/WTE.TintTrack.Api.csproj'
    arguments: 'migrate-tenants --all --continue-on-error'
```

### Manual Deployment Script

```powershell
# Run migrations for all tenants
dotnet run --project WTE.TintTrack.Api -- migrate-tenants --all
```

## Troubleshooting

### Issue: No tenants found

**Symptom:** Log shows "No tenants found in database"

**Solution:** Ensure tenants are registered in `ApplicationDbContext` before running migrations

### Issue: Cannot connect to tenant database

**Symptom:** "Cannot connect to database for tenant: {TenantCode}"

**Solution:** 
- Verify tenant database exists
- Check connection string template is correct
- Verify network connectivity and credentials

### Issue: Migration fails for specific tenant

**Symptom:** One tenant fails while others succeed

**Solution:**
- Check tenant-specific database state
- Verify migration scripts are compatible with tenant's schema
- Review error logs for specific error details

## Future Enhancements

Potential improvements:

1. **Parallel Migrations:** Run migrations in parallel with concurrency limits
2. **Migration Rollback:** Support rolling back migrations for specific tenants
3. **Dry-Run Mode:** Preview migrations without applying them
4. **Migration History:** Track migration history per tenant
5. **Selective Migration:** Migrate only tenants matching specific criteria
6. **Web UI:** Admin interface for managing tenant migrations

## Related Files

- `WTE.TintTrack.Api/Helpers/TenantMigrationService.cs` - Migration service implementation
- `WTE.TintTrack.Api/Helpers/DatabaseInitializer.cs` - Application startup initialization
- `WTE.TintTrack.Api/Helpers/Extensions/DIExtension.cs` - Service registration
- `_Business/WTE.TintTrack.Business.Infrastructure/TenantDbContextFactory.cs` - Design-time factory

---

*Last Updated: 2025-01-19*

