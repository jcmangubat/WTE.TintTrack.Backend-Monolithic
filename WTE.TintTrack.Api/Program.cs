using dotenv.net;
using Serilog;
using Serilog.Events;
using WTE.TintTrack.Api;
using WTE.TintTrack.Api.Helpers;

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
        var azureBlobContainerName = configurationRoot["AzureBlobStorage:ContainerName"];

        configuration.ReadFrom.Configuration(context.Configuration)
            .WriteTo.Console() // Console sink
            .WriteTo.File("logs/WTE.TintTrack.Core.Api-Logs.txt", rollingInterval: RollingInterval.Day) // File sink

            /*.WriteTo.ApplicationInsights(appInsightsInstrumentationKey, TelemetryConverter.Traces) // Application Insights sink

            .WriteTo.AzureBlobStorage(
                    sharedAccessSignature: azureBlobStorageConnectionString,
                    accountUrl: azureBlobContainerName,
                    storageFileName: "logs/WTE.TintTrack.Api-Logs-{Date}.txt", // Blob storage sink
                    restrictedToMinimumLevel: LogEventLevel.Error,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")*/

            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .MinimumLevel.Override("System.Net.Mail", LogEventLevel.Error);
    });


var host = builder.Build();

// Run migrations and seed data
await DatabaseInitializer.InitializeAsync(host);

/*// Run migrations and seed data
using (var scope = host.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        // Migrate the database
        IEnumerable<string> pendingMigrations = [];
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var appSettings = serviceProvider.GetRequiredService<IOptions<ApplicationSettings>>().Value;

        pendingMigrations = context.Database.GetPendingMigrations();

        if (pendingMigrations.Any())
            await context.Database.MigrateAsync();

        // Seed the data
        await ApplicationDbContextSeed.SeedAsync(serviceProvider);

        var connStrTemplate = appSettings.TenantConnStrTemplate;
        var tenantService = serviceProvider.GetRequiredService<ITenantService>();

        var dbContext = scope.ServiceProvider.GetRequiredService<TenantDbContext>();

        var tenantCodes = new List<string>() { "DEFAULTCLIENT"
                                                , "LA2ND", "WTE001", "WTE002"
                                                };

        *//*var tenants = await tenantService.GetAllAsync();
        if (tenants?.Count() > 0)
            tenantCodes.AddRange(tenants.Select(t => t.TenantCode));*//*

        if (tenantCodes != null && tenantCodes.Any())
            tenantCodes.AddRange(tenantCodes);

        foreach (var tenantCode in tenantCodes)
        {
            var tenantConnStr = connStrTemplate.Replace("{TENANTCODE}", tenantCode);
            var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();

            optionsBuilder.UseSqlServer(tenantConnStr);

            using var tenantContext = new TenantDbContext(optionsBuilder.Options);

            if (!await tenantContext.Database.CanConnectAsync())
            {
                var created = await tenantContext.Database.EnsureCreatedAsync();

                var dropAllFKsSQL = await TenantDbContext.GetSqlAsync("WTE.TintTrack.Business.Infrastructure.SqlFiles.DropAllFKs.sql");
                await tenantContext.Database.ExecuteSqlRawAsync(dropAllFKsSQL);

                var dropAllPKsSQL = await TenantDbContext.GetSqlAsync("WTE.TintTrack.Business.Infrastructure.SqlFiles.DropAllPKs.sql");
                await tenantContext.Database.ExecuteSqlRawAsync(dropAllPKsSQL);

                var dropAllDBsSQL = await TenantDbContext.GetSqlAsync("WTE.TintTrack.Business.Infrastructure.SqlFiles.DropAllDBs.sql");
                await tenantContext.Database.ExecuteSqlRawAsync(dropAllDBsSQL);
            }

            pendingMigrations = tenantContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
                await tenantContext.Database.MigrateAsync();
        }
    }
    catch (Exception ex)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Startup>>();
        logger.LogError(ex, "An error occurred during migration or seeding.");
    }
}*/


host.Run();