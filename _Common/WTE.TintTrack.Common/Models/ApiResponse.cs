namespace WTE.TintTrack.Common.Models;

/// <summary>
/// Standardized API response wrapper
/// </summary>
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public string? CorrelationId { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
    public string? ErrorCode { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static ApiResponse<T> SuccessResponse(T data, string? message = null, string? correlationId = null)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Message = message,
            CorrelationId = correlationId
        };
    }

    public static ApiResponse<T> ErrorResponse(string message, string? errorCode = null, Dictionary<string, string[]>? errors = null, string? correlationId = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            ErrorCode = errorCode,
            Errors = errors,
            CorrelationId = correlationId
        };
    }
}

/// <summary>
/// Non-generic API response for operations without data
/// </summary>
public class ApiResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? CorrelationId { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
    public string? ErrorCode { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static ApiResponse SuccessResponse(string? message = null, string? correlationId = null)
    {
        return new ApiResponse
        {
            Success = true,
            Message = message,
            CorrelationId = correlationId
        };
    }

    public static ApiResponse ErrorResponse(string message, string? errorCode = null, Dictionary<string, string[]>? errors = null, string? correlationId = null)
    {
        return new ApiResponse
        {
            Success = false,
            Message = message,
            ErrorCode = errorCode,
            Errors = errors,
            CorrelationId = correlationId
        };
    }
}

