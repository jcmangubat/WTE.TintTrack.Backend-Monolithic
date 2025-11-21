# Configuration Management Guide

📖 **Documentation Index:** [Return to Solution Items README](../README.md)

---

## Overview

This document describes the standardized configuration management approach using the Options pattern in the TintTrack application.

## Options Pattern

The application uses the strongly-typed Options pattern for configuration management, which provides:
- Type safety
- Configuration validation
- Centralized configuration access
- Better testability

## Configuration Classes

### ApplicationSettings
- Access token expiry settings
- Tenant connection string template
- Email configuration
- ImageKit paths
- Error messages path

### JwtSettings
- JWT key and issuer
- Token expiration settings
- Audience configuration

### SMTPSettings
- SMTP host and port
- Authentication credentials

### CorsSettings
- Allowed origins
- CORS policy configuration

## Usage

### In Startup.cs

```csharp
// Register all configuration
services.AddApplicationConfiguration(Configuration);
```

### In Services

Inject strongly-typed configuration:

```csharp
public class MyService
{
    private readonly ApplicationSettings _appSettings;
    private readonly JwtSettings _jwtSettings;
    
    public MyService(
        IOptions<ApplicationSettings> appSettings,
        IOptions<JwtSettings> jwtSettings)
    {
        _appSettings = appSettings.Value;
        _jwtSettings = jwtSettings.Value;
    }
}
```

### Direct Access (Singleton Pattern)

For frequently accessed settings, singletons are registered:

```csharp
public class MyService
{
    private readonly ApplicationSettings _appSettings;
    
    public MyService(ApplicationSettings appSettings)
    {
        _appSettings = appSettings; // Direct injection, no IOptions wrapper
    }
}
```

## Configuration Validation

Configuration is validated at startup using Data Annotations:

```csharp
services.AddOptions<ApplicationSettings>()
    .Bind(configuration.GetSection("ApplicationSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart();
```

If validation fails, the application will not start, ensuring configuration errors are caught early.

## Best Practices

1. **Always use Options pattern** - Avoid direct `IConfiguration` access
2. **Validate configuration** - Use Data Annotations or custom validators
3. **Use strongly-typed classes** - Don't use magic strings for configuration keys
4. **Register as singleton for frequently accessed** - Reduces overhead
5. **Use IOptionsSnapshot for reloadable config** - If configuration changes need to be picked up at runtime

## Configuration Files

Configuration is loaded from:
1. `appsettings.json` - Base configuration
2. `appsettings.{Environment}.json` - Environment-specific overrides
3. Environment variables - Highest priority
4. Azure Key Vault (production) - For secrets

## Migration from Direct Configuration Access

**Before:**
```csharp
var connectionString = Configuration.GetConnectionString("MyConnection");
var setting = Configuration["MySetting"];
```

**After:**
```csharp
// Create a settings class
public class MySettings
{
    public string ConnectionString { get; set; }
    public string MySetting { get; set; }
}

// Register in Startup
services.Configure<MySettings>(Configuration.GetSection("MySettings"));

// Inject in service
public MyService(IOptions<MySettings> settings)
{
    var connectionString = settings.Value.ConnectionString;
    var setting = settings.Value.MySetting;
}
```

