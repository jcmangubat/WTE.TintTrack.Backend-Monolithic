using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.SalesAndQuotingConfig;

public class InvoiceConfiguration(string schema = "dbo")
    : EntityConfiguration<Invoice, Guid>(
        prefixEntityNameToId: false,
        prefixAltTblNameToEntity: false,
        schema: schema, pluralizeTblName: true
    )
{
    public override void OnModelCreating(EntityTypeBuilder<Invoice> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);

        entityBuilder.DefineDbField(p => p.IssueDate, true);
        entityBuilder.DefineDbField(p => p.DueDate, false);
        entityBuilder.DefineDbField(p => p.Status, true);
        entityBuilder.DefineDbField(p => p.Subtotal, true, "decimal(18,2)"); // Total before taxes/discounts
        entityBuilder.DefineDbField(p => p.TaxAmount, true, "decimal(18,2)"); // Total tax amount
        entityBuilder.DefineDbField(p => p.DiscountAmount, true, "decimal(18,2)"); // Total discount amount
        entityBuilder.DefineDbField(p => p.Total, true, "decimal(18,2)"); // Final total amount after adjustments
        entityBuilder.DefineDbField(p => p.Notes, false, FieldLengths.General.SummaryParagraph); // e.g., payment instructions, legal disclaimers
        entityBuilder.DefineDbField(p => p.IsViewed, true); // Tracking if the invoice has been viewed
        entityBuilder.DefineDbField(p => p.IsPaid, true); // Tracking if the invoice has been paid
        entityBuilder.DefineDbField(p => p.PaidDate, false, "datetime2"); // Date when the invoice was paid
        entityBuilder.DefineDbField(p => p.SignatureType, true); // Signature type (e.g., digital, handwritten)
        entityBuilder.DefineDbField(p => p.IsSigned, true); // Tracking if the invoice has been signed  
        entityBuilder.DefineDbField(p => p.SignedDate, false, "datetime2"); // Date when the invoice was signed
        entityBuilder.DefineDbField(p => p.SignatureUrl, false, FieldLengths.General.URL);    // Signed doc or signature image
        entityBuilder.DefineDbField(p => p.SignatureContent, false);  // Optional, if embedded or from 3rd party
        entityBuilder.DefineDbField(p => p.SignedBy, false, FieldLengths.General.Name);  // Email, name, or external user ID
        entityBuilder.DefineDbField(p => p.SignatureProvider, false, FieldLengths.General.Name);  // e.g., "DocuSign", "AdobeSign"
        entityBuilder.DefineDbField(p => p.SignatureEnvelopeId, false, FieldLengths.General.LENGTH30);  // External ID for tracking
        entityBuilder.DefineDbField(p => p.PaymentMethod, false);
        entityBuilder.DefineDbField(p => p.InvoiceFileUrl , false, FieldLengths.General.URL); // URL to the invoice PDF or attachment
        

        //public Guid? ContractId 
        //public virtual Contract? Contract 



        //entityBuilder.DefineDbField(p => p.IssuedDate, true);
        //entityBuilder.DefineDbField(p => p.DueDate, false);
        //entityBuilder.DefineDbField(p => p.TotalAmount, true, "decimal(18,2)");

        entityBuilder.HasMany(p => p.InvoiceItems)
            .WithOne(p => p.Invoice)
            .HasForeignKey(p => p.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}