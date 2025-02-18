namespace WTE.TintTrack.Application.Shared.Validator.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class UrlAttribute(string message) : Attribute
{
    public string Message { get; } = message;

    public UrlAttribute() : this("A valid URL is required.")
    {
    }
}
