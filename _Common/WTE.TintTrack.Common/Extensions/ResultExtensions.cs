using WTE.TintTrack.Common.Models;

namespace WTE.TintTrack.Common.Extensions;

/// <summary>
/// Extension methods for Result pattern
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Maps a Result to another Result type
    /// </summary>
    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> mapper)
    {
        if (result.IsFailure)
        {
            if (result.Errors != null)
                return Result<TOut>.Failure<TOut>(result.ErrorMessage ?? "Unknown error", result.Errors, result.ErrorCode);
            return Result<TOut>.Failure<TOut>(result.ErrorMessage ?? "Unknown error", result.ErrorCode);
        }

        return Result<TOut>.Success(mapper(result.Value!));
    }

    /// <summary>
    /// Binds a Result to another Result (monadic bind)
    /// </summary>
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> binder)
    {
        if (result.IsFailure)
        {
            if (result.Errors != null)
                return Result<TOut>.Failure<TOut>(result.ErrorMessage ?? "Unknown error", result.Errors, result.ErrorCode);
            return Result<TOut>.Failure<TOut>(result.ErrorMessage ?? "Unknown error", result.ErrorCode);
        }

        return binder(result.Value!);
    }

    /// <summary>
    /// Executes an action if the result is successful
    /// </summary>
    public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsSuccess)
            action(result.Value!);

        return result;
    }

    /// <summary>
    /// Executes an action if the result is a failure
    /// </summary>
    public static Result<T> OnFailure<T>(this Result<T> result, Action<string> action)
    {
        if (result.IsFailure)
            action(result.ErrorMessage ?? "Unknown error");

        return result;
    }

    /// <summary>
    /// Gets the HTTP status code for a Result based on its error code
    /// </summary>
    public static int GetHttpStatusCode(this Result result)
    {
        return result.ErrorCode switch
        {
            "NOT_FOUND" => 404,
            "VALIDATION_ERROR" => 400,
            "UNAUTHORIZED" => 401,
            "FORBIDDEN" => 403,
            "CONFLICT" => 409,
            _ => 400
        };
    }

    /// <summary>
    /// Combines multiple Results into a single Result
    /// </summary>
    public static Result<IEnumerable<T>> Combine<T>(params Result<T>[] results)
    {
        var failures = results.Where(r => r.IsFailure).ToList();
        
        if (failures.Any())
        {
            var errorMessages = failures.Select(f => f.ErrorMessage ?? "Unknown error");
            var allErrors = failures
                .Where(f => f.Errors != null)
                .SelectMany(f => f.Errors!)
                .GroupBy(kvp => kvp.Key)
                .ToDictionary(g => g.Key, g => g.SelectMany(x => x.Value).ToArray());

            if (allErrors.Any())
                return Result<IEnumerable<T>>.Failure<IEnumerable<T>>(
                    string.Join("; ", errorMessages),
                    allErrors);
            
            return Result<IEnumerable<T>>.Failure<IEnumerable<T>>(string.Join("; ", errorMessages));
        }

        return Result<IEnumerable<T>>.Success(results.Select(r => r.Value!));
    }
}

