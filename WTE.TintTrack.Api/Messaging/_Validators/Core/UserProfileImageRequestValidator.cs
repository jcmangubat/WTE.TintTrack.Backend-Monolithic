using FluentValidation;
using WTE.TintTrack.Api.Messaging.Core.Request;

namespace WTE.TintTrack.Api.Messaging._Validators.Core;

public class UserProfileImageRequestValidator : AbstractValidator<UserProfileImageRequest>
{
    private readonly List<string> allowedImageContentTypes =
    [
        "image/jpeg",
        "image/png",
        "image/gif",
        "image/bmp",
        "image/tiff"
    ];

    public UserProfileImageRequestValidator()
    {
        // Validate that UserImage is provided, has content, and is of type image
        /*RuleFor(x => x.UserImage)
            .NotNull().WithMessage("User image file is required.")
            .Must(file => file.Length > 0).WithMessage("User image file cannot be empty.")
            .Must(file => allowedImageContentTypes.Contains(file.ContentType))
                .WithMessage("User image must be a valid image file (jpeg, png, gif, bmp, tiff).");*/

        RuleFor(x => x.UserImage)
            .Must(file => file == null || file.Length > 0 && allowedImageContentTypes.Contains(file.ContentType))
            .WithMessage("User image must be null or a valid image file (jpeg, png, gif, bmp, tiff).");
    }
}
