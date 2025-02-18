using System.Net;

namespace WTE.TintTrack.Common.Application.Messaging;

public class _GenericApiResponseWrapper
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public dynamic Data { get; set; }
    public Dictionary<string, string[]> Errors { get; set; }
}