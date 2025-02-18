using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;

namespace WTE.TintTrack.Application.Shared.Interfaces;

public interface IEntityRecordValidator<TEntityDto>
{
    Task<bool> ExistAsync(TEntityDto entity);
    /*Task<TEntityDto> TransformAsync(TEntityDto entityDto)*/
    TEntityDto ValidateRecordForInsert(TEntityDto entityDto);
}