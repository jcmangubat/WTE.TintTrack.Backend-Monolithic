namespace WTE.TintTrack.Application.Shared.Validator.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class PhoneAttribute(string message) : Attribute
{
    public string Message { get; } = message;

    public PhoneAttribute() : this("Invalid phone number format.") { }
}