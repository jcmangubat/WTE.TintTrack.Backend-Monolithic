namespace WTE.TintTrack.Application.Shared.Validator.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class RequiredAttribute(string message) : Attribute
{
    public string Message { get; } = message;

    public RequiredAttribute()
        : this("This field is required.")
    {

    }
}
