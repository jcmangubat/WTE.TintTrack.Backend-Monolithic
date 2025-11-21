using WTE.TintTrack.Application.Shared.Validator.Attributes;

namespace WTE.TintTrack.Api.Messaging.Core.Requests;

public class ConfirmEmailRequest
{
    /// <summary>
    /// The email confirmation token.
    /// </summary>
    [Required]
    public string Token { get; set; }

    /// <summary>
    /// The user's email.
    /// </summary>
    [Required]
    [Email]
    public string Email { get; set; }
}