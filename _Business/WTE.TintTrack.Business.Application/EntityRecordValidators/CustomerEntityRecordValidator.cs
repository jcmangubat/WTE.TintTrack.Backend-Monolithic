using AutoMapper;
using SMEAppHouse.Core.Patterns.EF.Exceptions;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Common.Helpers;

namespace WTE.TintTrack.Business.Application.EntityRecordValidators;

public class CustomerRecordValidator(IMapper mapper, ICustomerRepository customerRepository)
    : IEntityRecordValidator<CustomerDto>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<bool> ExistAsync(CustomerDto entity) =>
        await _customerRepository.AnyAsync(p => p.Code == entity.Code);

    public CustomerDto ValidateRecordForInsert(CustomerDto entityDto)
    {
        if (entityDto.Id == Guid.Empty)
            entityDto.Id = Guid.NewGuid();

        if (string.IsNullOrEmpty(entityDto.Code))
            entityDto.Code = CodeGenerator.GenerateUniqueCode($"{entityDto.Name}{entityDto.Email}", FieldLengths.General.CODE);

        return entityDto;
    }
}
