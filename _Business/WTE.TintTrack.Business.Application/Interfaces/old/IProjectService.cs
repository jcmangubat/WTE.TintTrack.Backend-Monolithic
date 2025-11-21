using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Application.Interfaces;

public interface IProjectService : IMappedLoggingServiceWithCRUD<IProjectService, IProjectRepository, Project, ProjectDto>
{
    Task<ProjectDto?> GetByCodeAsync(string code);
}
