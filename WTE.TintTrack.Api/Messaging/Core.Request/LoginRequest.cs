using WTE.TintTrack.Application.Shared.Validator.Attributes;

namespace WTE.TintTrack.Api.Messaging.Core.Request;

public class LoginRequest
{
    [Required]
    [Email]
    public required string Email { get; set; }

    [Required]
    [Password]
    public required string Password { get; set; }
}
