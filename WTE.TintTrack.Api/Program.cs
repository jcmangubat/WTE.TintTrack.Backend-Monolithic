using dotenv.net;
using Serilog;
using Serilog.Events;
using WTE.TintTrack.Api;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        DotEnv.Load();

        // Add appsettings.json and environment-specific config files
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();

        // Use user secrets if in Development environment
        if (context.HostingEnvironment.IsDevelopment())
        {
            //config.AddUserSecrets<Program>();
        }
        else
        {
            // Retrieve the Azure Key Vault URL from configuration
            var builtConfig = config.Build();
            string vaultUri = builtConfig["AzureKeyVault:SecretsVaultUri"];

            /*if (!string.IsNullOrEmpty(vaultUri))
                config.AddAzureKeyVault(new Uri(vaultUri), new DefaultAzureCredential());*/
        }
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .UseSerilog((context, configuration) =>
    {
        var configurationRoot = context.Configuration;
        var appInsightsInstrumentationKey = configurationRoot["ApplicationInsights:InstrumentationKey"];
        var azureBlobStorageConnectionString = configurationRoot["AzureBlobStorage:ConnectionString"];
        var azureBlobStorageContainerName = configurationRoot["AzureBlobStorage:ContainerName"];

        configuration.ReadFrom.Configuration(context.Configuration)
            .WriteTo.Console() // Console sink
            .WriteTo.File("logs/WTE.TintTrack.Core.Api-Logs.txt", rollingInterval: RollingInterval.Day) // File sink

            /*.WriteTo.ApplicationInsights(appInsightsInstrumentationKey, TelemetryConverter.Traces) // Application Insights sink

            .WriteTo.AzureBlobStorage(
                    sharedAccessSignature: azureBlobStorageConnectionString,
                    accountUrl: azureBlobStorageContainerName,
                    storageFileName: "logs/WTE.TintTrack.Api-Logs-{Date}.txt", // Blob storage sink
                    restrictedToMinimumLevel: LogEventLevel.Error,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")*/

            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .MinimumLevel.Override("System.Net.Mail", LogEventLevel.Error);
    });


var host = builder.Build();

// Run migrations and seed data
// Database initialization is handled by DatabaseInitializer.InitializeAsync()
// Uncomment the line below to enable automatic database initialization on startup
// await DatabaseInitializer.InitializeAsync(host);


host.Run();

// Make Program accessible to test projects
public partial class Program { }
