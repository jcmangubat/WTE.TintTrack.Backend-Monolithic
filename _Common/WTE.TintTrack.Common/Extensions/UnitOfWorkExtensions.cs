using WTE.TintTrack.Common.Interfaces;
using WTE.TintTrack.Common.Models;

namespace WTE.TintTrack.Common.Extensions;

/// <summary>
/// Extension methods for Unit of Work to simplify transaction management
/// </summary>
public static class UnitOfWorkExtensions
{
    /// <summary>
    /// Executes an operation within a transaction, automatically committing on success or rolling back on exception
    /// </summary>
    public static async Task<T> ExecuteInTransactionAsync<T>(
        this IUnitOfWork unitOfWork,
        Func<Task<T>> operation,
        CancellationToken cancellationToken = default)
    {
        using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var result = await operation().ConfigureAwait(false);
            await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            await unitOfWork.CommitTransactionAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
            throw;
        }
    }

    /// <summary>
    /// Executes an operation within a transaction, automatically committing on success or rolling back on exception
    /// </summary>
    public static async Task ExecuteInTransactionAsync(
        this IUnitOfWork unitOfWork,
        Func<Task> operation,
        CancellationToken cancellationToken = default)
    {
        using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            await operation().ConfigureAwait(false);
            await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            await unitOfWork.CommitTransactionAsync(cancellationToken).ConfigureAwait(false);
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
            throw;
        }
    }

    /// <summary>
    /// Executes an operation within a transaction and returns a Result pattern
    /// </summary>
    public static async Task<Result<T>> ExecuteInTransactionWithResultAsync<T>(
        this IUnitOfWork unitOfWork,
        Func<Task<Result<T>>> operation,
        CancellationToken cancellationToken = default)
    {
        using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var result = await operation().ConfigureAwait(false);
            
            if (result.IsFailure)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
                return result;
            }

            await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            await unitOfWork.CommitTransactionAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
            return Result<T>.Failure<T>(ex.Message);
        }
    }
}

