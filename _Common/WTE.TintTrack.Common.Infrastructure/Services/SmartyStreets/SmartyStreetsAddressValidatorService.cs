using System.Text.Json;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Domain.Shared.SmartyStreets;
using WTE.TintTrack.Domain.Shared.SmartyStreets.ValueObjects;
using WTE.TintTrack.Infrastructure.Shared.Services.ImageKit.DTOs;
using WTE.TintTrack.Infrastructure.Shared.Services.SmartyStreets.DTOs;

namespace WTE.TintTrack.Infrastructure.Shared.Services.SmartyStreets;

public class SmartyStreetsAddressValidatorService(SmartyStreetsCredentials credentials, HttpClient httpClient)
    : IAddressValidatorService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly SmartyStreetsCredentials _credentials = credentials;

    public async Task<ValidatedAddress> ValidateAddressAsync(Address address)
    {
        var requestUrl = $"{_credentials.ApiUrl}/street-address" +
                         $"?auth-id={_credentials.AuthId}&auth-token={_credentials.AuthToken}" +
                         $"&street={Uri.EscapeDataString(address.Street)}" +
                         $"&city={Uri.EscapeDataString(address.City)}" +
                         $"&state={address.State}&zipcode={address.ZipCode}";

        var response = await _httpClient.GetAsync(requestUrl);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Address validation failed.");

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var addressData = JsonSerializer.Deserialize<List<SmartyStreetsResponse>>(jsonResponse);

        if (addressData == null || addressData.Count == 0)
            throw new Exception("Invalid address response from SmartyStreets.");

        var firstMatch = addressData.First();
        return new ValidatedAddress(
            firstMatch.DeliveryLine1,
            firstMatch.LastLine,
            firstMatch.Metadata.Latitude,
            firstMatch.Metadata.Longitude
        );
    }
}