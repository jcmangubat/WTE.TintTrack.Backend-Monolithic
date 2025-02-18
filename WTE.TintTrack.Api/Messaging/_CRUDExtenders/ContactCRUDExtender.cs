using AutoMapper;
using System.Linq.Expressions;
using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Api.Messaging.Business.Request;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Api.Messaging._CRUDExtenders;

public class ContactCRUDExtender(ILogger<ContactCRUDExtender> logger, IMapper mapper, IContactRepository repository)
    : CRUDExtenderBase<IContactRepository, ContactDto, CreateContactRequest, UpdateContactRequest>(logger, mapper, repository)
{
    public override ContactDto TransformForUpdate(ContactDto entityDto, UpdateContactRequest entityUpdateRequest)
    {
        entityDto = base.TransformForUpdate(entityDto, entityUpdateRequest);

        if (entityUpdateRequest.FirstName != null) entityDto.FirstName = entityUpdateRequest.FirstName;
        if (entityUpdateRequest.LastName != null) entityDto.LastName = entityUpdateRequest.LastName;
        if (entityUpdateRequest.Phone != null) entityDto.Phone = entityUpdateRequest.Phone;
        if (entityUpdateRequest.Mobile != null) entityDto.Mobile = entityUpdateRequest.Mobile;
        if (entityUpdateRequest.AltPhone != null) entityDto.AltPhone = entityUpdateRequest.AltPhone;
        if (entityUpdateRequest.Email != null) entityDto.Email = entityUpdateRequest.Email;
        if (entityUpdateRequest.StreetAddress != null) entityDto.StreetAddress = entityUpdateRequest.StreetAddress;
        if (entityUpdateRequest.AddressLine2 != null) entityDto.AddressLine2 = entityUpdateRequest.AddressLine2;
        if (entityUpdateRequest.City != null) entityDto.City = entityUpdateRequest.City;
        if (entityUpdateRequest.StateOrRegion != null) entityDto.StateOrRegion = entityUpdateRequest.StateOrRegion;
        if (entityUpdateRequest.PostalCode != null) entityDto.PostalCode = entityUpdateRequest.PostalCode;
        if (entityUpdateRequest.CountryISOCode != null) entityDto.CountryISOCode = entityUpdateRequest.CountryISOCode;
        if (entityUpdateRequest.IsImported != null) entityDto.IsImported = entityUpdateRequest.IsImported;
        if (entityUpdateRequest.Tags != null) entityDto.Tags = entityUpdateRequest.Tags;
        if (entityUpdateRequest.DateOfBirth != null) entityDto.DateOfBirth = entityUpdateRequest.DateOfBirth;
        if (entityUpdateRequest.Website != null) entityDto.Website = entityUpdateRequest.Website;
        if (entityUpdateRequest.ContactType != null) entityDto.ContactType = entityUpdateRequest.ContactType.Value;
        if (entityUpdateRequest.JobTitle != null) entityDto.JobTitle = entityUpdateRequest.JobTitle;
        if (entityUpdateRequest.Notes != null) entityDto.Notes = entityUpdateRequest.Notes;

        return entityDto;
    }

    public override async Task<bool> ExistAsync(ContactDto entity)
    {
        return await _repository.AnyAsync(p => p.Code == entity.Code);
    }

    public override Expression<Func<ContactDto, object>>[]? GetIncludes() =>
    [
        dto => dto.CustomerContacts
    ];
}
