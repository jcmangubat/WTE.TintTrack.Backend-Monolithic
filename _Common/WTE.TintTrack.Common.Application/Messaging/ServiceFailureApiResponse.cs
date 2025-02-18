using Microsoft.AspNetCore.Http;

namespace WTE.TintTrack.Application.Shared.Messaging;

public class ServiceFailureApiResponse<T> : DefaultApiResponse<T>
{
    public ServiceFailureApiResponse(T data, string? message = null) :
        this(data, string.Empty, message, null, StatusCodes.Status400BadRequest)
    {
    }

    public ServiceFailureApiResponse(T data, string errorCode,
                                        string? message = null, IEnumerable<string>? errors = null,
                                        int statusCode = StatusCodes.Status400BadRequest)
        : base(data, statusCode, message)
    {
        Success = false;
        Errors = errors;
        Message = message ?? "Failed in service";
        ErrorCode = errorCode;
    }

    public IEnumerable<string>? Errors { get; set; } = [];

    public string ErrorCode { get; set; }
}

