namespace WTE.TintTrack.Application.Shared.Validator.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class RegularExpressionAttribute(string pattern, string message) : Attribute
{
    public string Pattern { get; } = pattern;
    public string Message { get; } = message;

    public RegularExpressionAttribute(string pattern) : this(pattern, "Invalid format.")
    {
    }
}
