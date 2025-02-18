using WTE.TintTrack.Application.Shared.Validator.Attributes;

namespace WTE.TintTrack.Api.Messaging.Core.Request;

public class TenantLogoImageRequest
{
    public string? TenantCode { get; set; }

    [Required]
    public required IFormFile LogoImage { get; set; }
}