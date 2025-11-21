namespace WTE.TintTrack.Api.Messaging.Core.Requests;

public class UserProfileImageRequest
{
    public string? UserCode { get; set; }

    public IFormFile? UserImage { get; set; }
}
