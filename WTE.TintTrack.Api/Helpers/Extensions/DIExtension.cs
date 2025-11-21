using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Community.OData.DependencyInjection;
using System.Security.Claims;
using System.Text;
using WTE.TintTrack.Api.Helpers.Configurations;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Helpers.Filters.Swagger;
using WTE.TintTrack.Api.Messaging._CRUDExtenders;
using WTE.TintTrack.Api.Messaging._Validators.Business;
using WTE.TintTrack.Api.Messaging._Validators.Core;
using WTE.TintTrack.Api.Messaging.Business.Requests.Contact;
using WTE.TintTrack.Api.Messaging.Business.Requests.Customer;
using WTE.TintTrack.Api.Messaging.Business.Requests.Inquiry;
using WTE.TintTrack.Api.Messaging.Business.Requests.PropertyAsset;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.DTOs.PropertySpecificationModels;
using WTE.TintTrack.Business.Infrastructure;
using WTE.TintTrack.Common.Events;
using WTE.TintTrack.Common.Interfaces;
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Infrastructure.Shared.Services;
using WTE.TintTrack.Core.Application.Services;
using WTE.TintTrack.Core.Application.Validators;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Services;
using WTE.TintTrack.Core.Infrastructure;
using WTE.TintTrack.Infrastructure.Shared.Services.ImageKit;
using WTE.TintTrack.Infrastructure.Shared.Services.ImageKit.DTOs;
using WTE.TintTrack.Infrastructure.Shared.Services.SmartyStreets;
using WTE.TintTrack.Integration;
using static WTE.TintTrack.Common.Constants.Consts;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WTE.TintTrack.Api.Helpers.Extensions;

public static class DIExtension
{

    /// <summary>
    /// Registers the DbContexts for the application.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        // Check if the environment is for testing
        var isTesting = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Testing";

        if (isTesting)
        {
            // Use an in-memory database for testing
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("TintTrackCRMMasterConnection_InMemory");
                options.LogTo(Console.WriteLine, LogLevel.Debug);
            });

            return;
        }

        // Determine the database provider (SQL Server or MariaDB) from configuration
        var dbProvider = configuration["DatabaseProvider"] ?? "SqlServer";

        // Master database setup
        var masterConnectionString = configuration.GetConnectionString("TintTrackCRMMasterConnection") ??
                                     throw new InvalidOperationException("Connection string 'TintTrackCRMMasterConnection' is not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (dbProvider.Equals("MariaDB", StringComparison.OrdinalIgnoreCase))
            {
                /*options.UseMySql(masterConnectionString,
                    new MariaDbServerVersion(new Version(10, 5, 9)), // Replace with your MariaDB version
                    mySqlOptions =>
                    {
                        mySqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        mySqlOptions.EnableRetryOnFailure();
                        mySqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });*/
            }
            else
            {
                options.UseSqlServer(masterConnectionString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure();
                        sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });
            }

            options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.MultipleCollectionIncludeWarning))
                   .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        });

        // Register Tenant Context and Provider Service BEFORE TenantDbContext
        // These are needed for dynamic connection string resolution
        services.AddScoped<ITenantProviderService, TenantProviderService>();
        services.AddScoped<ITenantContext, Core.Application.Services.TenantContext>();

        // Tenant database setup - Dynamic connection string resolution per tenant
        // Note: The connection string is resolved dynamically using ITenantContext
        // which uses TenantConnStrTemplate from ApplicationSettings (e.g., "Database=WTE.TintTrackCRM.{TENANTCODE}-DEV")
        // 
        // IMPORTANT: For one-database-per-tenant architecture:
        // - At runtime: TenantContextMiddleware resolves tenant, then TenantDbContext uses tenant-specific connection string
        // - For migrations: Uses default connection string from appsettings.json (TintTrackCRMTenantConnection)
        services.AddDbContext<TenantDbContext>((serviceProvider, options) =>
        {
            // Try to resolve tenant connection string dynamically at runtime
            // This will work when TenantContextMiddleware has already resolved the tenant
            string? connectionString = null;
            
            try
            {
                var tenantContext = serviceProvider.GetRequiredService<ITenantContext>();
                
                // If tenant is already resolved (by TenantContextMiddleware), use its connection string
                // This ensures each tenant gets their own database
                if (tenantContext.IsResolved && !string.IsNullOrEmpty(tenantContext.TenantConnectionString))
                {
                    connectionString = tenantContext.TenantConnectionString;
                }
            }
            catch
            {
                // Tenant context not available (e.g., during migrations, design-time, or before middleware runs)
                // Will fall back to OnConfiguring method in TenantDbContext or default connection string
            }

            // Configure connection string if tenant is resolved
            // Otherwise, OnConfiguring will handle it (for migrations/design-time scenarios)
            if (!string.IsNullOrEmpty(connectionString))
            {
                // Configure database provider with tenant-specific connection string
                if (dbProvider.Equals("MariaDB", StringComparison.OrdinalIgnoreCase))
                {
                    /*options.UseMySql(connectionString,
                        new MariaDbServerVersion(new Version(10, 5, 9)),
                        mySqlOptions =>
                        {
                            mySqlOptions.MigrationsAssembly(typeof(TenantDbContext).Assembly.FullName);
                            mySqlOptions.EnableRetryOnFailure();
                            mySqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        });*/
                }
                else
                {
                    options.UseSqlServer(connectionString,
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(TenantDbContext).Assembly.FullName);
                            sqlOptions.EnableRetryOnFailure();
                            sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        });
                }
            }
            // Note: If connectionString is null/empty, OnConfiguring in TenantDbContext will handle it
            // using ITenantProviderService (for migrations and design-time scenarios)
            // However, ITenantProviderService needs to be injected into TenantDbContext constructor
            // which requires using a factory pattern or ensuring it's available in the service provider

            options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        });

        // Add developer-friendly exception filter for EF migrations errors
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<ITenantDatabaseCreator>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<TenantDatabaseCreator>>();
            return new TenantDatabaseCreator(logger);
        });
        
        // Register Tenant Migration Service for managing migrations across all tenant databases
        services.AddScoped<TenantMigrationService>();

        // Register Domain Event Dispatcher
        services.AddScoped<IDomainEventDispatcher, Common.Infrastructure.Events.DomainEventDispatcher>();

        // Register Unit of Work implementations
        services.AddScoped<IApplicationUnitOfWork>(sp =>
        {
            var context = sp.GetRequiredService<ApplicationDbContext>();
            var dispatcher = sp.GetService<IDomainEventDispatcher>();
            return new Core.Infrastructure.UnitOfWork(context, dispatcher);
        });
        services.AddScoped<ITenantUnitOfWork>(sp =>
        {
            var context = sp.GetRequiredService<TenantDbContext>();
            var dispatcher = sp.GetService<IDomainEventDispatcher>();
            return new Business.Infrastructure.TenantUnitOfWork(context, dispatcher);
        });
    }

    /// <summary>
    /// Configures Swagger/OpenAPI documentation with API versioning support
    /// </summary>
    /// <param name="services">The service collection</param>
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        // Register API Explorer for Swagger generation
        services.AddEndpointsApiExplorer();

        // Load API description from file or use default
        var apiDescriptionContent = LoadApiDescription();

        // Configure Swagger with versioned API support
        // Note: AddVersionedApiExplorer must be called before AddSwaggerGen
        services.AddSwaggerGen(options =>
        {
            ConfigureSwaggerOptions(options, apiDescriptionContent);
        });

        // Configure Swagger for OData endpoints
        ConfigureSwaggerOData(services, apiDescriptionContent);
    }

    /// <summary>
    /// Loads API description from Description.txt file or returns default description
    /// </summary>
    private static string LoadApiDescription()
    {
        var apiDescriptionPath = Path.Combine(Directory.GetCurrentDirectory(), "Description.txt");
        if (File.Exists(apiDescriptionPath))
        {
            try
            {
                return File.ReadAllText(apiDescriptionPath);
            }
            catch
            {
                // Fall through to default if file read fails
            }
        }

        return "TintTrack is a cloud-based, multi-tenant SaaS platform specifically developed for glass tint shops to manage all aspects of their business operations efficiently.";
    }

    /// <summary>
    /// Configures Swagger options with versioning, security, and documentation
    /// </summary>
    private static void ConfigureSwaggerOptions(SwaggerGenOptions options, string apiDescriptionContent)
    {
        // Register operation filters
        options.OperationFilter<AuthorizeCheckOperationFilter>();
        options.OperationFilter<GenericTypeDescriptionFilter>();
        
        // Include XML documentation comments FIRST (this sets tag descriptions from summary)
        IncludeXmlComments(options);
        
        // Register document filter to include controller remarks in tag descriptions
        // This runs AFTER IncludeXmlComments, so it can append remarks to existing descriptions
        options.DocumentFilter<ControllerRemarksTagFilter>();

        // Resolve conflicting actions by taking the first one
        options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

        // Configure API versioning integration
        // The versioned API explorer automatically groups controllers by version
        // We need to include actions that match the version group
        options.DocInclusionPredicate((version, desc) =>
        {
            // The versioned API explorer provides the version group name (e.g., "v1.0")
            // We need to check if the action belongs to this version group
            if (!desc.TryGetMethodInfo(out var methodInfo))
                return false;

            // Get the controller type
            var controllerType = methodInfo.DeclaringType;
            if (controllerType == null)
                return false;

            // Check for ApiVersion attribute on controller
            var controllerVersions = controllerType
                .GetCustomAttributes(true)
                .OfType<Microsoft.AspNetCore.Mvc.ApiVersionAttribute>()
                .SelectMany(attr => attr.Versions)
                .ToList();

            // If controller has no version attribute, include it in v1 (default version)
            if (!controllerVersions.Any())
            {
                return version == "v1" || version == "v1.0";
            }

            // Check for MapToApiVersion attribute on action
            var actionVersions = methodInfo
                .GetCustomAttributes(true)
                .OfType<Microsoft.AspNetCore.Mvc.MapToApiVersionAttribute>()
                .SelectMany(attr => attr.Versions)
                .ToList();

            // Match version format: "v1.0" matches ApiVersion(1, 0), "v1" also matches ApiVersion(1, 0)
            var versionToMatch = version.Replace("v", "").Split('.').Select(int.Parse).ToList();
            var majorVersion = versionToMatch.Count > 0 ? versionToMatch[0] : 1;
            var minorVersion = versionToMatch.Count > 1 ? versionToMatch[1] : 0;

            var versionMatch = controllerVersions.Any(v => v.MajorVersion == majorVersion && v.MinorVersion == minorVersion) ||
                              (actionVersions.Any() && actionVersions.Any(v => v.MajorVersion == majorVersion && v.MinorVersion == minorVersion));

            return versionMatch;
        });

        // Configure Swagger documents for each API version
        // The versioned API explorer will create groups like "v1.0" based on GroupNameFormat ('v'VV)
        // We create documents for both "v1" (default) and "v1.0" (versioned) to ensure compatibility
        options.SwaggerDoc("v1", CreateOpenApiInfo("v1", apiDescriptionContent));
        options.SwaggerDoc("v1.0", CreateOpenApiInfo("v1.0", apiDescriptionContent));

        // Configure schema filters for polymorphism
        options.SchemaFilter<PolymorphismSchemaFilter>();

        // Register derived property asset DTOs explicitly for proper schema generation
        RegisterPropertyAssetDtoTypes(options);

        // Enable polymorphism support
        options.UseAllOfToExtendReferenceSchemas();

        // Configure JWT Bearer authentication
        ConfigureJwtBearerSecurity(options);
    }

    /// <summary>
    /// Creates OpenApiInfo for a specific API version
    /// </summary>
    private static OpenApiInfo CreateOpenApiInfo(string version, string description)
    {
        return new OpenApiInfo
        {
            Title = "WTE TintTrack Core and Business API",
            Version = version,
            Description = description,
            Contact = new OpenApiContact
            {
                Name = "Window Tints Everything",
                Email = "info@wteverything.com",
                Url = new Uri("https://windowtintseverything.com")
            },
            License = new OpenApiLicense
            {
                Name = "Proprietary",
                Url = new Uri("https://windowtintseverything.com")
            }
        };
    }

    /// <summary>
    /// Registers property asset DTO types for Swagger schema generation
    /// </summary>
    private static void RegisterPropertyAssetDtoTypes(SwaggerGenOptions options)
    {
        options.MapType<ArchitecturalPropertyAssetDto>(() => new OpenApiSchema { Type = "object" });
        options.MapType<AutomotivePropertyAssetDto>(() => new OpenApiSchema { Type = "object" });
        options.MapType<ResidentialPropertyAssetDto>(() => new OpenApiSchema { Type = "object" });
        options.MapType<CommercialPropertyAssetDto>(() => new OpenApiSchema { Type = "object" });
        options.MapType<SpecialtyPropertyAssetDto>(() => new OpenApiSchema { Type = "object" });
        options.MapType<GlassFilmPropertyAssetDto>(() => new OpenApiSchema { Type = "object" });
        options.MapType<EnergyEfficientPropertyAssetDto>(() => new OpenApiSchema { Type = "object" });
        options.MapType<CustomPropertyAssetDto>(() => new OpenApiSchema { Type = "object" });
        options.MapType<SignagePropertyAssetDto>(() => new OpenApiSchema { Type = "object" });
        options.MapType<OutdoorPropertyAssetDto>(() => new OpenApiSchema { Type = "object" });
        options.MapType<OtherPropertyAssetDto>(() => new OpenApiSchema { Type = "object" });
    }

    /// <summary>
    /// Configures JWT Bearer authentication for Swagger
    /// </summary>
    private static void ConfigureJwtBearerSecurity(SwaggerGenOptions options)
    {
        // Define the security scheme
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT"
        });

        // Apply security requirement globally
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    }

    /// <summary>
    /// Includes XML documentation comments from all relevant assemblies
    /// </summary>
    /// <remarks>
    /// This method configures Swagger to include XML documentation comments including summary, remarks, param, returns, and other XML documentation tags.
    /// The includeControllerXmlComments parameter ensures that controller-level XML comments (including remarks) are included in the Swagger documentation.
    /// Remarks are automatically included in the description field of the Swagger UI.
    /// </remarks>
    private static void IncludeXmlComments(SwaggerGenOptions options)
    {
        var baseDirectory = AppContext.BaseDirectory;
        var assemblies = new[]
        {
            "WTE.TintTrack.Api",
            "WTE.TintTrack.Core.Application",
            "WTE.TintTrack.Business.Application"
        };

        foreach (var assemblyName in assemblies)
        {
            var xmlFile = $"{assemblyName}.xml";
            var xmlPath = Path.Combine(baseDirectory, xmlFile);
            
            if (File.Exists(xmlPath))
            {
                // includeControllerXmlComments: true ensures controller XML comments (including remarks) are included
                // Remarks are automatically included in the Swagger description field
                options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            }
        }
    }

    /// <summary>
    /// Configures Swagger for OData endpoints
    /// </summary>
    private static void ConfigureSwaggerOData(IServiceCollection services, string apiDescriptionContent)
    {
        var openApiInfo = CreateOpenApiInfo("v1", apiDescriptionContent);

        services.AddSwaggerGenOData(opt =>
        {
            opt.SwaggerDoc("v1", "odata", openApiInfo);
        });
    }

    public static void SetupDuendeIdentity(this IServiceCollection services, IConfiguration configuration, ILogger<Startup>? logger = null)
    {
        // Bind the IdentityServer section to the settings class
        var identityServerSettings = new IdentityServerSettings();
        configuration.Bind("IdentityServer", identityServerSettings);

        services.Configure<IdentityServerSettings>(configuration.GetSection("IdentityServer"));

        // Bind Jwt settings
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        // Configure ASP.NET Identity to use ApplicationUser and ApplicationRole
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            // Configure password policy options here like password strength, lockout settings, etc.
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;

            // Configure other Identity options (like lockout, sign-in, etc.) as needed
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()   // Store user and role information in ApplicationDbContext
            .AddDefaultTokenProviders();                        // Adds default token providers for password reset, etc.

        // Configure IdentityServer:
        // This configuration defines IdentityServer's role in handling user authentication and issuing tokens for the API.
        services.AddIdentityServer(options =>
        {
            options.EmitStaticAudienceClaim = true; // Optional

            options.Events.RaiseSuccessEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseErrorEvents = true;

        })
            .AddAspNetIdentity<ApplicationUser>()
            .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
            .AddInMemoryClients(IdentityConfig.GetClients(identityServerSettings))
            .AddInMemoryApiResources(IdentityConfig.GetApiResources())
            .AddInMemoryApiScopes(IdentityConfig.GetApiScopes())
            .AddDeveloperSigningCredential();// Use in development only; switch to a persistent key in production

        // JWT Bearer authentication - use ILoggerFactory to resolve logger at runtime
        services.AddAuthentication(options =>
            {
                // ensures that for API requests, JWT Bearer is used, avoiding cookie-based redirects.
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = configuration["IdentityServer:Authority"]; // IdentityServer URL
                options.Audience = "api1"; // The API resource the token should be issued for
                options.RequireHttpsMetadata = false; // Set this to true in production
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };

                // We want to avoid redirection and return a 401 Unauthorized if the token is invalid or missing.
                // Logger will be resolved from HttpContext.RequestServices at runtime when events fire
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetService<ILogger<Startup>>();
                        logger?.LogError(context.Exception, "Authentication failed.");
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            context.Response.Headers.Append("Token-Expired", "true");

                        return Task.CompletedTask;
                    },
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        var logger = context.HttpContext.RequestServices.GetService<ILogger<Startup>>();
                        logger?.LogWarning($"Unauthorized access attempt to {context.Request.Path}. Details -> {context.Error} : {context.ErrorDescription}");

                        var apiMessageResponse = new DefaultApiResponse<string>
                        {
                            Data = context.Request.Path,
                            StatusCode = StatusCodes.Status401Unauthorized,
                            Message = "Unauthorized access. Please provide a valid token.",
                            Success = false
                        };

                        var jsonResponse = JsonConvert.SerializeObject(apiMessageResponse);

                        // Clear and reset the existing response body before writing the custom response
                        context.Response.Clear();
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                        await context.Response.WriteAsync(jsonResponse);
                    }
                };

            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthPoliciesEnum.GlobalAdminPolicy, policy =>
                policy.RequireClaim(ClaimTypes.Role,
                UserRolesEnum.GlobalAdmin.ToString()));

            options.AddPolicy(AuthPoliciesEnum.GlobalAdminAccountPolicy, policy =>
                policy.RequireClaim(ClaimTypes.Role,
                UserRolesEnum.GlobalAdmin.ToString(),
                UserRolesEnum.GlobalAccountMgr.ToString()
                ));

            options.AddPolicy(AuthPoliciesEnum.GlobalTechnicalSupportPolicy, policy =>
                policy.RequireClaim(ClaimTypes.Role,
                UserRolesEnum.GlobalAdmin.ToString(),
                UserRolesEnum.GlobalTechSupport.ToString()
                ));

            options.AddPolicy(AuthPoliciesEnum.TenantOwnerPolicy, policy =>
                policy.RequireClaim(ClaimTypes.Role,
                    UserRolesEnum.GlobalAdmin.ToString(),
                    UserRolesEnum.TenantOwner.ToString()
                ));

            options.AddPolicy(AuthPoliciesEnum.TenantBillingManagementPolicy, policy =>
                policy.RequireClaim(ClaimTypes.Role,
                    UserRolesEnum.GlobalAdmin.ToString(),
                    UserRolesEnum.GlobalTechSupport.ToString(),
                    UserRolesEnum.TenantOwner.ToString(),
                    UserRolesEnum.TenantBillingManager.ToString()
                ));

            options.AddPolicy(AuthPoliciesEnum.TenantSystemAdminPolicy, policy =>
                policy.RequireClaim(ClaimTypes.Role,
                    UserRolesEnum.GlobalAdmin.ToString(),
                    UserRolesEnum.GlobalTechSupport.ToString(),
                    UserRolesEnum.TenantOwner.ToString(),
                    UserRolesEnum.TenantSystemAdmin.ToString()
                ));
        });


    }

    /// <summary>
    /// Registers all repositories in the application.
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterRepositories(this IServiceCollection services)
    {
        // Use modular registration methods
        services.AddCoreRepositories();
        services.AddBusinessRepositories();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // SmartyStreets
        services.Configure<SmartyStreetsCredentials>(configuration.GetSection("SmartyStreets"));
        services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<SmartyStreetsCredentials>>().Value);

        // Error, Warning Or Information messages provider service
        services.AddSingleton<IMessageProviderService, MessageProviderService>();

        // Caching
        services.AddMemoryCache();
        services.AddScoped<ICacheService, CacheService>();

        // Rate Limiting
        services.AddScoped<IRateLimiter, RateLimiter>();

        // ImageKitIO
        services.Configure<ImageKitCredentials>(configuration.GetSection("ImageKitIO"));
        services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<ImageKitCredentials>>().Value);

        // Auxiliary services
        services.AddSingleton<ITokenValidationService, TokenValidationService>();
        services.AddTransient<IEmailSenderService, EmailSenderService>();
        services.AddSingleton<IImageKitUploadService, ImageKitUploadService>();
        services.AddTransient<IAddressValidatorService, SmartyStreetsAddressValidatorService>();

        // Use modular registration methods for domain services
        services.AddCoreServices();
        services.AddBusinessServices();
    }

    /// <summary>
    /// Register FluentValidation
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterFluentValidations(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            //Stop ASP.NET Core from returning a 400 Bad Request automatically when invalid data is passed into the request.
            //This is so we'll be able to manually handle validation errors in our controller action using FluentValidation.
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddFluentValidationAutoValidation();

        // Validators for application layer data validators
        services.AddValidatorsFromAssemblyContaining<ApplicationRoleDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<ApplicationUserDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<SubscriptionPlanDiscountDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<SubscriptionPlanDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<SubscriptionPlanFeatureDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<TenantDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<TenantInvitationDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<TenantSubscriptionDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<TenantSubscriptionInvoiceDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<TenantSubscriptionPaymentDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<TokenDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UserBillingProfileDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UserTenantDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UserTenantRoleDtoValidator>();

        // Validators for front-end layer request validators
        services.AddValidatorsFromAssemblyContaining<RefreshTokenRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterTenantRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateTenantSubscriptionInvoiceRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UserRegisterRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<PasswordResetValidator>();
        services.AddValidatorsFromAssemblyContaining<SubscriptionPlanRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UserTenantRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UserTenantRoleRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<TenantSubscriptionInvoiceRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UserBillingProfileRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UserProfileImageRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UserProfileDetailedRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateUserProfileRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<TenantLogoImageRequestValidator>();

        services.AddValidatorsFromAssemblyContaining<CreateCustomerRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateCustomerRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateContactRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateContactRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateCustomerContactRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CreatePropertyAssetRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdatePropertyAssetRequestValidator>();

        services.AddValidatorsFromAssemblyContaining<CreateInquiryRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateInquiryRequestValidator>();
    }

    public static IEdmModel GetBusinessEdmModel()
    {
        var builder = new ODataConventionModelBuilder();

        builder.EntityType<CustomerDto>().HasKey(f => f.Id);
        builder
            .EntityType<CustomerDto>()
            .HasDeleteRestrictions()
            .IsDeletable(false)
            .HasDescription("Not supported");
        builder
            .EntityType<CustomerDto>()
            .HasUpdateRestrictions()
            .IsUpdatable(false)
            .HasDescription("Not supported");
        builder
            .EntityType<CustomerDto>()
            .HasInsertRestrictions()
            .IsInsertable(false)
            .HasDescription("Not supported");

        builder.EntitySet<CustomerDto>("Customer");

        return builder.GetEdmModel();
    }

    public static IMvcBuilder AddAppOData(this IMvcBuilder builder)
    {

        builder.AddOData(opt =>
        {
            // Enable OData features
            opt.Select()
               .Filter()
               .OrderBy()
               .SetMaxTop(100)
               .Expand()
               .Count()
               .SkipToken()
               .SetMaxTop(100);

            opt.EnableQueryFeatures()
                .AddRouteComponents("api", GetEdmModel());
        });

        return builder;
    }

    public static IServiceCollection AddCRUDExtenders(this IServiceCollection services)
    {
        services.AddTransient<ICRUDExtender<CustomerDto, CreateCustomerRequest, UpdateCustomerRequest>, CustomerCRUDExtender>();
        services.AddTransient<ICRUDExtender<ContactDto, CreateContactRequest, UpdateContactRequest>, ContactCRUDExtender>();
        services.AddTransient<ICRUDExtender<PropertyAssetDto, CreatePropertyAssetRequest, UpdatePropertyAssetRequest>, PropertyCRUDExtender>();
        services.AddTransient<ICRUDExtender<InquiryDto, CreateInquiryRequest, UpdateInquiryRequest>, InquiryCRUDExtender>();

        return services;
    }

    // Configure OData routing with EDM (Entity Data Model)
    private static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();

        // Register entity sets
        builder.EntitySet<AuditLogDto>("AuditLogs")
                .HasSelectSupport()
                .IsSupported(true)
                .IsSkipSupported(true)
                .IsSearchable(true)
                .IsExpandable(true)
                .IsFilterable(true)
                .IsCountable(true)
                .IsSortable(true)
                .IsTopSupported(true)
                .IsComputeSupported(true);

        builder.EntitySet<CustomerDto>("Customers")
                .HasSelectSupport()
                .IsSupported(true)
                .IsSkipSupported(true)
                .IsSearchable(true)
                .IsExpandable(true)
                .IsFilterable(true)
                .IsCountable(true)
                .IsSortable(true)
                .IsTopSupported(true)
                .IsComputeSupported(true);

        builder.EntitySet<ContactDto>("Contacts")
                .HasSelectSupport()
                .IsSupported(true)
                .IsSkipSupported(true)
                .IsSearchable(true)
                .IsExpandable(true)
                .IsFilterable(true)
                .IsCountable(true)
                .IsSortable(true)
                .IsTopSupported(true)
                .IsComputeSupported(true);

        builder.EntitySet<CustomerContactDto>("CustomerContacts")
                .HasSelectSupport()
                .IsSupported(true)
                .IsSkipSupported(true)
                .IsSearchable(true)
                .IsExpandable(true)
                .IsFilterable(true)
                .IsCountable(true)
                .IsSortable(true)
                .IsTopSupported(true)
                .IsComputeSupported(true);

        builder.EntitySet<PropertyAssetDto>("PropertyAssets")
                .HasSelectSupport()
                .IsSupported(true)
                .IsSkipSupported(true)
                .IsSearchable(true)
                .IsExpandable(true)
                .IsFilterable(true)
                .IsCountable(true)
                .IsSortable(true)
                .IsTopSupported(true)
                .IsComputeSupported(true);

/*        builder.EntitySet<ProposalDto>("Proposals")
                .HasSelectSupport()
                .IsSupported(true)
                .IsSkipSupported(true)
                .IsSearchable(true)
                .IsExpandable(true)
                .IsFilterable(true)
                .IsCountable(true)
                .IsSortable(true)
                .IsTopSupported(true)
                .IsComputeSupported(true);*/

        builder.EntitySet<InquiryDto>("CustomerInquiries")
                .HasSelectSupport()
                .IsSupported(true)
                .IsSkipSupported(true)
                .IsSearchable(true)
                .IsExpandable(true)
                .IsFilterable(true)
                .IsCountable(true)
                .IsSortable(true)
                .IsTopSupported(true)
                .IsComputeSupported(true);

        /*builder.EntitySet<CustomerOwnershipDto>("CustomerOwnerships")
                .HasSelectSupport()
                .IsSupported(true)
                .IsSkipSupported(true)
                .IsSearchable(true)
                .IsExpandable(true)
                .IsFilterable(true)
                .IsCountable(true)
                .IsSortable(true)
                .IsTopSupported(true)
                .IsComputeSupported(true);*/


        /*builder.EntitySet<InvoiceDto>("Invoices")
                .HasSelectSupport()
                .IsSupported(true)
                .IsSkipSupported(true)
                .IsSearchable(true)
                .IsExpandable(true)
                .IsFilterable(true)
                .IsCountable(true)
                .IsSortable(true)
                .IsTopSupported(true)
                .IsComputeSupported(true);

        builder.EntitySet<ProjectDto>("Projects")
                .HasSelectSupport()
                .IsSupported(true)
                .IsSkipSupported(true)
                .IsSearchable(true)
                .IsExpandable(true)
                .IsFilterable(true)
                .IsCountable(true)
                .IsSortable(true)
                .IsTopSupported(true)
                .IsComputeSupported(true);*/

        /*

        builder.EntitySet<QuoteDto>("Quotes")
                .HasSelectSupport()
                .IsSupported(true)
                .IsSkipSupported(true)
                .IsSearchable(true)
                .IsExpandable(true)
                .IsFilterable(true)
                .IsCountable(true)
                .IsSortable(true)
                .IsTopSupported(true)
                .IsComputeSupported(true);*/


        return builder.GetEdmModel();
    }
}
