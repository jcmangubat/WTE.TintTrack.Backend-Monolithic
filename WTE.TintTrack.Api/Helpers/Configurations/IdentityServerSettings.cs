namespace WTE.TintTrack.Api.Helpers.Configurations;

public class IdentityServerSettings
{
    public string Authority { get; set; }
    public bool RequireHttpsMetadata { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public List<string> Scopes { get; set; }
    public int TokenExpirationMinutes { get; set; }
}