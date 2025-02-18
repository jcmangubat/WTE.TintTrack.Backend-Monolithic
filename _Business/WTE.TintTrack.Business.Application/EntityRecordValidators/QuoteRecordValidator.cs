using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Application.EntityRecordValidators;

public class QuoteRecordValidator(IQuoteRepository quoteRepository)
    : IEntityRecordValidator<QuoteDto>
{
    private readonly IQuoteRepository _quoteRepository = quoteRepository;

    public async Task<bool> ExistAsync(QuoteDto entity) =>
        await _quoteRepository.AnyAsync(p => p.Code == entity.Code);

    /*public async Task<QuoteDto> TransformAsync(QuoteDto entityDto)
    {
        return entityDto;
    }*/

    public QuoteDto ValidateRecordForInsert(QuoteDto entityDto)
    {
        if (entityDto.Id == Guid.Empty)
            entityDto.Id = Guid.NewGuid();

        /*if (string.IsNullOrEmpty(entityDto.Code))
            entityDto.Code = CodeGenerator.GenerateUniqueCode($"{entityDto.Email}{entityDto.FirstName}{entityDto.LastName}", FieldLengths.General.CODE);*/

        return entityDto;
    }
}