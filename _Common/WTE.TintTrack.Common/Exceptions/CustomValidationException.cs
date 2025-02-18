namespace WTE.TintTrack.Common.Exceptions;

[Serializable]
public class CustomValidationException : ApplicationException
{
    public List<string> Errors { get; } = [];

    public CustomValidationException(List<string> errors)
    {
        Errors = errors;
    }

    public CustomValidationException()
    {
    }

    public CustomValidationException(string? message) : base(message)
    {
    }

    public CustomValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}