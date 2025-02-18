namespace WTE.TintTrack.Common.Application.Validation;

public class ValidationErrorResponse(string message, IDictionary<string, List<string>> errors)
{
    public required string Message { get; set; } = message;
    public IDictionary<string, List<string>> Errors { get; set; } = errors;
}