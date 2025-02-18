using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SMEAppHouse.Core.Patterns.EF.SettingsModel;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Infrastructure.Configurations;

namespace WTE.TintTrack.Core.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    private readonly DbMigrationInformation? _dbMigrationInformation;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

        base.OnConfiguring(optionsBuilder);
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                DbMigrationInformation? dbMigrationInformation = null)
        : base(options)
    {
        _dbMigrationInformation = dbMigrationInformation;
    }

    public DbSet<Permission> Permissions { get; set; } = default!;
    public DbSet<RolePermission> RolePermissions { get; set; } = default!;

    public DbSet<Token> Tokens { get; set; }

    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    public DbSet<SubscriptionPlanFeature> SubscriptionPlanFeatures { get; set; }
    public DbSet<SubscriptionPlanFeatureAssociation> SubscriptionPlanFeatureAssociations { get; set; }

    public DbSet<SubscriptionPlanDiscount> SubscriptionPlanDiscounts { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<UserTenantInvitation> TenantInvitations { get; set; }
    public DbSet<TenantSubscription> TenantSubscriptions { get; set; }
    public DbSet<TenantSubscriptionInvoice> TenantSubscriptionInvoices { get; set; }
    public DbSet<TenantSubscriptionPayment> TenantSubscriptionPayments { get; set; }
    public DbSet<UserBillingProfile> UserBillingProfiles { get; set; }
    public DbSet<UserTenant> UserTenants { get; set; }
    public DbSet<UserTenantRole> UserTenantRoles { get; set; }

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

        modelBuilder.ApplyConfiguration(new ApplicationRoleConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());

        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());

        modelBuilder.ApplyConfiguration(new TokenConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionPlanConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionPlanFeatureConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionPlanFeatureAssociationConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionPlanDiscountConfiguration());
        modelBuilder.ApplyConfiguration(new TenantConfiguration());
        modelBuilder.ApplyConfiguration(new UserTenantInvitationConfiguration());
        modelBuilder.ApplyConfiguration(new TenantSubscriptionConfiguration());
        modelBuilder.ApplyConfiguration(new TenantSubscriptionInvoiceConfiguration());
        modelBuilder.ApplyConfiguration(new TenantSubscriptionPaymentConfiguration());
        modelBuilder.ApplyConfiguration(new UserBillingProfileConfiguration());
        modelBuilder.ApplyConfiguration(new UserTenantConfiguration());
        modelBuilder.ApplyConfiguration(new UserTenantRoleConfiguration());
    }
}
