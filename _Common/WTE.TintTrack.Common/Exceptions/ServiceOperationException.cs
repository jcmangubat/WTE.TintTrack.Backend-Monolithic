namespace WTE.TintTrack.Common.Exceptions;

[Serializable]
public class ServiceOperationException : ApplicationException
{
    // Default constructor
    public ServiceOperationException()
    {
    }

    // Constructor with only a message
    public ServiceOperationException(string? message)
        : base(message)
    {
    }

    // Constructor with only a message and inner exception
    public ServiceOperationException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    // Constructor with only a message and errors
    public ServiceOperationException(Dictionary<string, string[]> errors, string? message = null)
        : this(message, null, errors)
    {
    }

    // Constructor with a message, inner exception, and errors
    public ServiceOperationException(string? message, Exception? innerException, Dictionary<string, string[]>? errors)
        : base(message, innerException)
    {
        Errors = errors ?? new Dictionary<string, string[]>();
    }

    // Constructor with only an error code and message
    public ServiceOperationException(string errorCode, string? message)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    // Constructor with an error code, message, and inner exception
    public ServiceOperationException(string errorCode, string? message, Exception? innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }

    // Constructor with an error code, message, and errors
    public ServiceOperationException(string errorCode, Dictionary<string, string[]> errors, string? message = null)
        : this(errorCode, message, null, errors)
    {
    }

    // Constructor with an error code, message, inner exception, and errors
    public ServiceOperationException(string errorCode, string? message, Exception? innerException, Dictionary<string, string[]>? errors)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
        Errors = errors ?? new Dictionary<string, string[]>();
    }

    // Properties
    public string? ErrorCode { get; set; }
    public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}
