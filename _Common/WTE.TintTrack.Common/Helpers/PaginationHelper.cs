using Microsoft.EntityFrameworkCore;

namespace WTE.TintTrack.Common.Helpers;

/// <summary>
/// Helper class for pagination operations
/// </summary>
public static class PaginationHelper
{
    /// <summary>
    /// Applies pagination to an IQueryable and returns paginated results
    /// </summary>
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        if (pageNumber < 1)
            pageNumber = 1;
        
        if (pageSize < 1)
            pageSize = 10;
        
        if (pageSize > 100)
            pageSize = 100; // Maximum page size

        var totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken).ConfigureAwait(false);

        return new PagedResult<T>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            HasPreviousPage = pageNumber > 1,
            HasNextPage = pageNumber < totalPages
        };
    }

    /// <summary>
    /// Applies pagination to an IEnumerable (in-memory pagination)
    /// </summary>
    public static PagedResult<T> ToPagedResult<T>(
        this IEnumerable<T> source,
        int pageNumber,
        int pageSize)
    {
        if (pageNumber < 1)
            pageNumber = 1;
        
        if (pageSize < 1)
            pageSize = 10;
        
        if (pageSize > 100)
            pageSize = 100; // Maximum page size

        var items = source.ToList();
        var totalCount = items.Count;
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var pagedItems = items
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResult<T>
        {
            Items = pagedItems,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            HasPreviousPage = pageNumber > 1,
            HasNextPage = pageNumber < totalPages
        };
    }
}

/// <summary>
/// Represents a paginated result
/// </summary>
public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}

