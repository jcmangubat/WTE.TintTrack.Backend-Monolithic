namespace WTE.TintTrack.Application.Shared.Validator.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class NumericRangeAttribute(int min, int max, string message) : Attribute
{
    public int Min { get; } = min;
    public int Max { get; } = max;
    public string Message { get; } = message;

    public NumericRangeAttribute(int min, int max) : this(min, max, "Value must be within the specified range.")
    {
    }
}
