namespace WTE.TintTrack.Application.Shared.Validator.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class MinLengthAttribute(int length, string message) : Attribute
{
    public int Length { get; } = length;
    public string Message { get; } = message;

    public MinLengthAttribute(int length) : this(length, "Minimum length required.")
    {
    }
}
