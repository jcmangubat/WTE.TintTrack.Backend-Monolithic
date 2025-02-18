using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SMEAppHouse.Core.Patterns.EF.SettingsModel;
using System.Reflection;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.PropertySpecifications;
using WTE.TintTrack.Business.Infrastructure.Configurations;

namespace WTE.TintTrack.Business.Infrastructure;

public class TenantDbContext : DbContext
{
    private readonly DbMigrationInformation? _dbMigrationInformation;
    private readonly ITenantProviderService? _tenantProviderService;

    public TenantDbContext(DbContextOptions<TenantDbContext> options,
                                DbMigrationInformation? dbMigrationInformation = null,
                                ITenantProviderService? tenantProviderService = null) : base(options)
    {
        _dbMigrationInformation = dbMigrationInformation;
        _tenantProviderService = tenantProviderService;
    }

    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Inquiry> Inquiries { get; set; }
    public DbSet<CustomerOwnership> CustomerOwnerships { get; set; }
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Property> CustomerProperties { get; set; }

    public DbSet<AutomotiveProperty> AutomotivePropertySpecifications { get; set; }
    public DbSet<ArchitecturalProperty> ArchitecturalPropertySpecifications { get; set; }
    public DbSet<CommercialProperty> CommercialPropertySpecifications { get; set; }
    public DbSet<CustomProperty> CustomPropertySpecifications { get; set; }
    public DbSet<EnergyEfficientProperty> EnergyEfficientPropertySpecifications { get; set; }
    public DbSet<GlassFilmProperty> GlassFilmPropertySpecifications { get; set; }
    public DbSet<OtherProperty> OtherPropertySpecifications { get; set; }
    public DbSet<OutdoorProperty> OutdoorPropertySpecifications { get; set; }
    public DbSet<ResidentialProperty> ResidentialPropertySpecifications { get; set; }
    public DbSet<SignageProperty> SignagePropertySpecifications { get; set; }
    public DbSet<SpecialtyProperty> SpecialtyPropertySpecifications { get; set; }


    public DbSet<Contact> Contacts { get; set; }
    public DbSet<CustomerContact> CustomerContacts { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Proposal> Proposals { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Invoice> Invoices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_tenantProviderService != null)
        {
            var tenantCode = _tenantProviderService.GetTenantCodeAsync().GetAwaiter().GetResult();
            var connectionString = _tenantProviderService.GetTenantSQLDbConnectionAsync().GetAwaiter().GetResult();

            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException($"Connection string for tenant {tenantCode} not found.");

            optionsBuilder.UseSqlServer(connectionString);
        }

        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var dbSchema = "dbo";
        modelBuilder.HasDefaultSchema(dbSchema);

        if (_dbMigrationInformation != null && !string.IsNullOrEmpty(_dbMigrationInformation.DbSchema))
        {
            dbSchema = _dbMigrationInformation.DbSchema;
            modelBuilder.HasDefaultSchema(dbSchema);
        }

        modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
        modelBuilder.ApplyConfiguration(new InquiryConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerOwnershipConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyConfiguration());
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerContactConfiguration());
        /*modelBuilder.ApplyConfiguration(new QuoteConfiguration());
        modelBuilder.ApplyConfiguration(new ProposalConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceConfiguration());*/
    }

    public static async Task<string> GetSqlAsync(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceStream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new FileNotFoundException($"Resource '{resourceName}' not found.");

        using var reader = new StreamReader(resourceStream);
        return await reader.ReadToEndAsync();
    }
}
