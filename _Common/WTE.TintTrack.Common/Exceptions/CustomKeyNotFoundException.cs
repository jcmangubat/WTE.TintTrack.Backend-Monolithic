namespace WTE.TintTrack.Common.Exceptions;

[Serializable]
public class CustomKeyNotFoundException : KeyNotFoundException
{
    public string? ErrorCode { get; set; }

    public CustomKeyNotFoundException(string message, string? errorCode = null)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    public CustomKeyNotFoundException(string message, Exception? innerException, string? errorCode = null)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}