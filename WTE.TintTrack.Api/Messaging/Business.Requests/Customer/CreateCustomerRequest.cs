using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Messaging.Business.Requests.Customer;

public class CreateCustomerRequest : IEntityCreateRequest, ICodedModel
{
    [Required]
    [MaxLength(FieldLengths.Customer.Code)]
    public required string Code { get; set; }

    [Required]
    [MaxLength(FieldLengths.Customer.Name)]
    public required string Name { get; set; }
    public required CustomerStatusEnum CustomerStatus { get; set; }

    [MaxLength(FieldLengths.Customer.MainPhone)]
    public string? MainPhone { get; set; }

    [Email]
    [MaxLength(FieldLengths.Customer.GeneralEmail)]
    public string? GeneralEmail { get; set; }

    [MaxLength(FieldLengths.Customer.Website)]
    public string? Website { get; set; }

    /// <summary>
    /// The primary street address (e.g., "123 Main St").
    /// </summary>
    [MaxLength(FieldLengths.Address.Street)]
    public string? Street { get; set; }

    /// <summary>
    /// Optional secondary address details (e.g., "Apt 4B" or "Suite 200").
    /// </summary>
    [MaxLength(FieldLengths.Address.AdditionalInfo)]
    public string? AdditionalInfo { get; set; }

    /// <summary>
    /// The city where the address is located.
    /// </summary>
    [MaxLength(FieldLengths.Address.City)]
    public string? City { get; set; }

    /// <summary>
    /// The state, province, or region for the address.
    /// </summary>
    [MaxLength(FieldLengths.Address.StateOrRegion)]
    public string? StateOrRegion { get; set; }

    /// <summary>
    /// The postal or ZIP code for the address.
    /// </summary>
    [MaxLength(FieldLengths.Address.PostalCode)]
    public string? PostalCode { get; set; }

    public TaxExemptionReasonsEnum? TaxExemptionReason { get; set; } = TaxExemptionReasonsEnum.NotExempt;

    public string? IndustryType { get; set; } 

    /// <summary>
    /// The country of the address.
    /// Store as a two-letter ISO country code (ISO 3166-1 alpha-2) 
    /// To be sourced from https://restcountries.com/v3.1/all?fields=name,cca3
    /// </summary>
    [MaxLength(FieldLengths.Address.CountryISOCode)]
    public string? CountryISOCode { get; set; }

    [MaxLength(FieldLengths.Customer.Notes)]
    public string? Notes { get; set; }

    public IEnumerable<string>? Tags { get; set; }

    public bool? IsImported { get; set; }
}
