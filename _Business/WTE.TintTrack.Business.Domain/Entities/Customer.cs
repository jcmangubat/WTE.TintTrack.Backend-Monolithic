using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities;

public class Customer : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }

    public required string Name { get; set; }

    public string? Company { get; set; }
    public string? Phone { get; set; }
    public string? Phone2 { get; set; }
    public string? Email { get; set; }

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

    public CustomerStatusEnum CustomerStatus { get; set; }

    public IEnumerable<string>? Tags { get; set; }
    public string? CreatedBy { get; set; }
    public bool? IsImported { get; set; }

    public TaxExemptionReasonsEnum? TaxExemptionReason { get; set; } = TaxExemptionReasonsEnum.NotExempt;

    // Navigation properties for related entities
    public virtual ICollection<CustomerOwnership> CustomerOwnerships { get; set; } = new HashSet<CustomerOwnership>();
    public virtual ICollection<CustomerContact> CustomerContacts { get; set; } = new HashSet<CustomerContact>();
    public virtual ICollection<Quote> Quotes { get; set; } = new HashSet<Quote>();
    public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    public virtual ICollection<Property> CustomerProperties { get; set; } = new HashSet<Property>();
    public virtual ICollection<Inquiry> Inquiries { get; set; } = new HashSet<Inquiry>();
}
