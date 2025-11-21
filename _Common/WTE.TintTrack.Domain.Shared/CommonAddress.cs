using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Domain.Shared;

public class CommonAddress: GuidKeyedAuditableEntity
{
    /// <summary>
    /// Street address, includes house/building number if applicable
    /// </summary>
    public required string Street { get; set; }

    /// <summary>
    /// Apartment, suite, unit, or other relevant details
    /// </summary>
    public string? AdditionalInfo { get; set; }

    /// <summary>
    /// City, town, or locality
    /// </summary>
    public required string City { get; set; }

    /// <summary>
    /// State, province, or region
    /// </summary>
    public required string StateOrRegion { get; set; }

    /// <summary>
    /// ZIP code, postcode, or PIN code
    /// </summary>
    public required string PostalCode { get; set; }

    /// <summary>
    /// Country name
    /// </summary>
    public required string Country { get; set; }

    /// <summary>
    /// The country of the address.
    /// ISO 3166-1 alpha-2 or alpha-3 country code
    /// Store as a two-letter ISO country code (ISO 3166-1 alpha-2) 
    /// To be sourced from https://restcountries.com/v3.1/all?fields=name,cca3
    /// </summary>
    public required string CountryISOCode { get; set; }

    /// <summary>
    /// Optional for geolocation support
    /// </summary>
    public string? Latitude { get; set; }

    /// <summary>
    /// Optional for geolocation support
    /// </summary>
    public string? Longitude { get; set; }

    public required AddressTypesEnum AddressType { get; set; }

}
