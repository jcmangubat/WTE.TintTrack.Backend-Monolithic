using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using WTE.TintTrack.Api.Helpers.Extensions;
using WTE.TintTrack.Api.Messaging._Mappings;
using WTE.TintTrack.Api.Messaging.Business.Requests.PropertyAsset;
using WTE.TintTrack.Api.Middlewares;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.DTOs.PropertySpecificationModels;
using WTE.TintTrack.Business.Application.Mappings;
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Core.Application.Mappings;
using static WTE.TintTrack.Common.Constants.Consts;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace WTE.TintTrack.Api;

public class Startup
{
    private readonly ApplicationSettings _appSettings;
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
        _appSettings = configuration.GetSection("ApplicationSettings").Get<ApplicationSettings>(); ;
    }

    public IConfiguration Configuration => _configuration;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Configure logging
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
            //loggingBuilder.AddAzureWebAppDiagnostics();
        });

        // Add AutoMapper services
        services.AddAutoMapper(typeof(CoreModelsMappingProfile),
                                typeof(BusinessModelsMappingProfile),
                                typeof(CustomMappingProfileForUserBilling),
                                typeof(CoreDomainMappingProfile),
                                typeof(BusinessDomainMappingProfile));

        // Add ApplicationDbContext and configure the connection string
        services.AddDbContexts(Configuration);

        // Add setup for Duende Identity
        // Note: Logger will be resolved from DI when JWT events fire, avoiding service provider anti-pattern
        services.SetupDuendeIdentity(Configuration);

        // Register all application configuration using Options pattern
        services.AddApplicationConfiguration(Configuration);
        
        services.RegisterRepositories();
        services.RegisterServices(_configuration);

        // Add CORS policy using strongly-typed configuration
        var corsSettings = Configuration.GetSection("Cors").Get<CorsSettings>() ?? new CorsSettings();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                if (corsSettings.AllowedOrigins?.Length > 0)
                {
                    policy.WithOrigins(corsSettings.AllowedOrigins);
                }
                else
                {
                    policy.AllowAnyOrigin();
                }
                
                policy.AllowAnyHeader()
                      .AllowAnyMethod();
                
                if (corsSettings.AllowCredentials)
                {
                    policy.AllowCredentials();
                }
            });
        });

        // Add Response Compression for better performance
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.BrotliCompressionProvider>();
            options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.GzipCompressionProvider>();
        });

        // Configure API Versioning
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new HeaderApiVersionReader("X-Version"),
                new QueryStringApiVersionReader("version")
            );
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VV";  // Produces "v1.0" for ApiVersion(1, 0)
            options.SubstituteApiVersionInUrl = true;
        });

        // Add other services like MVC, controllers, etc.
        services.AddControllers(mvcOptions =>
            {
                mvcOptions.EnableEndpointRouting = false; // Disable routing for OData.
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers = { typeInfo =>
                    {
                        if (typeInfo.Type == typeof(PropertyAssetDto)||
                            typeInfo.Type == typeof(CreatePropertyAssetRequest))
                        {
                            typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                            {
                                TypeDiscriminatorPropertyName = nameof(PropertyAssetDto.PropertyType),
                                IgnoreUnrecognizedTypeDiscriminators = true,
                                DerivedTypes =
                                {
                                    new JsonDerivedType(typeof(ArchitecturalPropertyAssetDto), (int)PropertyTypesEnum.Architectural),
                                    new JsonDerivedType(typeof(AutomotivePropertyAssetDto), (int)PropertyTypesEnum.Automotive),
                                    new JsonDerivedType(typeof(ResidentialPropertyAssetDto), (int)PropertyTypesEnum.Residential),
                                    new JsonDerivedType(typeof(CommercialPropertyAssetDto), (int)PropertyTypesEnum.Commercial),
                                    new JsonDerivedType(typeof(SpecialtyPropertyAssetDto), (int)PropertyTypesEnum.Specialty),
                                    new JsonDerivedType(typeof(GlassFilmPropertyAssetDto), (int)PropertyTypesEnum.GlassFilm),
                                    new JsonDerivedType(typeof(EnergyEfficientPropertyAssetDto), (int)PropertyTypesEnum.EnergyEfficient),
                                    new JsonDerivedType(typeof(CustomPropertyAssetDto), (int)PropertyTypesEnum.Custom),
                                    new JsonDerivedType(typeof(SignagePropertyAssetDto), (int)PropertyTypesEnum.Signage),
                                    new JsonDerivedType(typeof(OutdoorPropertyAssetDto), (int)PropertyTypesEnum.Outdoor)
                                }
                            };
                        }
                    }}
                };
            })
            .AddAppOData();

        services.AddCRUDExtenders();

        // Register FluentValidation
        services.RegisterFluentValidations();

        // Add Health Checks
        services.AddHealthChecks()
            .AddDbContextCheck<Core.Infrastructure.ApplicationDbContext>("core_database")
            .AddDbContextCheck<Business.Infrastructure.TenantDbContext>("tenant_database");

        services.AddSwaggerConfiguration();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, IServiceProvider serviceProvider)
    {
        logger.LogInformation("Application is starting...");

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseMiddleware<CorrelationIdMiddleware>();
        app.UseMiddleware<RateLimitingMiddleware>();
        app.UseMiddleware<TenantContextMiddleware>();
        app.UseMiddleware<HttpMessagingMiddleware>();
        //app.UseMiddleware<TokenValidationMiddleware>();

        // Enable response compression (should be early in pipeline, before routing)
        app.UseResponseCompression();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // Use the CORS policy before mapping controllers
        app.UseCors("AllowFrontend");

        app.UseAuthentication(); // Enables authentication middleware
        app.UseAuthorization();  // Enables authorization middleware

        app.UseIdentityServer();


        //if (env.IsDevelopment() || Configuration.GetValue<bool>("EnableSwaggerInProd"))
        if (env.IsDevelopment() || (_appSettings.EnableSwaggerInProd ?? false))
        {
            app.UseSwagger();
            
            var apiVersionDescriptionProvider = app.ApplicationServices
                .GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwaggerUI(c =>
            {
                // Always add v1 endpoint first (Swagger UI default)
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WTE TintTrack API V1");
                
                // Generate Swagger endpoints for all API versions discovered by the versioned API explorer
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.OrderByDescending(x => x.ApiVersion))
                {
                    // Skip v1 if it's already added above
                    if (description.GroupName != "v1")
                    {
                        c.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            $"WTE TintTrack API {description.GroupName.ToUpperInvariant()}");
                    }
                }
                
                c.RoutePrefix = "swagger";  // Swagger UI available at /swagger

                c.InjectStylesheet("/swagger/swagger-custom.css"); // Inject custom CSS
                c.InjectJavascript("/swagger/swagger-custom.js"); // Optionally inject custom JS
                //c.InjectJavascript("/swagger-ui/swagger-clear-token.js");
            });
        }

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Health check endpoints
            endpoints.MapHealthChecks("/health");
            endpoints.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("ready")
            });
            endpoints.MapHealthChecks("/health/live", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                Predicate = _ => false
            });
        });

        app.UseODataRouteDebug();
    }
}
