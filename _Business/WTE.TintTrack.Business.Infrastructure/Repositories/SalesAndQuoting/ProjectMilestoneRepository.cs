using SMEAppHouse.Core.Patterns.Repo;

namespace WTE.TintTrack.Business.Infrastructure.Repositories;

public class ProjectMilestoneRepository(TenantDbContext dbContext) : RepositoryForGuidKeyedEntity<ProjectMilestone>(dbContext), IProjectMilestoneRepository { }
