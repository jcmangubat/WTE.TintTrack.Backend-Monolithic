using Microsoft.AspNetCore.Identity;
using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Interfaces;

namespace WTE.TintTrack.Core.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>, IAuditableEntity
{
    public bool? IsActive { get; set; } = true;

    public required string UserCode { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? ProfileImageUrl { get; set; }
    public string? JobTitle { get; set; }

    /// <summary>
    /// The primary street address (e.g., "123 Main St").
    /// </summary>
    public string? StreetAddress { get; set; }

    /// <summary>
    /// Optional secondary address details (e.g., "Apt 4B" or "Suite 200").
    /// </summary>
    public string? AddressLine2 { get; set; }

    /// <summary>
    /// The city where the address is located.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// The state, province, or region for the address.
    /// </summary>
    public string? StateOrRegion { get; set; }

    /// <summary>
    /// The postal or ZIP code for the address.
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// The country of the address.
    /// Store as a two-letter ISO country code (ISO 3166-1 alpha-2) 
    /// To be sourced from https://restcountries.com/v3.1/all?fields=name,cca3
    /// </summary>
    public string? CountryISOCode { get; set; }

    public string? TimeZone { get; set; }

    #region Fields for auditing

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime? DateModified { get; set; } = DateTime.UtcNow;
    public bool? IsArchived { get; set; }
    public DateTime? DateArchived { get; set; }
    public string? ReasonArchived { get; set; }

    #endregion

    /// <summary>
    /// Many-to-Many Relationship with Tenant: 
    /// This User may have several tenants association
    /// </summary>
    public virtual ICollection<UserTenant> UserTenants { get; set; } = new HashSet<UserTenant>();

    // Many-to-Many Relationship with UserBillingProfile
    public virtual ICollection<UserBillingProfile> UserBillingProfiles { get; set; } = new HashSet<UserBillingProfile>();

    public virtual ICollection<Token> Tokens { get; set; } = new HashSet<Token>();

    public virtual ICollection<UserTenantInvitation> UserTenantInvitations { get; set; } = new HashSet<UserTenantInvitation>();
}