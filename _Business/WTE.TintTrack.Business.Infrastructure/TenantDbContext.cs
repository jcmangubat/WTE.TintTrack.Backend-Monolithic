using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SMEAppHouse.Core.Patterns.EF.SettingsModel;
using System.Reflection;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;
using WTE.TintTrack.Business.Domain.Entities.PropertySpecifications;
using WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;
using WTE.TintTrack.Business.Domain.Entities.TintServiceEntities;
using WTE.TintTrack.Business.Infrastructure.Configurations;
using WTE.TintTrack.Business.Infrastructure.Configurations.CommercialOffersConfig;
using WTE.TintTrack.Business.Infrastructure.Configurations.SalesAndQuotingConfig;
using WTE.TintTrack.Business.Infrastructure.Configurations.TintMaterialConfig;
using WTE.TintTrack.Business.Infrastructure.Configurations.TintServiceConfig;

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
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<CustomerContact> CustomerContacts { get; set; }
    public DbSet<PropertyAsset> PropertyAssets { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public DbSet<AutomotivePropertyAsset> AutomotivePropertyAssets { get; set; }
    public DbSet<ArchitecturalPropertyAsset> ArchitecturalPropertyAssets { get; set; }
    public DbSet<CommercialPropertyAsset> CommercialPropertyAssets { get; set; }
    public DbSet<CustomPropertyAsset> CustomPropertyAssets { get; set; }
    public DbSet<EnergyEfficientPropertyAsset> EnergyEfficientPropertyAssets { get; set; }
    public DbSet<GlassFilmPropertyAsset> GlassFilmPropertyAssets { get; set; }
    public DbSet<OtherPropertyAsset> OtherPropertyAssets { get; set; }
    public DbSet<OutdoorPropertyAsset> OutdoorPropertyAssets { get; set; }
    public DbSet<ResidentialPropertyAsset> ResidentialPropertyAssets { get; set; }
    public DbSet<SignagePropertyAsset> SignagePropertyAssets { get; set; }
    public DbSet<SpecialtyPropertyAsset> SpecialtyPropertyAssets { get; set; }

    public DbSet<TintMaterial> TintMaterials { get; set; }
    public DbSet<TintMaterialPriceHistory> TintMaterialPriceHistories { get; set; }
    public DbSet<TintMaterialPriceOverride> TintMaterialPriceOverrides { get; set; }
    public DbSet<TintMaterialPriceSchedule> TintMaterialPriceSchedules { get; set; }
    public DbSet<TintMaterialPriceTier> TintMaterialPriceTiers { get; set; }

    public DbSet<TintService> TintServices { get; set; }
    public DbSet<Inquiry> CustomerInquiries { get; set; }

    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Proposal> Proposals { get; set; }
    public DbSet<Estimate> Estimates { get; set; }

    public DbSet<OfferMilestone> OfferMilestones { get; set; }
    public DbSet<OfferRecipient> OfferRecipients { get; set; }
    public DbSet<OfferHistory> OfferHistories { get; set; }

    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectMilestone> ProjectMilestones { get; set; }

    //public DbSet<WorkOrder> WorkOrders { get; set; }
    //public DbSet<WorkOrderItem> WorkOrderItems { get; set; }

    //public DbSet<Invoice> Invoices { get; set; }
    //public DbSet<InvoiceItem> InvoiceItems { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Note: Connection string should already be configured via DI in DIExtension.cs
        // This method is only called if options weren't configured during service registration
        // For design-time migrations, TenantDbContextFactory handles connection string setup
        
        // Only configure if not already configured and we have a tenant provider service
        // However, we avoid async calls here to prevent deadlocks
        // If connection string is not set, EF Core will throw a clear error
        
        if (!optionsBuilder.IsConfigured && _tenantProviderService != null)
        {
            // WARNING: This is a fallback for edge cases only
            // At runtime, connection string should be set via DI configuration
            // For design-time, use TenantDbContextFactory which doesn't require async
            // 
            // If you reach here, it means:
            // 1. Connection string wasn't configured in DI (shouldn't happen at runtime)
            // 2. You're not using TenantDbContextFactory for migrations (should use factory)
            //
            // This fallback is kept for backward compatibility but should rarely execute
            throw new InvalidOperationException(
                "TenantDbContext connection string must be configured via DI. " +
                "For design-time migrations, use TenantDbContextFactory. " +
                "For runtime, ensure TenantDbContext is registered with connection string in DIExtension.cs");
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
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerContactConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyAssetConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new TintServiceConfiguration());
        modelBuilder.ApplyConfiguration(new InquiryConfiguration());

        modelBuilder.ApplyConfiguration(new TintMaterialConfiguration());
        modelBuilder.ApplyConfiguration(new TintMaterialPriceTierConfiguration());
        modelBuilder.ApplyConfiguration(new TintMaterialPriceHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new TintMaterialPriceOverrideConfiguration());
        modelBuilder.ApplyConfiguration(new TintMaterialPriceScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new TintMaterialPriceTierConfiguration());

        modelBuilder.ApplyConfiguration(new QuoteConfiguration());
        modelBuilder.ApplyConfiguration(new EstimateConfiguration());
        modelBuilder.ApplyConfiguration(new ProposalConfiguration());

        modelBuilder.ApplyConfiguration(new OfferRecipientConfiguration());
        modelBuilder.ApplyConfiguration(new OfferHistoryConfiguration());

        modelBuilder.ApplyConfiguration(new ContractConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectMilestoneConfiguration());
        //modelBuilder.ApplyConfiguration(new WorkOrderConfiguration());
        //modelBuilder.ApplyConfiguration(new WorkOrderItemConfiguration());
        //modelBuilder.ApplyConfiguration(new WorkOrderAssignmentConfiguration());
        //modelBuilder.ApplyConfiguration(new WorkOrderLogConfiguration());
        //modelBuilder.ApplyConfiguration(new WorkOrderLogPhotoConfiguration());

        //modelBuilder.ApplyConfiguration(new InvoiceConfiguration());

        /*
        modelBuilder.ApplyConfiguration(new CustomerOwnershipConfiguration());*/
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
