using AutoMapper;
using System.Linq.Expressions;
using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Api.Messaging.Business.Request;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Api.Messaging._CRUDExtenders;

public class CustomerCRUDExtender(ILogger<CustomerCRUDExtender> logger, IMapper mapper, ICustomerRepository repository)
    : CRUDExtenderBase<ICustomerRepository, CustomerDto, CreateCustomerRequest, UpdateCustomerRequest>(logger, mapper, repository)
{
    public override CustomerDto TransformForUpdate(CustomerDto entityDto, UpdateCustomerRequest entityUpdateRequest)
    {
        entityDto = base.TransformForUpdate(entityDto, entityUpdateRequest);

        if (entityUpdateRequest.CustomerStatus != null) entityDto.CustomerStatus = entityUpdateRequest.CustomerStatus.Value;
        if (entityUpdateRequest.AddressLine2 != null) entityDto.AddressLine2 = entityUpdateRequest.AddressLine2;
        if (entityUpdateRequest.CreatedBy != null) entityDto.CreatedBy = entityUpdateRequest.CreatedBy;
        if (entityUpdateRequest.City != null) entityDto.City = entityUpdateRequest.City;
        if (entityUpdateRequest.CountryISOCode != null) entityDto.CountryISOCode = entityUpdateRequest.CountryISOCode;
        if (entityUpdateRequest.Email != null) entityDto.Email = entityUpdateRequest.Email;
        if (entityUpdateRequest.PostalCode != null) entityDto.PostalCode = entityUpdateRequest.PostalCode;
        if (entityUpdateRequest.StateOrRegion != null) entityDto.StateOrRegion = entityUpdateRequest.StateOrRegion;
        if (entityUpdateRequest.StreetAddress != null) entityDto.StreetAddress = entityUpdateRequest.StreetAddress;
        if (entityUpdateRequest.Company != null) entityDto.Company = entityUpdateRequest.Company;
        if (entityUpdateRequest.Name != null) entityDto.Name = entityUpdateRequest.Name;
        if (entityUpdateRequest.Phone != null) entityDto.Phone = entityUpdateRequest.Phone;
        if (entityUpdateRequest.Phone2 != null) entityDto.Phone2 = entityUpdateRequest.Phone2;

        return entityDto;
    }

    public override async Task<bool> ExistAsync(CustomerDto entity)
    {
        return await _repository.AnyAsync(p => p.Code == entity.Code);
    }

    public override Expression<Func<CustomerDto, object>>[]? GetIncludes() =>
    [
        dto => dto.CustomerContacts
    ];
}
