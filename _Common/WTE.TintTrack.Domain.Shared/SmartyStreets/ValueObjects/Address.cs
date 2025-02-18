namespace WTE.TintTrack.Domain.Shared.SmartyStreets.ValueObjects;

public class Address(string street, string city, string state, string zipCode)
{
    public string Street { get; private set; } = street;
    public string City { get; private set; } = city;
    public string State { get; private set; } = state;
    public string ZipCode { get; private set; } = zipCode;
}
