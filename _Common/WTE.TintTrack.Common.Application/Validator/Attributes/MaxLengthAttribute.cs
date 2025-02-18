namespace WTE.TintTrack.Application.Shared.Validator.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class MaxLengthAttribute(int length, string message) : Attribute
{
    public int Length { get; } = length;
    public string Message { get; } = message;

    public MaxLengthAttribute(int length) : this(length, "Maximum length exceeded.")
    {
    }
}
