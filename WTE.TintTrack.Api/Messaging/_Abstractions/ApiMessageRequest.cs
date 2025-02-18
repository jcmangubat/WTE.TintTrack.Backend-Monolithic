namespace WTE.TintTrack.Api.Messaging._Abstractions;


public class ApiMessageRequest : IApiMessageRequest
{
    public bool? IsActive { get; set; } = true;

    public bool? IsArchived { get; set; }
    public string? ReasonArchived { get; set; }
}
