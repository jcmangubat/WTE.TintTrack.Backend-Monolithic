using AutoMapper;
using SMEAppHouse.Core.Patterns.EF.Exceptions;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Common.Helpers;

namespace WTE.TintTrack.Business.Application.EntityRecordValidators;

public class ContactRecordValidator(IMapper mapper, IContactRepository contactRepository)
    : IEntityRecordValidator<ContactDto>
{
    private readonly IContactRepository _contactRepository = contactRepository;

    public async Task<bool> ExistAsync(ContactDto entity) =>
        await _contactRepository.AnyAsync(p => p.Code == entity.Code);

/*    public async Task<ContactDto> TransformAsync(ContactDto entityDto)
    {
        var entity = await _contactRepository.GetSingleAsync(p => p.Code == entityDto.Code)
            ?? throw new EntityNotFoundException<Contact>($"{nameof(Contact)} is not found.");

        entity.DateModified = DateTime.UtcNow;
        if (entityDto.Notes != null) entity.Notes = entityDto.Notes;
        if (entityDto.Tags != null) entity.Tags = entityDto.Tags;
        if (entityDto.Mobile != null) entity.Mobile = entityDto.Mobile;
        if (entityDto.AddressLine2 != null) entity.AddressLine2 = entityDto.AddressLine2;
        if (entityDto.AltPhone != null) entity.AltPhone = entityDto.AltPhone;
        if (entityDto.City != null) entity.City = entityDto.City;
        if (entityDto.ContactType != entity.ContactType) entity.ContactType = entityDto.ContactType;
        if (entityDto.CountryISOCode != null) entity.CountryISOCode = entityDto.CountryISOCode;
        if (entityDto.DateOfBirth != null) entity.DateOfBirth = entityDto.DateOfBirth;
        if (entityDto.Email != null) entity.Email = entityDto.Email;
        if (entityDto.FirstName != entity.FirstName) entity.FirstName = entityDto.FirstName;
        if (entityDto.LastName != null) entity.LastName = entityDto.LastName;
        if (entityDto.JobTitle != null) entity.JobTitle = entityDto.JobTitle;
        if (entityDto.ContactType != entity.ContactType) entity.ContactType = entityDto.ContactType;
        if (entityDto.PostalCode != null) entity.PostalCode = entityDto.PostalCode;
        if (entityDto.StateOrRegion != null) entity.StateOrRegion = entityDto.StateOrRegion;
        if (entityDto.StreetAddress != null) entity.StreetAddress = entityDto.StreetAddress;
        if (entityDto.Website != null) entity.Website = entityDto.Website;

        if (entityDto.IsActive != null) entity.IsActive = entityDto.IsActive;
        if (entityDto.IsArchived != null) entity.IsArchived = entityDto.IsArchived;
        if (entityDto.ReasonArchived != null) entity.ReasonArchived = entityDto.ReasonArchived;

        return mapper.Map<ContactDto>(entity);
    }*/

    public ContactDto ValidateRecordForInsert(ContactDto entityDto)
    {
        if (entityDto.Id == Guid.Empty)
            entityDto.Id = Guid.NewGuid();

        if (string.IsNullOrEmpty(entityDto.Code))
            entityDto.Code = CodeGenerator.GenerateUniqueCode($"{entityDto.Email}{entityDto.FirstName}{entityDto.LastName}", FieldLengths.General.CODE);

        return entityDto;
    }
}
