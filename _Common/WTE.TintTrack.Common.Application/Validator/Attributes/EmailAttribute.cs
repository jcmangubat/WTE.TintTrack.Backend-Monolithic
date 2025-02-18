namespace WTE.TintTrack.Application.Shared.Validator.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class EmailAttribute(string message) : Attribute
{
    public string Message { get; } = message;

    public EmailAttribute() : this("A valid email is required.")
    {
    }
}
