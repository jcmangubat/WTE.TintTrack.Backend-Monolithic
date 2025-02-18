using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs;

public class CustomerDto : GuidKeyedAuditableModel, ICodedEntity
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

    public TaxExemptionReasonsEnum? TaxExemptionReason { get; set; } = TaxExemptionReasonsEnum.NotExempt;

    public string? CreatedBy { get; set; }
    public bool? IsImported { get; set; }

    public IEnumerable<CustomerContactDto> CustomerContacts { get; set; } = [];

    public IEnumerable<string> ContactCodes { get; set; } = [];

    /*    // Navigation properties for related entities
        public ICollection<CustomerOwnershipDto> CustomerOwnerships { get; set; } = new List<CustomerOwnershipDto>();

        public ICollection<QuoteDto> Quotes { get; set; } = new List<QuoteDto>();
        public ICollection<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
        public ICollection<PropertyDto> CustomerProperties { get; set; } = new List<PropertyDto>();
        public ICollection<CustomerInquiry> CustomerInquiries { get; set; } = new List<CustomerInquiry>();*/
}
