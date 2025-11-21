# Configuration Management Best Practices

📖 **Documentation Index:** [Return to Solution Items README](../README.md)

---

## Overview

This document outlines best practices for managing application configuration in the TintTrack CRM backend.

## Configuration Files

### appsettings.json
Base configuration file. Contains default values for all environments.

### appsettings.Development.json
Development-specific overrides. Automatically loaded when `ASPNETCORE_ENVIRONMENT=Development`.

### appsettings.Staging.json
Staging/QA-specific overrides. Automatically loaded when `ASPNETCORE_ENVIRONMENT=Staging`.

### appsettings.Production.json
Production-specific overrides. Automatically loaded when `ASPNETCORE_ENVIRONMENT=Production`. **DO NOT commit actual production credentials - use environment variables or Key Vault.**

## Security Best Practices

### ❌ Never Commit Sensitive Data

**Bad:**
```json
{
  "Jwt": {
    "Key": "FZ7R@0n!q4XPm8n3l^A0YvZ9F6J1kO!9x&%2y*"
  },
  "Smtp": {
    "Password": "$r3Bt169k"
  }
}
```

### ✅ Use User Secrets (Development)

```bash
# Set user secret
dotnet user-secrets set "Jwt:Key" "your-secret-key"
dotnet user-secrets set "Smtp:Password" "your-password"
```

### ✅ Use Environment Variables (Production)

```bash
# Set environment variable
export Jwt__Key="your-secret-key"
export Smtp__Password="your-password"
```

### ✅ Use Azure Key Vault (Azure Deployments)

```csharp
// In Program.cs or Startup.cs
builder.Configuration.AddAzureKeyVault(
    vaultUri: "https://your-keyvault.vault.azure.net/",
    credential: new DefaultAzureCredential());
```

## Configuration Sections

### ConnectionStrings

**Purpose:** Database connection strings for master and tenant databases.

**Development:** Use local SQL Server Express or LocalDB
**Production:** Use Azure SQL Database or production SQL Server

**Note:** `TintTrackCRMTenantConnection` is used for design-time migrations only. Runtime uses `TenantConnStrTemplate`.

### ApplicationSettings

**Key Settings:**
- `TenantConnStrTemplate`: Template for building tenant-specific connection strings
- `AccessTokenExpiryAgeInMinutes`: JWT token expiration
- `EnableSwaggerInProd`: Enable Swagger UI in production (default: false)

### Jwt

**Security:** Store `Key` in User Secrets or environment variables in production.

**Settings:**
- `Issuer`: Token issuer URL
- `Audience`: Token audience
- `Key`: **SENSITIVE** - Use secrets management
- `TokenExpirationMinutes`: Access token lifetime

### IdentityServer

**Security:** Store `ClientSecret` in User Secrets or environment variables.

**Settings:**
- `Authority`: IdentityServer URL
- `RequireHttpsMetadata`: Require HTTPS (true in production)
- `ClientId`: API client identifier
- `ClientSecret`: **SENSITIVE** - Use secrets management

### Smtp

**Security:** Store `Password` in User Secrets or environment variables.

**Settings:**
- `Host`: SMTP server hostname
- `Port`: SMTP port (587 for TLS, 465 for SSL)
- `Username`: SMTP username
- `Password`: **SENSITIVE** - Use secrets management

### SmartyStreets

**Security:** Store `AuthId` and `AuthToken` in User Secrets or environment variables.

### ImageKitIO

**Security:** Store private keys in User Secrets or environment variables.

**Settings:**
- `StandardPrivateKey`: **SENSITIVE**
- `RestrictedPrivateKey`: **SENSITIVE**

## Environment-Specific Configuration

### Development

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "ApplicationSettings": {
    "AccessTokenExpiryAgeInMinutes": 2,
    "EnableSwaggerInProd": true
  }
}
```

### Production

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "ApplicationSettings": {
    "AccessTokenExpiryAgeInMinutes": 60,
    "EnableSwaggerInProd": false
  },
  "Jwt": {
    "RequireHttpsMetadata": true
  }
}
```

## Configuration Hierarchy

Configuration is loaded in the following order (later values override earlier ones):

1. `appsettings.json` (base configuration)
2. `appsettings.{Environment}.json` (environment-specific)
3. User Secrets (development only)
4. Environment Variables
5. Command-line arguments

## Tenant Connection String Template

The `TenantConnStrTemplate` uses `{TENANTCODE}` placeholder:

```json
{
  "ApplicationSettings": {
    "TenantConnStrTemplate": "Server=server;Database=WTE.TintTrackCRM.{TENANTCODE}-DEV;..."
  }
}
```

At runtime, `{TENANTCODE}` is replaced with the actual tenant code (e.g., `WTE001`, `DEFAULTCLIENT`).

## Migration Guide

### Moving from Hardcoded Values to Secrets

1. **Identify sensitive values:**
   - Passwords
   - API keys
   - Connection strings with credentials
   - JWT signing keys

2. **Use User Secrets (Development):**
   ```bash
   dotnet user-secrets init
   dotnet user-secrets set "Jwt:Key" "your-key"
   dotnet user-secrets set "Smtp:Password" "your-password"
   ```

3. **Use Environment Variables (Production):**
   ```bash
   export Jwt__Key="your-key"
   export Smtp__Password="your-password"
   ```

4. **Remove from appsettings.json:**
   - Remove sensitive values
   - Add placeholder comments if needed

## Checklist

- [ ] No passwords in committed files
- [ ] No API keys in committed files
- [ ] No connection strings with credentials in committed files
- [ ] User Secrets configured for development
- [ ] Environment variables configured for production
- [ ] Production configuration template exists (`.example` file)
- [ ] `.gitignore` excludes `appsettings.Production.json`
- [ ] Documentation updated with configuration requirements

## Example: Secure Configuration Setup

### Development (appsettings.Development.json)
```json
{
  "Jwt": {
    "Key": "development-key-only"
  }
}
```

### User Secrets (secrets.json - not committed)
```json
{
  "Jwt:Key": "actual-development-key",
  "Smtp:Password": "actual-smtp-password"
}
```

### Production (Environment Variables)
```bash
Jwt__Key=production-key-from-secure-store
Smtp__Password=production-password-from-secure-store
```

## Related Documentation

- [Configuration Management](CONFIGURATION_MANAGEMENT.md)
- [Tenant Migration Strategy](TENANT_MIGRATION_STRATEGY.md)

---

*Last Updated: 2025-01-19*

