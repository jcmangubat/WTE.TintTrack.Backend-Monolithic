namespace WTE.TintTrack.Domain.Shared.SmartyStreets;

public class ValidatedAddress(string deliveryLine, string lastLine, double latitude, double longitude)
{
    public string DeliveryLine { get; private set; } = deliveryLine;
    public string LastLine { get; private set; } = lastLine;
    public double Latitude { get; private set; } = latitude;
    public double Longitude { get; private set; } = longitude;
}