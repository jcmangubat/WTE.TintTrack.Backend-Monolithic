using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Domain.Interfaces.Repositories;

public interface IRolePermissionRepository : IRepositoryForKeyedEntity<RolePermission, Guid>
{
}
