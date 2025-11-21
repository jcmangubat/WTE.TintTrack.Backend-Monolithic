using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Messaging.Business.Requests.Contact;

public class CreateContactRequest : IEntityCreateRequest, ICodedModel
{
    [Required]
    [MaxLength(FieldLengths.Contact.Code)]
    public required string Code { get; set; }

    [Required]
    [MaxLength(FieldLengths.Contact.FirstName)]
    public required string FirstName { get; set; }

    [MaxLength(FieldLengths.Contact.LastName)]
    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }
    public GendersEnum Gender { get; set; }
    public MaritalStatusEnum MaritalStatus { get; set; }

    [Phone]
    [MaxLength(FieldLengths.Contact.Phone)]
    public string? Phone { get; set; }

    [Phone]
    [MaxLength(FieldLengths.Contact.Mobile)]
    public string? Mobile { get; set; }

    [MaxLength(FieldLengths.Contact.AltPhone)]
    public string? AltPhone { get; set; }

    [Email]
    [MaxLength(FieldLengths.Contact.Email)]
    public string? Email { get; set; }

    public string? JobTitle { get; set; }

    //public required ContactTypesEnum ContactType { get; set; }

    public IEnumerable<string>? Tags { get; set; }
    public string? Notes { get; set; }

    public bool IsImported { get; set; }

    // Navigation property for associated entities
    /*public virtual IEnumerable<AddressDto> Addresses { get; set; } = [];
    public IEnumerable<string> CustomerCodes { get; set; } = [];
    public virtual IEnumerable<CustomerContactDto> CustomerContacts { get; set; } = [];*/

    /*

    /// <summary>
    /// The primary street address (e.g., "123 Main St").
    /// </summary>
    [MaxLength(FieldLengths.Contact.StreetAddress)] 
    public string? StreetAddress { get; set; }

    /// <summary>
    /// Optional secondary address details (e.g., "Apt 4B" or "Suite 200").
    /// </summary>
    [MaxLength(FieldLengths.Contact.AddressLine2)] 
    public string? AddressLine2 { get; set; }

    /// <summary>
    /// The city where the address is located.
    /// </summary>

    [MaxLength(FieldLengths.Contact.City)]
    public string? City { get; set; }

    /// <summary>
    /// The state, province, or region for the address.
    /// </summary>

    [MaxLength(FieldLengths.Contact.StateOrRegion)]
    public string? StateOrRegion { get; set; }

    /// <summary>
    /// The postal or ZIP code for the address.
    /// </summary>
    [MaxLength(FieldLengths.Contact.PostalCode)]
    public string? PostalCode { get; set; }

    /// <summary>
    /// The country of the address.
    /// Store as a two-letter ISO country code (ISO 3166-1 alpha-2) 
    /// To be sourced from https://restcountries.com/v3.1/all?fields=name,cca3
    /// </summary>

    [MaxLength(FieldLengths.Contact.CountryISOCode)]
    public string? CountryISOCode { get; set; }


    public bool? IsImported { get; set; }

    public TaxExemptionReasonsEnum? TaxExemptionReason { get; set; } = TaxExemptionReasonsEnum.NotExempt;

    public IEnumerable<string>? Tags { get; set; }

    

    [MaxLength(FieldLengths.Contact.Website)]
    public string? Website { get; set; }
    
    public required ContactTypesEnum ContactType { get; set; }
    
    [MaxLength(FieldLengths.Contact.JobTitle)] 
    public string? JobTitle { get; set; }
    
    [MaxLength(FieldLengths.Contact.Notes)] 
    public string? Notes { get; set; }*/
}
