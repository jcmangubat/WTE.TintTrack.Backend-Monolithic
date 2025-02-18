using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Messaging.Business.Request;

public class UpdateCustomerRequest : ApiMessageRequest, IEntityUpdateRequest
{
    [MaxLength(FieldLengths.Customer.Name)]
    public string? Name { get; set; }

    public CustomerStatusEnum? CustomerStatus { get; set; }

    public IEnumerable<string>? Tags { get; set; }

    [MaxLength(FieldLengths.Customer.Company)]
    public string? Company { get; set; }

    [MaxLength(FieldLengths.Customer.Phone)]
    public string? Phone { get; set; }

    [MaxLength(FieldLengths.Customer.Phone2)]
    public string? Phone2 { get; set; }

    [Email]
    [MaxLength(FieldLengths.Customer.Email)]
    public string? Email { get; set; }

    public bool? IsImported { get; set; }

    /// <summary>
    /// The primary street address (e.g., "123 Main St").
    /// </summary>
    [MaxLength(FieldLengths.Customer.StreetAddress)]
    public string? StreetAddress { get; set; }

    /// <summary>
    /// Optional secondary address details (e.g., "Apt 4B" or "Suite 200").
    /// </summary>
    [MaxLength(FieldLengths.Customer.AddressLine2)]
    public string? AddressLine2 { get; set; }

    /// <summary>
    /// The city where the address is located.
    /// </summary>
    [MaxLength(FieldLengths.Customer.City)]
    public string? City { get; set; }

    /// <summary>
    /// The state, province, or region for the address.
    /// </summary>
    [MaxLength(FieldLengths.Customer.StateOrRegion)]
    public string? StateOrRegion { get; set; }

    /// <summary>
    /// The postal or ZIP code for the address.
    /// </summary>
    [MaxLength(FieldLengths.Customer.PostalCode)]
    public string? PostalCode { get; set; }

    public TaxExemptionReasonsEnum? TaxExemptionReason { get; set; }

    /// <summary>
    /// The country of the address.
    /// Store as a two-letter ISO country code (ISO 3166-1 alpha-2) 
    /// To be sourced from https://restcountries.com/v3.1/all?fields=name,cca3
    /// </summary>
    [MaxLength(FieldLengths.Customer.CountryISOCode)]
    public string? CountryISOCode { get; set; }

    [MaxLength(FieldLengths.Customer.CreatedBy)]
    public string? CreatedBy { get; set; }
}