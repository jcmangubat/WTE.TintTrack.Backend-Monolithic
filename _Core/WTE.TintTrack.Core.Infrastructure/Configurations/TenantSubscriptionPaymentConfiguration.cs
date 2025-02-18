using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Infrastructure.Configurations;

public class TenantSubscriptionPaymentConfiguration(string schema = "dbo")
    : EntityConfiguration<TenantSubscriptionPayment, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<TenantSubscriptionPayment> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(payment => payment.Amount, true, "decimal(18,2)");
        builder.DefineDbField(payment => payment.PaymentDate, true);
        builder.DefineDbField(payment => payment.PaymentStatus, true);

        builder.HasOne(payment => payment.TenantSubscriptionInvoice)
               .WithMany(invoice => invoice.TenantSubscriptionPayments)
               .HasForeignKey(payment => payment.InvoiceId);
    }
}
