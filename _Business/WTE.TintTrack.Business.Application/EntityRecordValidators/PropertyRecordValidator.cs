using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Application.EntityRecordValidators;

public class PropertyRecordValidator(ICustomerPropertyRepository propertyRepository)
    : IEntityRecordValidator<PropertyDto>
{
    private readonly ICustomerPropertyRepository _propertyRepository = propertyRepository;

    public async Task<bool> ExistAsync(PropertyDto entity) =>
        await _propertyRepository.AnyAsync(p => p.Code == entity.Code);

    /*public async Task<PropertyDto> TransformAsync(PropertyDto entityDto)
    {
        return entityDto;
    }*/

    public PropertyDto ValidateRecordForInsert(PropertyDto entityDto)
    {
        if (entityDto.Id == Guid.Empty)
            entityDto.Id = Guid.NewGuid();

        /*if (string.IsNullOrEmpty(entityDto.Code))
            entityDto.Code = CodeGenerator.GenerateUniqueCode($"{entityDto.Email}{entityDto.FirstName}{entityDto.LastName}", FieldLengths.General.CODE);*/

        return entityDto;
    }
}
