using WTE.TintTrack.Application.Shared.ServiceAbstractions;

namespace WTE.TintTrack.Business.Application.Interfaces;

public interface IProjectMilestoneService : IMappedLoggingServiceWithCRUD<IProjectMilestoneService, IProjectMilestoneRepository, ProjectMilestone, ProjectMilestoneDto>
{
    //Task<ProjectMilestoneDto?> GetByCodeAsync(string code);
}
