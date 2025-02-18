using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Application.EntityRecordValidators;

public class ProposalRecordValidator(IProposalRepository proposalRepository)
    : IEntityRecordValidator<ProposalDto>
{
    private readonly IProposalRepository _proposalRepository = proposalRepository;

    public async Task<bool> ExistAsync(ProposalDto entity) =>
        await _proposalRepository.AnyAsync(p => p.Code == entity.Code);

    /*public async Task<ProposalDto> TransformAsync(ProposalDto entityDto)
    {
        return entityDto;
    }*/

    public ProposalDto ValidateRecordForInsert(ProposalDto entityDto)
    {
        if (entityDto.Id == Guid.Empty)
            entityDto.Id = Guid.NewGuid();

        /*if (string.IsNullOrEmpty(entityDto.Code))
            entityDto.Code = CodeGenerator.GenerateUniqueCode($"{entityDto.Email}{entityDto.FirstName}{entityDto.LastName}", FieldLengths.General.CODE);*/

        return entityDto;
    }
}
