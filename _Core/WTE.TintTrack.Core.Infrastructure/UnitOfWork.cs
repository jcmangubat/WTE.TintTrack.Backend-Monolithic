using Microsoft.EntityFrameworkCore.Storage;
using WTE.TintTrack.Common.Events;
using WTE.TintTrack.Common.Extensions;

namespace WTE.TintTrack.Core.Infrastructure;

/// <summary>
/// Unit of Work implementation for ApplicationDbContext (Core/Platform database)
/// </summary>
public class UnitOfWork : IApplicationUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IDomainEventDispatcher? _eventDispatcher;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(
        ApplicationDbContext context,
        IDomainEventDispatcher? eventDispatcher = null)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _eventDispatcher = eventDispatcher;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch domain events before saving (if dispatcher is available)
        if (_eventDispatcher != null)
        {
            await _context.PublishDomainEventsAsync(_eventDispatcher, cancellationToken).ConfigureAwait(false);
        }
        
        return await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        return _transaction;
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction in progress.");
        }

        try
        {
            // Dispatch domain events before saving (if dispatcher is available)
            if (_eventDispatcher != null)
            {
                await _context.PublishDomainEventsAsync(_eventDispatcher, cancellationToken).ConfigureAwait(false);
            }
            
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            await _transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
            throw;
        }
        finally
        {
            await DisposeTransactionAsync().ConfigureAwait(false);
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            return;
        }

        try
        {
            await _transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            await DisposeTransactionAsync().ConfigureAwait(false);
        }
    }

    public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation, CancellationToken cancellationToken = default)
    {
        await BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var result = await operation().ConfigureAwait(false);
            await CommitTransactionAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
            throw;
        }
    }

    public async Task ExecuteInTransactionAsync(Func<Task> operation, CancellationToken cancellationToken = default)
    {
        await BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            await operation().ConfigureAwait(false);
            await CommitTransactionAsync(cancellationToken).ConfigureAwait(false);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
            throw;
        }
    }

    private async Task DisposeTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync().ConfigureAwait(false);
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
    }
}

