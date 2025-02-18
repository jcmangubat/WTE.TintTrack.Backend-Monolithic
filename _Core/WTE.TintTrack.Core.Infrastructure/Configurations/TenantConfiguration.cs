using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Infrastructure.Configurations;

public class TenantConfiguration(string schema = "dbo")
    : EntityConfiguration<Tenant, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Tenant> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(tenant => tenant.TenantCode, true, FieldLengths.Tenant.TenantCode, null, "nvarchar");
        builder.DefineDbField(tenant => tenant.Name, true, FieldLengths.Tenant.Name, null, "nvarchar");
        builder.DefineDbField(tenant => tenant.Description, true, FieldLengths.Tenant.Description, null, "nvarchar");
        builder.DefineDbField(tenant => tenant.ConnectionString, false, FieldLengths.Tenant.ConnectionString, null, "nvarchar");
        builder.DefineDbField(tenant => tenant.Domain, false, FieldLengths.Tenant.Domain, null, "nvarchar");
        builder.DefineDbField(tenant => tenant.LogoImageUrl, false, FieldLengths.General.URL, null, "nvarchar");
        builder.DefineDbField(tenant => tenant.CountryOfHost, false, FieldLengths.Tenant.CountryOfHost, null, "nvarchar");
        builder.DefineDbField(tenant => tenant.TenantStatus, true);

        builder.HasMany(tenant => tenant.UserTenants)
               .WithOne(userTenant => userTenant.Tenant)
               .HasForeignKey(userTenant => userTenant.TenantId);

        builder.HasMany(tenant => tenant.TenantSubscriptions)
               .WithOne(subscription => subscription.Tenant)
               .HasForeignKey(subscription => subscription.TenantId);

        /*builder.HasMany(tenant => tenant.Tokens)
               .WithOne(token => token.Tenant)
               .HasForeignKey(token => token.TenantId);*/
    }
}
