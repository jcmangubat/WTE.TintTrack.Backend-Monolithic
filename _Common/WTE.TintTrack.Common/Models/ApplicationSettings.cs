namespace WTE.TintTrack.Common.Models;

public class ApplicationSettings
{
    //public int UserCodeLength { get; set; } = 6;
    public int AccessTokenExpiryAgeInMinutes { get; set; } = 60;
    public int RefreshTokenExpiryAgeInDays { get; set; } = 1;
    public bool? EnableSwaggerInProd { get; set; }

    public string TenantConnStrTemplate {  get; set; }  

    public required IEnumerable<EmailContact> ContactUsRecipients { get; set; } = [];

    public required EmailContact NoReplyEmailAddress { get; set; }

    public TimeSpan? MessageForwardingInterval { get; set; }

    public string? ImgKitUserAvatarsPath { get; set; }

    public string? ImgKitTenantLogosPath { get; set; }
    public string? ErrorMessagesPath { get; set; }
}

