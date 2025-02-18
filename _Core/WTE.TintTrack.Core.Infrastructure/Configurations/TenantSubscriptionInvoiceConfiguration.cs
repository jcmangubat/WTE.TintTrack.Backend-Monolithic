using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Infrastructure.Configurations;

public class TenantSubscriptionInvoiceConfiguration(string schema = "dbo")
    : EntityConfiguration<TenantSubscriptionInvoice, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<TenantSubscriptionInvoice> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(invoice => invoice.InvoiceNo, true, FieldLengths.TenantSubscriptionInvoice.InvoiceNo, null, "nvarchar");
        builder.DefineDbField(invoice => invoice.InvoiceCode, true, FieldLengths.TenantSubscriptionInvoice.InvoiceCode, null, "nvarchar");
        builder.DefineDbField(invoice => invoice.Currency, true, FieldLengths.TenantSubscriptionInvoice.Currency, null, "nvarchar");
        builder.DefineDbField(invoice => invoice.Notes, false, FieldLengths.TenantSubscriptionInvoice.Notes);

        builder.DefineDbField(invoice => invoice.DueDate, true);
        builder.DefineDbField(invoice => invoice.Amount, true, "decimal(18,2)");
        builder.DefineDbField(invoice => invoice.LateFeeAmount, false, "decimal(18,2)");
        builder.DefineDbField(invoice => invoice.InvoiceStatus, true);


        builder.HasMany(invoice => invoice.TenantSubscriptionPayments)
               .WithOne(payment => payment.TenantSubscriptionInvoice)
               .HasForeignKey(payment => payment.InvoiceId);

        builder.HasOne(invoice => invoice.TenantSubscription)
               .WithMany(subscription => subscription.TenantSubscriptionInvoices)
               .HasForeignKey(invoice => invoice.TenantSubscriptionId);
    }
}
