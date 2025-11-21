using AutoMapper;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Business.Application.Services;

public class ProjectMilestoneService(
    IMapper mapper,
    ILogger<ProjectMilestoneService> logger,
    IMessageProviderService messageProviderService,
    IProjectMilestoneRepository repository)
    : MappedLoggingServiceWithCRUD<IProjectMilestoneService, IProjectMilestoneRepository, ProjectMilestone, ProjectMilestoneDto>(
        mapper, logger, messageProviderService, repository), IProjectMilestoneService
{
    /*public async Task<ProjectMilestoneDto?> GetByCodeAsync(string code)
    {
        var entity = await Repository.GetSingleAsync(p => p.Code == code);
        return Mapper.Map<ProposalDto>(entity);
    }*/
}
