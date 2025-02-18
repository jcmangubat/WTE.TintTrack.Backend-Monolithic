using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Infrastructure.Repositories;

public class PermissionRepository(ApplicationDbContext dbContext)
    : RepositoryForGuidKeyedEntity<Permission>(dbContext), IPermissionRepository
{ }
