using FluentValidation.Results;
using WTE.TintTrack.Application.Shared.Helpers;

namespace WTE.TintTrack.Application.Shared.Helpers;

public static class ValidationHelper
{
    public static Dictionary<string, string[]> ToDictionary(this IEnumerable<ValidationFailure> failures)
    {
        return failures
            .GroupBy(f => f.PropertyName)
            .ToDictionary(
                g => g.Key, // Key is the property name
                g => g.Select(f => f.ErrorMessage).ToArray() // Value is an array of error messages
            );
    }

    public static Dictionary<string, string[]> ParseValidationResult(this ValidationResult? validationResult)
    {
        // Initialize a dictionary to hold the parsed validation errors
        var errors = new Dictionary<string, string[]>();

        if (validationResult == null)
            return errors;

        // Group the validation failures by property name
        var groupedErrors = validationResult.Errors
                                .Where(p => !string.IsNullOrEmpty(p.PropertyName))
                                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

        // Populate the dictionary
        foreach (var group in groupedErrors)
        {
            errors[group.Key] = group.ToArray();
        }

        return errors;
    }
}
