namespace WTE.TintTrack.Common.Exceptions;

[Serializable]
public class CustomInvalidOperationException : InvalidOperationException
{
    public string? ErrorCode { get; set; }

    public CustomInvalidOperationException(string message, string? errorCode = null)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    public CustomInvalidOperationException(string message, Exception? innerException, string? errorCode = null)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}
