namespace WTE.TintTrack.Common.Exceptions;

public class CustomUnauthorizedAccessException : UnauthorizedAccessException
{
    public string? Code { get; set; }

    public CustomUnauthorizedAccessException(string message, string? code = null)
        : base(message)
    {
        Code = code;
    }

    public CustomUnauthorizedAccessException(string message, Exception? innerException, string? code = null)
        : base(message, innerException)
    {
        Code = code;
    }
}
