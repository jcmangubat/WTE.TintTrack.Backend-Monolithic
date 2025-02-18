namespace WTE.TintTrack.Api.Messaging._Abstractions;

public interface IApiMessageRequest
{
    bool? IsActive { get; set; }
    bool? IsArchived { get; set; }
    string? ReasonArchived { get; set; }
}
