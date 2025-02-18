using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Infrastructure.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(user => user.IsActive).IsRequired(false);
        builder.Property(user => user.UserCode).HasMaxLength(FieldLengths.ApplicationUser.UserCode).IsRequired(true);
        builder.Property(user => user.FirstName).HasMaxLength(FieldLengths.ApplicationUser.FirstName).IsRequired(false);
        builder.Property(user => user.LastName).HasMaxLength(FieldLengths.ApplicationUser.LastName).IsRequired(false);
        builder.Property(user => user.ProfileImageUrl).HasMaxLength(FieldLengths.ApplicationUser.ProfileImageUrl).IsRequired(false);
        builder.Property(user => user.JobTitle).HasMaxLength(FieldLengths.ApplicationUser.CompanyRole).IsRequired(false);

        builder.Property(user => user.StreetAddress).HasMaxLength(FieldLengths.GeneralAddress.StreetAddress).IsRequired(false);
        builder.Property(user => user.AddressLine2).HasMaxLength(FieldLengths.GeneralAddress.AddressLine2).IsRequired(false);
        builder.Property(user => user.City).HasMaxLength(FieldLengths.GeneralAddress.City).IsRequired(false);
        builder.Property(user => user.StateOrRegion).HasMaxLength(FieldLengths.GeneralAddress.StateOrRegionOrProvince).IsRequired(false);
        builder.Property(user => user.PostalCode).HasMaxLength(FieldLengths.GeneralAddress.PostalOrZIPCode).IsRequired(false);
        builder.Property(user => user.CountryISOCode).HasMaxLength(FieldLengths.GeneralAddress.CountryISOCode).IsRequired(false);

        builder.Property(user => user.TimeZone).HasMaxLength(FieldLengths.ApplicationUser.TimeZone).IsRequired(false);

        builder.Property(user => user.DateCreated).IsRequired(true).HasDefaultValue(DateTime.UtcNow);
        builder.Property(user => user.DateModified).IsRequired(true).HasDefaultValue(DateTime.UtcNow);
        builder.Property(user => user.IsArchived).IsRequired(false);
        builder.Property(user => user.DateArchived).IsRequired(false);
        builder.Property(user => user.ReasonArchived).IsRequired(false);
    }
}
