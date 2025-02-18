using WTE.TintTrack.Application.Shared.Validator.Attributes;

namespace WTE.TintTrack.Api.Messaging.Core.Request;

public class RefreshTokenRequest
{
    [Required]
    public required string RefreshToken { get; set; }
}
