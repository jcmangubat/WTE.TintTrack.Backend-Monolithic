using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Api.Messaging.Core.Requests;

public class UpdateUserProfileRequest
{
    /*[Required]
    [Email]
    public string Email { get; set; }*/

    [Phone]
    public string? PhoneNumber { get; set; }

    [MaxLength(FieldLengths.ApplicationUser.FirstName)]
    public string? FirstName { get; set; }

    [MaxLength(FieldLengths.ApplicationUser.LastName)]
    public string? LastName { get; set; }

    [MaxLength(FieldLengths.ApplicationUser.CompanyRole)]
    public string? CompanyRole { get; set; }

    /// <summary>
    /// The primary street address (e.g., "123 Main St").
    /// </summary>
    [MaxLength(FieldLengths.GeneralAddress.StreetAddress)]
    public string? StreetAddress { get; set; }

    /// <summary>
    /// Optional secondary address details (e.g., "Apt 4B" or "Suite 200").
    /// </summary>
    [MaxLength(FieldLengths.GeneralAddress.AddressLine2)]
    public string? AddressLine2 { get; set; }

    /// <summary>
    /// The city where the address is located.
    /// </summary>
    [MaxLength(FieldLengths.GeneralAddress.City)]
    public string? City { get; set; }

    /// <summary>
    /// The state, province, or region for the address.
    /// </summary>
    [MaxLength(FieldLengths.GeneralAddress.StateOrRegionOrProvince)]
    public string? StateOrRegion { get; set; }

    /// <summary>
    /// The postal or ZIP code for the address.
    /// </summary>
    [MaxLength(FieldLengths.GeneralAddress.PostalOrZIPCode)]
    public string? PostalCode { get; set; }

    /// <summary>
    /// The country of the address.
    /// Store as a two-letter ISO country code (ISO 3166-1 alpha-2) 
    /// </summary>
    [MaxLength(FieldLengths.GeneralAddress.CountryISOCode)]
    public string? CountryISOCode { get; set; }

    [MaxLength(FieldLengths.ApplicationUser.TimeZone)]
    public string? TimeZone { get; set; }

    [MaxLength(FieldLengths.ApplicationUser.ProfileImageUrl)]
    public string? ProfileImageUrl { get; set; }



    // Editable for admins to control lockout end

    public bool? LockoutEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; } // Editable by admin

    public bool? IsActive { get; set; }
}
