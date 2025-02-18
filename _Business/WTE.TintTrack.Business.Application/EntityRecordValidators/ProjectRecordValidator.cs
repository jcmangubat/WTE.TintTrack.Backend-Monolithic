using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Application.EntityRecordValidators;

public class ProjectRecordValidator(IProjectRepository projectRepository)
    : IEntityRecordValidator<ProjectDto>
{
    private readonly IProjectRepository _ProjectRepository = projectRepository;

    public async Task<bool> ExistAsync(ProjectDto entity) =>
        await _ProjectRepository.AnyAsync(p => p.Code == entity.Code);

    /*public async Task<ProjectDto> TransformAsync(ProjectDto entityDto)
    {
        return entityDto;
    }*/

    public ProjectDto ValidateRecordForInsert(ProjectDto entityDto)
    {
        if (entityDto.Id == Guid.Empty)
            entityDto.Id = Guid.NewGuid();

        /*if (string.IsNullOrEmpty(entityDto.Code))
            entityDto.Code = CodeGenerator.GenerateUniqueCode($"{entityDto.Email}{entityDto.FirstName}{entityDto.LastName}", FieldLengths.General.CODE);*/

        return entityDto;
    }
}
