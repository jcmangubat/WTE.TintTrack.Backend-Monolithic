using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using WTE.TintTrack.Api.Helpers.Extensions;
using WTE.TintTrack.Api.Messaging._Mappings;
using WTE.TintTrack.Api.Messaging.Business.Request;
using WTE.TintTrack.Api.Middlewares;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.DTOs.PropertySpecifications;
using WTE.TintTrack.Business.Application.Mappings;
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Core.Application.Mappings;
using static WTE.TintTrack.Common.Constants.Consts;

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

        // Add setup for Duende Identity, passing the logger
        services.AddTransient<Startup>(); // Register the class that contains ConfigureServices

        // Create a service provider to resolve the logger
        using (var serviceProvider = services.BuildServiceProvider())
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Startup>>();
            services.SetupDuendeIdentity(Configuration, logger);
        }

        services.RegisterSMTPAndApplicationSettings(Configuration);
        services.RegisterRepositories();
        services.RegisterServices(_configuration);

        // Add CORS policy to allow only the permissable origins setup in appSettings
        var allowedOrigins = Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
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
                        if (typeInfo.Type == typeof(PropertyDto)||
                            typeInfo.Type == typeof(CreatePropertyRequest))
                        {
                            typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                            {
                                TypeDiscriminatorPropertyName = nameof(PropertyDto.PropertyType),
                                IgnoreUnrecognizedTypeDiscriminators = true,
                                DerivedTypes =
                                {
                                    new JsonDerivedType(typeof(ArchitecturalPropertyDto), (int)PropertyTypesEnum.Architectural),
                                    new JsonDerivedType(typeof(AutomotivePropertyDto), (int)PropertyTypesEnum.Automotive),
                                    new JsonDerivedType(typeof(ResidentialPropertyDto), (int)PropertyTypesEnum.Residential),
                                    new JsonDerivedType(typeof(CommercialPropertyDto), (int)PropertyTypesEnum.Commercial),
                                    new JsonDerivedType(typeof(SpecialtyPropertyDto), (int)PropertyTypesEnum.Specialty),
                                    new JsonDerivedType(typeof(GlassFilmPropertyDto), (int)PropertyTypesEnum.GlassFilm),
                                    new JsonDerivedType(typeof(EnergyEfficientPropertyDto), (int)PropertyTypesEnum.EnergyEfficient),
                                    new JsonDerivedType(typeof(CustomPropertyDto), (int)PropertyTypesEnum.Custom),
                                    new JsonDerivedType(typeof(SignagePropertyDto), (int)PropertyTypesEnum.Signage),
                                    new JsonDerivedType(typeof(OutdoorPropertyDto), (int)PropertyTypesEnum.Outdoor)
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

        app.UseMiddleware<HttpMessagingMiddleware>();
        //app.UseMiddleware<TokenValidationMiddleware>();

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
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WTE TintTrack Business API Version 1.0");
                c.RoutePrefix = string.Empty;  // To serve Swagger UI at the app's root (https://wte-tinttrack-backend-dev.azurewebsites.net/)

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
        });

        app.UseODataRouteDebug();
    }
}
