using WTE.TintTrack.Application.Shared.ModelAbstraction;

namespace WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

public class ApplicationUserDto : IAuditableEntity
{
    // Editable fields

    public bool? IsActive { get; set; } = true;
    public string? Email { get; set; }

    public string? UserName { get; set; }

    public string? UserCode { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? ProfileImageUrl { get; set; }
    public string? JobTitle { get; set; }
    public string? TimeZone { get; set; }

    public string? PhoneNumber { get; set; }


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
    /// </summary>
    public string? CountryISOCode { get; set; }

    public bool TwoFactorEnabled { get; set; }

    // Editable for admins to control lockout end
    public bool LockoutEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; } // Editable by admin

    // Non-editable/System-managed properties
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? NormalizedUserName { get; set; }
    public string? NormalizedEmail { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? PasswordHash { get; set; }
    public string? SecurityStamp { get; set; }
    public string? ConcurrencyStamp { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public int AccessFailedCount { get; set; }


    // Fields for auditing

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime? DateModified { get; set; } = DateTime.UtcNow;
    public bool? IsArchived { get; set; }
    public DateTime? DateArchived { get; set; }
    public string? ReasonArchived { get; set; }
}
