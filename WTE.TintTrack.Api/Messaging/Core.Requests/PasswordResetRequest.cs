using WTE.TintTrack.Application.Shared.Validator.Attributes;

namespace WTE.TintTrack.Api.Messaging.Core.Requests;

public class PasswordResetRequest
{
    [Email]
    [Required]
    public required string Email { get; set; }

    [Required]
    public required string ResetToken { get; set; }

    [Required]
    public required string NewPassword { get; set; }
}