using AutoMapper;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Application.Services;

public class ProjectService(
    IMapper mapper,
    ILogger<ProjectService> logger,
    IMessageProviderService messageProviderService,
    IProjectRepository repository)
    : MappedLoggingServiceWithCRUD<IProjectService, IProjectRepository, Project, ProjectDto>(
        mapper, logger, messageProviderService, repository), IProjectService
{
    public async Task<ProjectDto?> GetByCodeAsync(string code)
    {
        var entity = await Repository.GetSingleAsync(p => p.Code == code);
        return Mapper.Map<ProjectDto>(entity);
    }
}
