using FluentValidation;
using WTE.TintTrack.Api.Messaging.Core.Request;

namespace WTE.TintTrack.Api.Messaging._Validators.Core;

public class TenantLogoImageRequestValidator : AbstractValidator<TenantLogoImageRequest>
{
    private readonly List<string> allowedImageContentTypes =
    [
        "image/jpeg",
        "image/png",
        "image/gif",
        "image/bmp",
        "image/tiff"
    ];

    public TenantLogoImageRequestValidator()
    {
        // Validate that UserImage is provided, has content, and is of type image
        RuleFor(x => x.LogoImage)
            .NotNull().WithMessage("Logo image file is required.")
            .Must(file => file.Length > 0).WithMessage("Logo image file cannot be empty.")
            .Must(file => allowedImageContentTypes.Contains(file.ContentType))
                .WithMessage("Logo image must be a valid image file (jpeg, png, gif, bmp, tiff).");
    }
}