namespace WTE.TintTrack.Application.Shared.Messaging.Interface;

public interface IApiResponse
{
    bool Success { get; set; }
    string? Message { get; set; }
    int StatusCode { get; set; }
    string ResponseWrapperType { get; }
}
