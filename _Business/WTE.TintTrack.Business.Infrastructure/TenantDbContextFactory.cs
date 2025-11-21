using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WTE.TintTrack.Business.Infrastructure;

public class TenantDbContextFactory : IDesignTimeDbContextFactory<TenantDbContext>
{
    public TenantDbContext CreateDbContext(string[] args)
    {
        // Setup the path to the appsettings.json in the Web API project
        var currentDirectory = Directory.GetCurrentDirectory();
        var solutionDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;

        // Point to the location of appsettings.json in the Web API project
        var configFilePath = Path.Combine(solutionDirectory, "TintTrack", "Backend", "WTE.TintTrack.Api", "appsettings.json");
        Console.WriteLine($"Path to appsettings.json: {configFilePath}");
        Console.WriteLine($"appsettings.json exists? {File.Exists(configFilePath)}");

        if (!File.Exists(configFilePath))
            throw new FileNotFoundException("The appsettings.json file was not found at the expected location.", configFilePath);

        // Load configuration using the explicit path
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(configFilePath)!) // Use the directory of appsettings.json
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Retrieve the connection string from appsettings.json
        var tenantConnectionString = configuration.GetConnectionString("TintTrackCRMTenantConnection");
        Console.WriteLine($"Connection string: {tenantConnectionString}");

        if (string.IsNullOrEmpty(tenantConnectionString))
            throw new InvalidOperationException("Connection string 'TintTrackCRMTenantConnection' is not found.");
        
        // Configure DbContext options with the connection string
        var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
        optionsBuilder.UseSqlServer(tenantConnectionString);

        return new TenantDbContext(optionsBuilder.Options);
    }
}