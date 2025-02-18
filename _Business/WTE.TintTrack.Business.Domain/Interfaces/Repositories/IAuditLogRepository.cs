using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using WTE.TintTrack.Business.Domain.Entities;

namespace WTE.TintTrack.Business.Domain.Interfaces.Repositories;

public interface IAuditLogRepository : IRepositoryForKeyedEntity<AuditLog, Guid>
{
    Task<AuditLog?> GetByIdAsync(Guid id);
    Task<IEnumerable<AuditLog>> GetByIdsAsync(IEnumerable<Guid> auditLogIds);
    Task<IEnumerable<AuditLog>> GetByUserAsync(Guid userId);
}
