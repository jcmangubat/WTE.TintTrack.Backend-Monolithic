using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations;

public class InquiryConfiguration(string schema = "dbo")
    : EntityConfiguration<Inquiry, Guid>(
        prefixEntityNameToId: false,
        prefixAltTblNameToEntity: false,
        schema: schema, pluralizeTblName: true
    )
{
    public override void OnModelCreating(EntityTypeBuilder<Inquiry> builder)
    {
        base.OnModelCreating(builder);

        builder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);
        builder.DefineDbField(p => p.LeadSource, true);
        builder.DefineDbField(p => p.ConsultationDate, true);
        builder.DefineDbField(p => p.Subject, true, FieldLengths.Inquiry.Subject);
        builder.DefineDbField(p => p.Details, true, FieldLengths.Inquiry.Details);
        builder.DefineDbField(p => p.PropertyType, true);

        builder.DefineDbField(p => p.Budget, false, "decimal(18,2)", p => p.HasColumnType("decimal(18, 2)").HasPrecision(18, 2));
        builder.DefineDbField(p => p.TintType, false);
        builder.DefineDbField(p => p.SpecialRequests, false, FieldLengths.Inquiry.SpecialRequests);
        builder.DefineDbField(p => p.FollowUpNeeded, false);

        builder.DefineDbField(p => p.SalesRepUserCode, false, FieldLengths.Inquiry.SalesRepUserCode);
        builder.DefineDbField(p => p.TintServiceCodes, false, FieldLengths.Inquiry.TintServiceCodes);

        builder.HasMany(p => p.Proposals)
            .WithOne(p => p.Inquiry)
            .HasForeignKey(p => p.InquiryId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasMany(p => p.Quotes)
            .WithOne(p => p.Inquiry)
            .HasForeignKey(p => p.InquiryId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasMany(p => p.Estimates)
            .WithOne(p => p.Inquiry)
            .HasForeignKey(p => p.InquiryId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}
