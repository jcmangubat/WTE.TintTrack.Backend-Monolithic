using Microsoft.IdentityModel.Tokens;

namespace WTE.TintTrack.Common.Exceptions;

[Serializable]
public class CustomSecurityTokenException : SecurityTokenException
{
    public string? ErrorCode { get; set; }

    public CustomSecurityTokenException(string message, string? errorCode = null)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    public CustomSecurityTokenException(string message, Exception? innerException, string? errorCode = null)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}