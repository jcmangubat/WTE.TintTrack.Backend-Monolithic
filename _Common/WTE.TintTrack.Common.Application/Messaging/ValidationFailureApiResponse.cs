using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using WTE.TintTrack.Application.Shared.Helpers;

namespace WTE.TintTrack.Application.Shared.Messaging;

public class ValidationFailureApiResponse<T> : DefaultApiResponse<T>
{
    public ValidationFailureApiResponse(T data, ValidationResult? validationResult = null)
        : this(data, validationResult, StatusCodes.Status400BadRequest, null)
    {
    }

    public ValidationFailureApiResponse(T data, ValidationResult? validationResult = null, string? message = null)
        : this(data, validationResult, StatusCodes.Status400BadRequest, message)
    {
    }

    public ValidationFailureApiResponse(T data, ValidationResult? validationResult = null, int statusCode = StatusCodes.Status400BadRequest, string? message = null)
        : this(data,
              validationResult.Errors.ToDictionary(),
              //ParseValidationResult(validationResult),
              statusCode, message)
    {
    }

    public ValidationFailureApiResponse(T data)
        : this(data, default(Dictionary<string, string[]>?), StatusCodes.Status400BadRequest, null)
    {
    }

    public ValidationFailureApiResponse(T data, Dictionary<string, string[]>? errors = null)
        : this(data, errors, StatusCodes.Status400BadRequest, null)
    {
    }

    public ValidationFailureApiResponse(T data, Dictionary<string, string[]>? errors = null, int statusCode = StatusCodes.Status400BadRequest, string? message = null)
            : base(data, statusCode, message)
    {
        Success = false;
        Errors = errors == default(Dictionary<string, string[]>?) ? null : errors;
        StatusCode = statusCode;
        Message = message ?? "Validation failure";
    }

    public Dictionary<string, string[]>? Errors { get; set; } = [];
}

