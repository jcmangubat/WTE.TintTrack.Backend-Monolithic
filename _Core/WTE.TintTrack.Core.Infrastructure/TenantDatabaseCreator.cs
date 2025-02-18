using Microsoft.EntityFrameworkCore;
using WTE.TintTrack.Business.Infrastructure;
using WTE.TintTrack.Common.Interfaces;

namespace WTE.TintTrack.Core.Infrastructure;

public class TenantDatabaseCreator : ITenantDatabaseCreator
{
    public async Task CreateDatabaseAsync(string connectionString)
    {
        // Example using Entity Framework
        var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        using var context = new TenantDbContext(optionsBuilder.Options);
        await context.Database.EnsureCreatedAsync();
    }
}