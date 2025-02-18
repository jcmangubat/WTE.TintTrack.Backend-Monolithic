using WTE.TintTrack.Application.Shared.Messaging.Interface;

namespace WTE.TintTrack.Application.Shared.Messaging;

public class DefaultApiResponse<T>(T data, int statusCode = 200, string? message = null) : IApiResponse
{
    public bool Success { get; set; } = true;
    public T? Data { get; set; } = data;
    public string? Message { get; set; } = message ?? "Success";
    public int StatusCode { get; set; } = statusCode;
    public virtual string ResponseWrapperType => GetType().Name.Split('`')[0];

    public DefaultApiResponse(T data) : this(data, null)
    {
    }

    public DefaultApiResponse(T data, string? message = null)
        : this(data, 200, message)
    {
    }

    public DefaultApiResponse() : this(default)
    {
    }
}
