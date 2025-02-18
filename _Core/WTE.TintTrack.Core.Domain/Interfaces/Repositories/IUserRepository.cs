using System.Linq.Expressions;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<ApplicationUser?> GetByIdAsync(Guid userId);
    Task<ApplicationUser?> GetByEmailAsync(string email);
    Task<ApplicationUser?> GetByUserCodeAsync(string userCode);
    
    Task CreateAsync(ApplicationUser user, string password);
    Task UpdateAsync(ApplicationUser user);
    Task CommitChangesAsync();
    Task DeleteAsync(ApplicationUser user);
    Task<IEnumerable<ApplicationUser>> GetAllAsync(bool? activeOnly = null);
    Task<IEnumerable<ApplicationUser>> GetAllAsync(Expression<Func<ApplicationUser, bool>> entityPredicate);
    Task<IEnumerable<ApplicationUser>> GetAllByTenantAsync(string tenantCode, bool? activeOnly = null);
    Task<IEnumerable<ApplicationUser>> GetAllByTenantAsync(Guid tenantId, bool? activeOnly = null);

    Task<bool> AnyAsync(Expression<Func<ApplicationUser, bool>> whereExpression);
}
