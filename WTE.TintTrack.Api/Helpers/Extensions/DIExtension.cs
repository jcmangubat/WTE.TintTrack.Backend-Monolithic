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
using System.Reflection;
using System.Security.Claims;
using System.Text;
using WTE.TintTrack.Api.Helpers.Configurations;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Helpers.Filters.Swagger;
using WTE.TintTrack.Api.Messaging._CRUDExtenders;
using WTE.TintTrack.Api.Messaging._Validators.Business;
using WTE.TintTrack.Api.Messaging._Validators.Core;
using WTE.TintTrack.Api.Messaging.Business.Request;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.DTOs.PropertySpecifications;
using WTE.TintTrack.Business.Application.Interfaces;
using WTE.TintTrack.Business.Application.Services;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;
using WTE.TintTrack.Business.Infrastructure;
using WTE.TintTrack.Business.Infrastructure.Repositories;
using WTE.TintTrack.Common.Interfaces;
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Application.Services;
using WTE.TintTrack.Core.Application.Validators;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;
using WTE.TintTrack.Core.Domain.Interfaces.Services;
using WTE.TintTrack.Core.Infrastructure;
using WTE.TintTrack.Core.Infrastructure.Repositories;
using WTE.TintTrack.Infrastructure.Shared.Services;
using WTE.TintTrack.Infrastructure.Shared.Services.ImageKit;
using WTE.TintTrack.Infrastructure.Shared.Services.ImageKit.DTOs;
using WTE.TintTrack.Infrastructure.Shared.Services.SmartyStreets;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Helpers.Extensions;

public static class DIExtension
{
    /// <summary>
    /// Registers the DbContexts for the application.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void AddDbContexts_OLD(this IServiceCollection services, IConfiguration configuration)
    {
        // Check if the environment is for testing
        var isTesting = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Testing";

        if (isTesting)
        {
            // Use an in-memory database for testing
            services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("WTETintTrackCRMMasterConnection_InMemory");
                    options.LogTo(Console.WriteLine, LogLevel.Debug);
                });

            return;
        }
        else
        {
            // Main application database setup
            var connectionString = configuration.GetConnectionString("WTETintTrackCRMMasterConnection") ??
            throw new InvalidOperationException("Connection string 'WTETintTrackCRMMasterConnection' is not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString,
                    options =>
                    {
                        options.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        options.EnableRetryOnFailure();
                        options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    }
                )
                .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.MultipleCollectionIncludeWarning))
                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
            );
        }

        var tenantConnectionString = configuration.GetConnectionString("WTETintTrackCRMTenantConnection") ??
            throw new InvalidOperationException("Connection string 'WTETintTrackCRMTenantConnection' is not found.");
        services.AddDbContext<TenantDbContext>(options =>
                options.UseSqlServer(tenantConnectionString,
                    options =>
                    {
                        options.MigrationsAssembly(typeof(TenantDbContext).Assembly.FullName);
                        options.EnableRetryOnFailure();
                        options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    })
                    .ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning)));

        // line below adds a developer-friendly exception filter for detailed
        // diagnostics of database errors during development. This provides helpful
        // error information in the development environment for EF migrations errors
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<ITenantDatabaseCreator, TenantDatabaseCreator>();
        services.AddScoped<ITenantProviderService, TenantProviderService>();
    }

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
                options.UseInMemoryDatabase("WTETintTrackCRMMasterConnection_InMemory");
                options.LogTo(Console.WriteLine, LogLevel.Debug);
            });

            return;
        }

        // Determine the database provider (SQL Server or MariaDB) from configuration
        var dbProvider = configuration["DatabaseProvider"] ?? "SqlServer";

        // Master database setup
        var masterConnectionString = configuration.GetConnectionString("WTETintTrackCRMMasterConnection") ??
                                     throw new InvalidOperationException("Connection string 'WTETintTrackCRMMasterConnection' is not found.");

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

        // Tenant database setup
        var tenantConnectionString = configuration.GetConnectionString("WTETintTrackCRMTenantConnection") ??
                                     throw new InvalidOperationException("Connection string 'WTETintTrackCRMTenantConnection' is not found.");

        services.AddDbContext<TenantDbContext>(options =>
        {
            if (dbProvider.Equals("MariaDB", StringComparison.OrdinalIgnoreCase))
            {
                /*options.UseMySql(tenantConnectionString,
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
                options.UseSqlServer(tenantConnectionString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(TenantDbContext).Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure();
                        sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });
            }

            options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        });

        // Add developer-friendly exception filter for EF migrations errors
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<ITenantDatabaseCreator, TenantDatabaseCreator>();
        services.AddScoped<ITenantProviderService, TenantProviderService>();
    }

    public static void AddSwaggerConfigurationX(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        var apiDescriptionPath = Path.Combine(Directory.GetCurrentDirectory(), "Description.txt");
        var apiDescriptionContent = File.Exists(apiDescriptionPath) ?
                                    File.ReadAllText(apiDescriptionPath) :
                                    "TintTrack is a cloud-based, multi-tenant SaaS platform to be specifically developed for glass tint shops to manage all aspects of their business operations efficiently.";

        services.AddSwaggerGen(c =>
        {
            var apiDescriptionPath = Path.Combine(Directory.GetCurrentDirectory(), "Description.txt");
            var apiDescriptionContent = File.Exists(apiDescriptionPath) ?
                                        File.ReadAllText(apiDescriptionPath) :
                                        "TintTrack is a cloud-based, multi-tenant SaaS platform to be specifically developed for glass tint shops to manage all aspects of their business operations efficiently.";

            c.OperationFilter<AuthorizeCheckOperationFilter>();

            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "WTE TintTrack Core and Business API",
                Version = "v1",
                Description = apiDescriptionContent,
                Contact = new OpenApiContact
                {
                    Name = "Window Tints Everything",
                    Email = "info@wteverything.com",
                    Url = new Uri("https://windowtintseverything.com")
                }
            });

            // Register derived types explicitly
            c.SchemaFilter<PolymorphismSchemaFilter>();

            // Enable polymorphism
            c.UseAllOfToExtendReferenceSchemas();

            // Enable grouping by `ApiExplorerSettings`
            c.DocInclusionPredicate((documentName, apiDescription) =>
            {
                if (string.IsNullOrEmpty(apiDescription.GroupName))
                    return false;

                return apiDescription.GroupName == documentName;
            });

            // Define the security scheme (JWT Bearer)
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            // Add a requirement for authentication
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

            //Include XML comments generated during compile of this project.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        // Configure Swagger with OData
        services.AddSwaggerGenOData(opt =>
        {
            opt.SwaggerDoc("v1", "odata", new OpenApiInfo
            {
                Title = "WTE TintTrack Core and Business API",
                Version = "v1",
                Description = apiDescriptionContent,
                Contact = new OpenApiContact
                {
                    Name = "Window Tints Everything",
                    Email = "info@wteverything.com",
                    Url = new Uri("https://windowtintseverything.com")
                }
            });
        });
    }


    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        var apiDescriptionPath = Path.Combine(Directory.GetCurrentDirectory(), "Description.txt");
        var apiDescriptionContent = File.Exists(apiDescriptionPath) ?
                                    File.ReadAllText(apiDescriptionPath) :
                                    "TintTrack is a cloud-based, multi-tenant SaaS platform...";

        var openApiInfo = new OpenApiInfo
        {
            Title = "WTE TintTrack Core and Business API",
            Version = "v1",
            Description = apiDescriptionContent,
            Contact = new OpenApiContact
            {
                Name = "Window Tints Everything",
                Email = "info@wteverything.com",
                Url = new Uri("https://windowtintseverything.com")
            }
        };

        services.AddSwaggerGen(c =>
        {
            c.OperationFilter<AuthorizeCheckOperationFilter>();
            c.OperationFilter<GenericTypeDescriptionFilter>();

            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            c.SwaggerDoc("v1", openApiInfo);

            // Register derived types explicitly
            c.SchemaFilter<PolymorphismSchemaFilter>();

            // Add derived DTOs explicitly
            c.MapType<ArchitecturalPropertyDto>(() => new OpenApiSchema { Type = "object" });
            c.MapType<AutomotivePropertyDto>(() => new OpenApiSchema { Type = "object" });
            c.MapType<ResidentialPropertyDto>(() => new OpenApiSchema { Type = "object" });
            c.MapType<CommercialPropertyDto>(() => new OpenApiSchema { Type = "object" });
            c.MapType<SpecialtyPropertyDto>(() => new OpenApiSchema { Type = "object" });
            c.MapType<GlassFilmPropertyDto>(() => new OpenApiSchema { Type = "object" });
            c.MapType<EnergyEfficientPropertyDto>(() => new OpenApiSchema { Type = "object" });
            c.MapType<CustomPropertyDto>(() => new OpenApiSchema { Type = "object" });
            c.MapType<SignagePropertyDto>(() => new OpenApiSchema { Type = "object" });
            c.MapType<OutdoorPropertyDto>(() => new OpenApiSchema { Type = "object" });
            c.MapType<OtherPropertyDto>(() => new OpenApiSchema { Type = "object" });

            // Enable polymorphism
            c.UseAllOfToExtendReferenceSchemas();

            // Enable grouping by `ApiExplorerSettings`
            c.DocInclusionPredicate((documentName, apiDescription) =>
            {
                return string.IsNullOrEmpty(apiDescription.GroupName) || apiDescription.GroupName == documentName;
            });

            // Define the security scheme (JWT Bearer)
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            // Define the security scheme (JWT Bearer)
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

            //Include XML comments generated during compile of this project.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
                c.IncludeXmlComments(xmlPath);
        });

        services.AddSwaggerGenOData(opt =>
        {
            opt.SwaggerDoc("v1", "odata", openApiInfo);
        });
    }

    public static void SetupDuendeIdentity(this IServiceCollection services, IConfiguration configuration, ILogger<Startup> logger)
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

        // JWT Bearer authentication
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
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        logger.LogError(context.Exception, "Authentication failed.");
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            context.Response.Headers.Append("Token-Expired", "true");

                        return Task.CompletedTask;
                    },
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        logger.LogWarning($"Unauthorized access attempt to {context.Request.Path}. Details -> {context.Error} : {context.ErrorDescription}");

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
        // Register core-service-specific repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserBillingProfileRepository, UserBillingProfileRepository>();
        services.AddScoped<IUserTenantRepository, UserTenantRepository>();
        services.AddScoped<IUserTenantInvitationRepository, UserTenantInvitationRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();

        services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
        services.AddScoped<ISubscriptionPlanFeatureRepository, SubscriptionPlanFeatureRepository>();
        services.AddScoped<ISubscriptionPlanFeatureAssociationRepository, SubscriptionPlanFeatureAssociationRepository>();
        services.AddScoped<ISubscriptionPlanDiscountRepository, SubscriptionPlanDiscountRepository>();

        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IUserTenantInvitationRepository, UserTenantInvitationRepository>();
        services.AddScoped<ITenantSubscriptionRepository, TenantSubscriptionRepository>();
        services.AddScoped<ITenantSubscriptionInvoiceRepository, TenantSubscriptionInvoiceRepository>();
        services.AddScoped<ITenantSubscriptionPaymentRepository, TenantSubscriptionPaymentRepository>();

        // Register business-service-specific repositories
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();
        services.AddScoped<IInquiryRepository, InquiryRepository>();
        services.AddScoped<ICustomerOwnershipRepository, CustomerOwnershipRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<ICustomerContactRepository, CustomerContactRepository>();
        services.AddScoped<IQuoteRepository, QuoteRepository>();
        services.AddScoped<IProposalRepository, ProposalRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void RegisterSMTPAndApplicationSettings(this IServiceCollection services, IConfiguration configuration)
    {
        // SMTPSettings
        services.Configure<SMTPSettings>(configuration.GetSection("Smtp"));
        services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<SMTPSettings>>().Value);

        // Bind the ApplicationSettings section to the ApplicationSettings class
        services.Configure<ApplicationSettings>(configuration.GetSection("ApplicationSettings"));
        services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<ApplicationSettings>>().Value);
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

        // ImageKitIO
        services.Configure<ImageKitCredentials>(configuration.GetSection("ImageKitIO"));
        services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<ImageKitCredentials>>().Value);

        // Auxiliary services
        services.AddSingleton<ITokenValidationService, TokenValidationService>();
        services.AddTransient<IEmailSenderService, EmailSenderService>();
        services.AddSingleton<IImageKitUploadService, ImageKitUploadService>();
        services.AddTransient<IAddressValidatorService, SmartyStreetsAddressValidatorService>();

        // Core domain services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserTenantService, UserTenantService>();
        services.AddScoped<IUserTenantInvitationService, UserTenantInvitationService>();
        services.AddScoped<IUserBillingProfileService, UserBillingProfileService>();
        services.AddScoped<IRolePermissionService, RolePermissionService>();

        services.AddScoped<ISubscriptionPlanService, SubscriptionPlanService>();
        services.AddScoped<ISubscriptionPlanDiscountService, SubscriptionPlanDiscountService>();
        services.AddScoped<ISubscriptionPlanFeatureService, SubscriptionPlanFeatureService>();

        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<ITenantSubscriptionService, TenantSubscriptionService>();
        services.AddScoped<ITenantSubscriptionInvoiceService, TenantSubscriptionInvoiceService>();
        services.AddScoped<ITenantSubscriptionPaymentService, TenantSubscriptionPaymentService>();

        // Business domain services
        services.AddScoped<IAuditLogService, AuditLogService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<ICustomerContactService, CustomerContactService>();
        services.AddScoped<IInquiryService, InquiryService>();
        services.AddScoped<ICustomerOwnershipService, CustomerOwnershipService>();
        services.AddScoped<ICustomerService, CustomerService>();

        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IPropertyService, PropertyService>();
        services.AddScoped<IProposalService, ProposalService>();
        services.AddScoped<IQuoteService, QuoteService>();
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
        services.AddValidatorsFromAssemblyContaining<AddCustomerContactRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateInquiryRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateInquiryRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CreatePropertyRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdatePropertyRequestValidator>();
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
        services.AddTransient<ICRUDExtender<InquiryDto, CreateInquiryRequest, UpdateInquiryRequest>, InquiryCRUDExtender>();
        services.AddTransient<ICRUDExtender<PropertyDto, CreatePropertyRequest, UpdatePropertyRequest>, PropertyCRUDExtender>();

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

        builder.EntitySet<CustomerOwnershipDto>("CustomerOwnerships")
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

        builder.EntitySet<InvoiceDto>("Invoices")
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
                .IsComputeSupported(true);
        ;
        builder.EntitySet<PropertyDto>("Properties")
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

        builder.EntitySet<ProposalDto>("Proposals")
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
                .IsComputeSupported(true);


        return builder.GetEdmModel();
    }
}
