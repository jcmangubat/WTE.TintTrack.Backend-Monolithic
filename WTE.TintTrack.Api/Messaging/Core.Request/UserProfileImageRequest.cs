namespace WTE.TintTrack.Api.Messaging.Core.Request;

public class UserProfileImageRequest
{
    public string? UserCode { get; set; }

    public IFormFile? UserImage { get; set; }
}
