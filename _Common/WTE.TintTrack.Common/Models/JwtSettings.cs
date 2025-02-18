namespace WTE.TintTrack.Common.Models;

public class JwtSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public double RefreshTokenExpirationDays { get; set; }
    public string Audience { get; set; }
    public int TokenExpirationMinutes { get; set; }
}
