namespace WTE.TintTrack.Application.Shared.Validator.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class CreditCardAttribute(string message) : Attribute
{
    public string Message { get; } = message;

    public CreditCardAttribute()
        : this("Invalid credit card number.")
    {

    }
}
