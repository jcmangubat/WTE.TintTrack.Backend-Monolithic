namespace WTE.TintTrack.Application.Shared.Validator.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class PasswordAttribute(string message) : Attribute
{
    public string Message { get; } = message;

    public PasswordAttribute() : this("Invalid password.")
    {
    }
}