using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs;

namespace WTE.TintTrack.Business.Application.Interfaces;

/// <summary>
/// Represents the service interface for managing audit logs in the domain.
/// </summary>
public interface IAuditLogService : IMappedLoggingService<IAuditLogService>
{
    /// <summary>
    /// Retrieves an audit log by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the audit log.</param>
    /// <returns>An audit log entity if found; otherwise, null.</returns>
    Task<AuditLogDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Retrieves a collection of audit logs based on their identifiers.
    /// </summary>
    /// <param name="auditLogIds">A collection of audit log identifiers.</param>
    /// <returns>A collection of matching audit log entities.</returns>
    Task<IEnumerable<AuditLogDto>> GetByIdsAsync(IEnumerable<Guid> auditLogIds);

    /// <summary>
    /// Retrieves a collection of audit logs associated with a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A collection of audit logs associated with the user.</returns>
    Task<IEnumerable<AuditLogDto>> GetByUserAsync(Guid userId);

    /// <summary>
    /// Deletes an audit log by its unique identifier.
    /// </summary>
    /// <param name="auditLogId">The unique identifier of the audit log to delete.</param>
    Task DeleteAsync(Guid auditLogId);

    /// <summary>
    /// Create an audit log
    /// </summary>
    /// <param name="auditLogDto"></param>
    /// <returns></returns>
    Task<AuditLogDto> CreateLog(AuditLogDto auditLogDto);
}
