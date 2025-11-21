# WTE TintTrack API

A multi-tenant ASP.NET Core Web API for TintTrack CRM, providing business and core domain functionality for managing tinting operations, customers, estimates, proposals, quotes, and more.

## Overview

TintTrack API is a comprehensive RESTful API built with .NET 9.0 that supports multi-tenant architecture. It provides endpoints for managing business operations (customers, contacts, estimates, proposals, quotes, inventory, tint materials) and core system functionality (authentication, tenants, subscriptions, users, permissions).

## Features

- **Multi-Tenant Architecture**: Supports multiple tenants with isolated data storage
- **Authentication & Authorization**: Duende Identity Server integration with JWT tokens
- **API Versioning**: Supports multiple API versions with header and query string versioning
- **OData Support**: Advanced querying capabilities with OData endpoints
- **Swagger/OpenAPI**: Interactive API documentation
- **Request Validation**: FluentValidation for request validation
- **Object Mapping**: AutoMapper for DTO mapping
- **Logging**: Serilog integration with file and console logging
- **Health Checks**: Database and application health monitoring
- **CORS Support**: Configurable CORS policies
- **Response Compression**: Gzip and Brotli compression support
- **Rate Limiting**: Built-in rate limiting middleware
- **Correlation ID**: Request tracking with correlation IDs
- **Tenant Context**: Automatic tenant resolution middleware

## Technology Stack

- **.NET 9.0**: Target framework
- **ASP.NET Core Web API**: Web framework
- **Entity Framework Core 9.0**: ORM with SQL Server support
- **Duende Identity Server 7.2.0**: Authentication and authorization
- **AutoMapper 14.0.0**: Object-to-object mapping
- **FluentValidation 11.3.0**: Request validation
- **Serilog 9.0.0**: Structured logging
- **Swashbuckle.AspNetCore 8.0.0**: Swagger/OpenAPI documentation
- **Microsoft.AspNetCore.OData 9.2.1**: OData query support
- **Microsoft.AspNetCore.Mvc.Versioning 5.1.0**: API versioning

## Project Structure

```
WTE.TintTrack.Api/
├── Controllers/
│   ├── Business/          # Business domain controllers
│   │   ├── AuditLogsController.cs
│   │   ├── ContactController.cs
│   │   ├── CustomerController.cs
│   │   ├── EstimateController.cs
│   │   ├── InquiryController.cs
│   │   ├── InventoryController.cs
│   │   ├── ProposalController.cs
│   │   ├── QuoteController.cs
│   │   └── TintMaterial*.cs
│   └── Core/              # Core domain controllers
│       ├── AccountController.cs
│       ├── AuthController.cs
│       ├── PermissionsController.cs
│       ├── SubscriptionPlan*.cs
│       ├── Tenant*.cs
│       └── User*.cs
├── Helpers/
│   ├── Configurations/    # Configuration helpers
│   ├── ControllerAbstractions/  # Base controller classes
│   ├── Extensions/        # Extension methods
│   └── Filters.Swagger/   # Swagger filters
├── Messaging/
│   ├── Business.Requests/ # Business request DTOs
│   ├── Business.Responses/ # Business response DTOs
│   ├── Core.Requests/     # Core request DTOs
│   ├── Core.Responses/    # Core response DTOs
│   ├── _Validators/       # FluentValidation validators
│   └── _Mappings/         # Custom mappings
├── Middlewares/
│   ├── CorrelationIdMiddleware.cs
│   ├── HttpMessagingMiddleware.cs
│   ├── RateLimitingMiddleware.cs
│   ├── TenantContextMiddleware.cs
│   └── TokenValidationMiddleware.cs
├── Program.cs             # Application entry point
├── Startup.cs             # Service configuration
└── appsettings.json       # Configuration file
```

## Prerequisites

- .NET 9.0 SDK or later
- SQL Server (local or remote)
- Visual Studio 2022 or VS Code (optional)

## Configuration

### Connection Strings

Configure database connections in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "TintTrackCRMMasterConnection": "Server=(local)\\SQLEXPRESS;Database=TintTrackCRMDb.Master;...",
    "TintTrackCRMTenantConnection": "Server=(local)\\SQLEXPRESS;Database=TintTrackCRMDb.DefaultClient;..."
  }
}
```

### Application Settings

Key settings in `appsettings.json`:

- **TenantConnStrTemplate**: Template for tenant-specific connection strings
- **TenantDomainPattern**: Regex pattern for tenant domain resolution
- **AccessTokenExpiryAgeInMinutes**: JWT token expiration time
- **RefreshTokenExpiryAgeInDays**: Refresh token expiration time
- **EnableSwaggerInProd**: Enable Swagger UI in production (default: false)

### Environment-Specific Configuration

- `appsettings.Development.json` - Development environment settings
- `appsettings.Staging.json` - Staging environment settings
- `appsettings.Production.json` - Production environment settings

### Environment Variables

The application supports `.env` files (via dotenv.net) and environment variables. Key variables:

- Database connection strings
- JWT signing keys
- Identity Server configuration
- SMTP settings
- ImageKit.IO credentials
- SmartyStreets API credentials

## Running the Application

### Development

1. Clone the repository
2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```
3. Update connection strings in `appsettings.json` or `appsettings.Development.json`
4. Run database migrations (if applicable)
5. Start the application:
   ```bash
   dotnet run
   ```
6. Navigate to `https://localhost:5001/swagger` for API documentation

### Production

1. Build the application:
   ```bash
   dotnet publish -c Release -o ./publish
   ```
2. Deploy to your hosting environment (Azure App Service, IIS, Docker, etc.)
3. Configure environment variables and connection strings
4. Ensure SSL certificates are configured

## API Documentation

### Swagger UI

When running in Development mode or when `EnableSwaggerInProd` is set to `true`, Swagger UI is available at:

- `/swagger` - Interactive API documentation

### API Versioning

The API supports versioning via:
- Header: `X-Version: 1.0`
- Query string: `?version=1.0`

Default version is `1.0`.

### Health Checks

- `/health` - Overall health check
- `/health/ready` - Readiness probe
- `/health/live` - Liveness probe

## Authentication

The API uses Duende Identity Server for authentication. To authenticate:

1. Obtain tokens from the Identity Server endpoint
2. Include the access token in the `Authorization` header:
   ```
   Authorization: Bearer <access_token>
   ```

## Key Endpoints

### Business Domain

- **Customers**: `/api/v1/customers`
- **Contacts**: `/api/v1/contacts`
- **Estimates**: `/api/v1/estimates`
- **Proposals**: `/api/v1/proposals`
- **Quotes**: `/api/v1/quotes`
- **Inventory**: `/api/v1/inventory`
- **Tint Materials**: `/api/v1/tintmaterials`

### Core Domain

- **Authentication**: `/api/v1/auth`
- **Accounts**: `/api/v1/accounts`
- **Tenants**: `/api/v1/tenants`
- **Users**: `/api/v1/users`
- **Subscriptions**: `/api/v1/subscriptions`

## Middleware Pipeline

The application uses the following middleware (in order):

1. **CorrelationIdMiddleware** - Adds correlation IDs to requests
2. **RateLimitingMiddleware** - Enforces rate limits
3. **TenantContextMiddleware** - Resolves tenant context
4. **HttpMessagingMiddleware** - Logs HTTP messages
5. **Response Compression** - Compresses responses
6. **Authentication/Authorization** - Handles security
7. **Identity Server** - Token validation
8. **Swagger** - API documentation (Development only)

## Logging

Logs are written to:
- Console output
- File: `logs/WTE.TintTrack.Core.Api-Logs.txt` (daily rolling)

Log levels can be configured in `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Dependencies

This project references several other projects in the solution:

- `WTE.TintTrack.Business.Application`
- `WTE.TintTrack.Business.Infrastructure`
- `WTE.TintTrack.Core.Application`
- `WTE.TintTrack.Core.Domain`
- `WTE.TintTrack.Core.Infrastructure`
- `WTE.TintTrack.Common.Application`
- `WTE.TintTrack.Common.Infrastructure`
- `WTE.TintTrack.Integration`

## Development Notes

- The project uses nullable reference types (`<Nullable>enable</Nullable>`)
- XML documentation is generated for Swagger
- Some controllers/files are excluded from compilation (see `.csproj` file)
- User secrets are supported in Development mode

## Troubleshooting

### Database Connection Issues

- Verify SQL Server is running
- Check connection strings in `appsettings.json`
- Ensure database exists or migrations have been run

### Authentication Issues

- Verify Identity Server is running and accessible
- Check JWT configuration in `appsettings.json`
- Ensure tokens are not expired

### CORS Issues

- Verify allowed origins in `appsettings.json`
- Check CORS policy configuration in `Startup.cs`

## License

[Add your license information here]

## Support

For support, contact: contactus@tinttrac.com

