# Configuration Profiles Guide

📖 **Documentation Index:** [Return to Solution Items README](../README.md)

---

## Overview

The TintTrack CRM backend supports three standard ASP.NET Core configuration profiles for different deployment environments:

1. **Development** - Local development
2. **Staging** - QA/Staging environment
3. **Production** - Production environment

## Configuration Files

### Base Configuration
- `appsettings.json` - Base configuration with default values

### Environment-Specific Configuration
- `appsettings.Development.json` - Development/Local overrides
- `appsettings.Staging.json` - Staging/QA overrides
- `appsettings.Production.json` - Production overrides

## How It Works

ASP.NET Core automatically loads configuration files based on the `ASPNETCORE_ENVIRONMENT` variable:

1. **Base:** `appsettings.json` is always loaded first
2. **Environment-Specific:** `appsettings.{Environment}.json` is loaded and overrides base values
3. **Environment Variables:** Override JSON values (highest priority)

### Configuration Loading Order (Priority: Highest to Lowest)

1. Environment Variables
2. `appsettings.{Environment}.json`
3. `appsettings.json`

## Connection String Names

All connection strings use the simplified naming convention:

- `TintTrackCRMMasterConnection` - Master/Platform database
- `TintTrackCRMTenantConnection` - Default tenant database (for design-time migrations)

**Note:** Runtime tenant connections are built dynamically using `TenantConnStrTemplate`.

## Profile Details

### Development Profile

**Environment Variable:** `ASPNETCORE_ENVIRONMENT=Development`

**Configuration File:** `appsettings.Development.json`

**Characteristics:**
- Local SQL Server Express
- Verbose logging (Debug level)
- EF Core query logging enabled
- Swagger enabled
- Short token expiration (2 minutes)
- HTTPS not required

**Usage:**
```bash
# Set environment variable
$env:ASPNETCORE_ENVIRONMENT="Development"

# Or use launchSettings.json profile
dotnet run --launch-profile Development
```

### Staging Profile

**Environment Variable:** `ASPNETCORE_ENVIRONMENT=Staging`

**Configuration File:** `appsettings.Staging.json`

**Characteristics:**
- Staging/QA SQL Server
- Moderate logging (Information level)
- Swagger enabled (for testing)
- Medium token expiration (30 minutes)
- HTTPS required
- Staging-specific domains

**Usage:**
```bash
# Set environment variable
$env:ASPNETCORE_ENVIRONMENT="Staging"

# Or use launchSettings.json profile
dotnet run --launch-profile Staging
```

### Production Profile

**Environment Variable:** `ASPNETCORE_ENVIRONMENT=Production`

**Configuration File:** `appsettings.Production.json`

**Characteristics:**
- Production SQL Server
- Minimal logging (Warning level)
- Swagger disabled
- Long token expiration (60 minutes)
- HTTPS required
- Production domains
- Sensitive values should use Key Vault or environment variables

**Usage:**
```bash
# Set environment variable
$env:ASPNETCORE_ENVIRONMENT="Production"

# Or use launchSettings.json profile
dotnet run --launch-profile Production
```

## Launch Profiles

The `launchSettings.json` file defines three profiles:

```json
{
  "profiles": {
    "Development": {
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "Staging": {
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Staging"
      }
    },
    "Production": {
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Production"
      }
    }
  }
}
```

### Using Launch Profiles

**Visual Studio:**
- Select profile from dropdown in toolbar
- Or use Debug → Properties → Launch Profiles

**Command Line:**
```bash
dotnet run --launch-profile Development
dotnet run --launch-profile Staging
dotnet run --launch-profile Production
```

**VS Code:**
- Configure in `.vscode/launch.json`
- Select profile from debug dropdown

## Configuration Override Examples

### Example 1: Development Profile

**appsettings.json:**
```json
{
  "ConnectionStrings": {
    "TintTrackCRMMasterConnection": "Server=default;Database=Master;..."
  },
  "ApplicationSettings": {
    "AccessTokenExpiryAgeInMinutes": 60
  }
}
```

**appsettings.Development.json:**
```json
{
  "ConnectionStrings": {
    "TintTrackCRMMasterConnection": "Server=work-pc\\sqlexpress;Database=Master-DEV;..."
  },
  "ApplicationSettings": {
    "AccessTokenExpiryAgeInMinutes": 2
  }
}
```

**Result:** Uses Development connection string and 2-minute token expiration.

### Example 2: Environment Variable Override

**appsettings.Production.json:**
```json
{
  "ConnectionStrings": {
    "TintTrackCRMMasterConnection": "Server=prod-server;Database=Master-PROD;Password=PLACEHOLDER;..."
  }
}
```

**Environment Variable:**
```bash
ConnectionStrings__TintTrackCRMMasterConnection="Server=prod-server;Database=Master-PROD;Password=actual-password;..."
```

**Result:** Uses environment variable value (highest priority).

## Database Naming Convention

Each environment uses a specific database naming pattern:

- **Development:** `WTE.TintTrackCRM.{DatabaseName}-DEV`
- **Staging:** `WTE.TintTrackCRM.{DatabaseName}-STAGING`
- **Production:** `WTE.TintTrackCRM.{DatabaseName}-PROD`

**Examples:**
- Master: `WTE.TintTrackCRM.Master-DEV` / `-STAGING` / `-PROD`
- Tenant: `WTE.TintTrackCRM.{TENANTCODE}-DEV` / `-STAGING` / `-PROD`

## Security Considerations

### Development Profile
- ✅ Can use local SQL Server with Windows Authentication
- ✅ Can disable HTTPS
- ✅ Can use short-lived tokens
- ⚠️ Never commit actual credentials

### Staging Profile
- ✅ Use staging-specific credentials
- ✅ Enable HTTPS
- ⚠️ Use separate staging credentials (not production)
- ⚠️ Don't commit actual credentials

### Production Profile
- ✅ **MUST** use environment variables or Azure Key Vault
- ✅ **MUST** enable HTTPS
- ✅ **MUST** use strong, long-lived tokens
- ✅ **MUST** disable Swagger
- ⚠️ **NEVER** commit production credentials

## Setting Up Configuration Files

### 1. Copy Template Files

```bash
# Production file already exists as appsettings.Production.json
# Update with actual values (or use environment variables)
```

### 2. Configure Environment Variables

**Windows (PowerShell):**
```powershell
$env:ASPNETCORE_ENVIRONMENT="Staging"
$env:ConnectionStrings__TintTrackCRMMasterConnection="Server=staging-server;..."
```

**Linux/Mac:**
```bash
export ASPNETCORE_ENVIRONMENT="Staging"
export ConnectionStrings__TintTrackCRMMasterConnection="Server=staging-server;..."
```

**Azure App Service:**
- Go to Configuration → Application Settings
- Add `ASPNETCORE_ENVIRONMENT` = `Staging`
- Add connection strings as separate settings

### 3. Use Azure Key Vault (Production)

```csharp
// In Program.cs
if (!context.HostingEnvironment.IsDevelopment())
{
    var vaultUri = builtConfig["AzureKeyVault:SecretsVaultUri"];
    if (!string.IsNullOrEmpty(vaultUri))
        config.AddAzureKeyVault(new Uri(vaultUri), new DefaultAzureCredential());
}
```

## Troubleshooting

### Issue: Configuration not loading

**Symptom:** Application uses base `appsettings.json` values

**Solution:**
- Verify `ASPNETCORE_ENVIRONMENT` is set correctly
- Check that `appsettings.{Environment}.json` exists
- Verify file naming matches exactly (case-sensitive)

### Issue: Connection string not found

**Symptom:** `InvalidOperationException: Connection string 'TintTrackCRMMasterConnection' is not found`

**Solution:**
- Verify connection string name matches exactly (case-sensitive)
- Check that connection string exists in appropriate `appsettings.{Environment}.json`
- Verify environment variable is set correctly

### Issue: Wrong environment loaded

**Symptom:** Application uses wrong configuration

**Solution:**
- Check `ASPNETCORE_ENVIRONMENT` value
- Verify launch profile settings
- Check for conflicting environment variables

## Best Practices

1. **Never commit sensitive data** - Use environment variables or Key Vault
2. **Use templates** - Keep `.example` files for reference
3. **Validate configuration** - Test each profile before deployment
4. **Document overrides** - Document any environment-specific requirements
5. **Use consistent naming** - Follow database naming conventions
6. **Test locally** - Test with Development profile before deploying

## Related Documentation

- [Configuration Best Practices](CONFIGURATION_BEST_PRACTICES.md)
- [Configuration Management](CONFIGURATION_MANAGEMENT.md)
- [Tenant Migration Strategy](TENANT_MIGRATION_STRATEGY.md)

---

*Last Updated: 2025-01-19*

