using System.Text.RegularExpressions;

namespace WTE.TintTrack.Common.Helpers;

public static class PhoneNumberValidator
{
    private const string PhoneNumberPattern = @"^\+?[1-9]\d{0,2}[\s\-]?\(?\d{1,4}\)?[\s\-]?\d{1,4}[\s\-]?\d{1,9}$";

    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return false; // Invalid if null or empty
        }

        var regex = new Regex(PhoneNumberPattern);
        return regex.IsMatch(phoneNumber);
    }
}