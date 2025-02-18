using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Infrastructure.Repositories;

public class RolePermissionRepository(ApplicationDbContext dbContext)
    : RepositoryForGuidKeyedEntity<RolePermission>(dbContext), IRolePermissionRepository
{ }