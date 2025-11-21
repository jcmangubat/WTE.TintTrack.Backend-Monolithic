namespace WTE.TintTrack.Common.Models;

/// <summary>
/// CORS configuration settings
/// </summary>
public class CorsSettings
{
    public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
    public bool AllowCredentials { get; set; } = true;
    public string[] AllowedMethods { get; set; } = Array.Empty<string>();
    public string[] AllowedHeaders { get; set; } = Array.Empty<string>();
}

