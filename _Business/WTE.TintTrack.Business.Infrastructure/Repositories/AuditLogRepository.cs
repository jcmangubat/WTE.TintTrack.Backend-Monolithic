using Microsoft.EntityFrameworkCore;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;
using WTE.TintTrack.Business.Domain.Entities;
using SMEAppHouse.Core.Patterns.Repo;

namespace WTE.TintTrack.Business.Infrastructure.Repositories;

public class AuditLogRepository(TenantDbContext dbContext) : RepositoryForGuidKeyedEntity<AuditLog>(dbContext), IAuditLogRepository
{
    private readonly TenantDbContext _dbContext = dbContext;

    public async Task<AuditLog?> GetByIdAsync(Guid id)
    {
        return await _dbContext.AuditLogs.FindAsync(id);
    }

    public async Task<IEnumerable<AuditLog>> GetByIdsAsync(IEnumerable<Guid> auditLogIds)
    {
        return await _dbContext.AuditLogs.Where(t => auditLogIds.Contains(t.Id)).ToListAsync();
    }

    public async Task<IEnumerable<AuditLog>> GetByUserAsync(Guid userId)
    {
        return await _dbContext.AuditLogs.Where(t => t.UserId == userId).ToListAsync();
    }
}
