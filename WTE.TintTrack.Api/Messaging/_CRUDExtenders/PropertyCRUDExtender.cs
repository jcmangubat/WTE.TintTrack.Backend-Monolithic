using AutoMapper;
using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Api.Messaging.Business.Requests.PropertyAsset;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Api.Messaging._CRUDExtenders;

public class PropertyCRUDExtender(ILogger<PropertyCRUDExtender> logger, IMapper mapper, IPropertyAssetRepository repository)
    : CRUDExtenderBase<IPropertyAssetRepository, PropertyAssetDto, CreatePropertyAssetRequest, UpdatePropertyAssetRequest>(logger, mapper, repository)
{
    public override PropertyAssetDto TransformForUpdate(PropertyAssetDto entityDto, UpdatePropertyAssetRequest entityUpdateRequest)
    {
        entityDto = base.TransformForUpdate(entityDto, entityUpdateRequest);

        if (entityUpdateRequest.Name != null) entityDto.Name = entityUpdateRequest.Name;
        if (entityUpdateRequest.Description != null) entityDto.Description = entityUpdateRequest.Description;

        /*if (entityUpdateRequest.AddressLine2 != null) entityDto.AddressLine2 = entityUpdateRequest.AddressLine2;
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
        if (entityUpdateRequest.Phone2 != null) entityDto.Phone2 = entityUpdateRequest.Phone2;*/

        return entityDto;
    }

    public override async Task<bool> ExistAsync(PropertyAssetDto entity)
    {
        return await _repository.AnyAsync(p => p.Code == entity.Code);
    }

    /*public override Expression<Func<PropertyDto, object>>[]? GetIncludes() =>
    [
        dto => dto.Customer
    ];*/
}
