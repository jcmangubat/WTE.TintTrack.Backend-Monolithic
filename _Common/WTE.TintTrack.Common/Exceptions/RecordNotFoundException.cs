namespace WTE.TintTrack.Common.Exceptions;

[Serializable]
public class RecordNotFoundException : ApplicationException
{
    // Property to store the error code
    public string? ErrorCode { get; set; }

    // Default constructor
    public RecordNotFoundException()
    {
    }

    // Constructor with a message
    public RecordNotFoundException(string? message)
        : base(message)
    {
    }

    // Constructor with a message and inner exception
    public RecordNotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    // Constructor with a message and error code
    public RecordNotFoundException(string errorCode, string? message)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    // Constructor with a message, error code, and inner exception
    public RecordNotFoundException(string errorCode, string? message, Exception? innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}
