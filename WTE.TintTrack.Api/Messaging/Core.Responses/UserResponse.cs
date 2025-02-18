using WTE.TintTrack.Api.Messaging._Abstractions;

namespace WTE.TintTrack.Api.Messaging.Core.Responses;

public class UserResponse : ApiMessageResponse
{
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? UserCode { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? CompanyRole { get; set; }
    public string? StreetAddress { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? StateOrRegion { get; set; }
    public string? PostalCode { get; set; }
    public string? CountryISOCode { get; set; }
    public string? TimeZone { get; set; }

    // Editable for admins to control lockout end
    public bool? LockoutEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
}
