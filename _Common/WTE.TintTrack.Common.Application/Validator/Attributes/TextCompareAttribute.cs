namespace WTE.TintTrack.Application.Shared.Validator.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class TextCompareAttribute(string otherProperty, string message) : Attribute
{
    public string OtherProperty { get; } = otherProperty;
    public string Message { get; } = message;

    public TextCompareAttribute(string otherProperty) : this(otherProperty, "Values do not match.")
    {
    }
}
