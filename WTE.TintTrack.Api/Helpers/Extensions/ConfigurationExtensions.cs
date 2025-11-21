using Microsoft.Extensions.Options;
using WTE.TintTrack.Common.Models;

namespace WTE.TintTrack.Api.Helpers.Extensions;

/// <summary>
/// Extension methods for standardized configuration management using Options pattern
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Registers all application configuration settings using Options pattern with validation
    /// </summary>
    public static IServiceCollection AddApplicationConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Application Settings
        services.Configure<ApplicationSettings>(configuration.GetSection("ApplicationSettings"));
        services.AddSingleton(resolver => 
            resolver.GetRequiredService<IOptions<ApplicationSettings>>().Value);

        // JWT Settings
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddSingleton(resolver => 
            resolver.GetRequiredService<IOptions<JwtSettings>>().Value);

        // SMTP Settings
        services.Configure<SMTPSettings>(configuration.GetSection("SMTPSettings"));
        services.AddSingleton(resolver => 
            resolver.GetRequiredService<IOptions<SMTPSettings>>().Value);

        // CORS Settings
        services.Configure<CorsSettings>(configuration.GetSection("Cors"));
        services.AddSingleton(resolver => 
            resolver.GetRequiredService<IOptions<CorsSettings>>().Value);

        // Validate critical settings at startup
        services.AddOptions<ApplicationSettings>()
            .Bind(configuration.GetSection("ApplicationSettings"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<JwtSettings>()
            .Bind(configuration.GetSection("JwtSettings"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    /// <summary>
    /// Gets a strongly-typed configuration value with validation
    /// </summary>
    public static T GetConfiguration<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var section = configuration.GetSection(sectionName);
        var options = new T();
        section.Bind(options);
        return options;
    }
}

