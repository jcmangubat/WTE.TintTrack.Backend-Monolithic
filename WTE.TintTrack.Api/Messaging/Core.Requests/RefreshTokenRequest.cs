using WTE.TintTrack.Application.Shared.Validator.Attributes;

namespace WTE.TintTrack.Api.Messaging.Core.Requests;

public class RefreshTokenRequest
{
    [Required]
    public required string RefreshToken { get; set; }
}
