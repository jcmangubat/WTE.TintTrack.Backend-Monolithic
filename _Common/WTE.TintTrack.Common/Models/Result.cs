namespace WTE.TintTrack.Common.Models;

/// <summary>
/// Result pattern implementation for operations that can succeed or fail
/// </summary>
public class Result
{
    public bool IsSuccess { get; private set; }
    public bool IsFailure => !IsSuccess;
    public string? ErrorMessage { get; private set; }
    public string? ErrorCode { get; private set; }
    public Dictionary<string, string[]>? Errors { get; private set; }

    protected Result(bool isSuccess, string? errorMessage = null, string? errorCode = null, Dictionary<string, string[]>? errors = null)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
        Errors = errors;
    }

    public static Result Success() => new Result(true);
    public static Result Failure(string errorMessage, string? errorCode = null) => new Result(false, errorMessage, errorCode);
    public static Result Failure(string errorMessage, Dictionary<string, string[]> errors, string? errorCode = null) 
        => new Result(false, errorMessage, errorCode, errors);

    public static Result<T> Success<T>(T value) => new Result<T>(value, true);
    public static Result<T> Failure<T>(string errorMessage, string? errorCode = null) => new Result<T>(default, false, errorMessage, errorCode);
    public static Result<T> Failure<T>(string errorMessage, Dictionary<string, string[]> errors, string? errorCode = null) 
        => new Result<T>(default, false, errorMessage, errorCode, errors);
}

/// <summary>
/// Result pattern implementation with value
/// </summary>
public class Result<T> : Result
{
    public T? Value { get; private set; }

    internal Result(T? value, bool isSuccess, string? errorMessage = null, string? errorCode = null, Dictionary<string, string[]>? errors = null)
        : base(isSuccess, errorMessage, errorCode, errors)
    {
        Value = value;
    }

    public static implicit operator Result<T>(T value) => Success(value);

    /// <summary>
    /// Creates a failure result for not found scenarios
    /// </summary>
    public static Result<T> NotFound(string? message = null)
    {
        return new Result<T>(default, false, message ?? "Resource not found", "NOT_FOUND");
    }

    /// <summary>
    /// Creates a failure result for unauthorized access
    /// </summary>
    public static Result<T> Unauthorized(string? message = null)
    {
        return new Result<T>(default, false, message ?? "Unauthorized access", "UNAUTHORIZED");
    }

    /// <summary>
    /// Creates a failure result for forbidden access
    /// </summary>
    public static Result<T> Forbidden(string? message = null)
    {
        return new Result<T>(default, false, message ?? "Forbidden", "FORBIDDEN");
    }

    /// <summary>
    /// Creates a failure result for conflict scenarios
    /// </summary>
    public static Result<T> Conflict(string? message = null)
    {
        return new Result<T>(default, false, message ?? "Conflict occurred", "CONFLICT");
    }
}

