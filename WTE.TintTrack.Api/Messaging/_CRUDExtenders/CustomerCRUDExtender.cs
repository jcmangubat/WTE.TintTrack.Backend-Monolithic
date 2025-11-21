using AutoMapper;
using System.Linq.Expressions;
using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Api.Messaging.Business.Requests.Customer;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Api.Messaging._CRUDExtenders;

public class CustomerCRUDExtender(ILogger<CustomerCRUDExtender> logger, IMapper mapper, ICustomerRepository repository)
    : CRUDExtenderBase<ICustomerRepository, CustomerDto, CreateCustomerRequest, UpdateCustomerRequest>(logger, mapper, repository)
{
    public override CustomerDto TransformForUpdate(CustomerDto entityDto, UpdateCustomerRequest entityUpdateRequest)
    {
        entityDto = base.TransformForUpdate(entityDto, entityUpdateRequest);

        /*if (entityUpdateRequest.CustomerStatus != null) entityDto.CustomerStatus = entityUpdateRequest.CustomerStatus.Value;
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
        if (entityUpdateRequest.Phone2 != null) entityDto.Phone2 = entityUpdateRequest.Phone2;*/

        if (entityUpdateRequest.Code != null) entityDto.Code = entityUpdateRequest.Code;
        if (entityUpdateRequest.Name != null) entityDto.Name = entityUpdateRequest.Name;
        if (entityUpdateRequest.IndustryType != null) entityDto.IndustryType = entityUpdateRequest.IndustryType;
        if (entityUpdateRequest.GeneralEmail != null) entityDto.GeneralEmail = entityUpdateRequest.GeneralEmail;
        if (entityUpdateRequest.MainPhone != null) entityDto.MainPhone = entityUpdateRequest.MainPhone;
        if (entityUpdateRequest.Website != null) entityDto.Website = entityUpdateRequest.Website;
        if (entityUpdateRequest.CustomerStatus != null) entityDto.CustomerStatus = entityUpdateRequest.CustomerStatus.Value;
        if (entityUpdateRequest.IsImported != null) entityDto.IsImported = entityUpdateRequest.IsImported.Value;
        if (entityUpdateRequest.Notes != null) entityDto.Notes = entityUpdateRequest.Notes;
        if (entityUpdateRequest.Tags != null) entityDto.Tags = entityUpdateRequest.Tags;
        if (entityUpdateRequest.TaxExemptionReason != null) entityDto.TaxExemptionReason = entityUpdateRequest.TaxExemptionReason;

        /*// Navigation properties for related entities
        public virtual IEnumerable<CustomerContactDto> CustomerContacts { get; set; } = [];
        public IEnumerable<string> ContactCodes { get; set; } = [];
        public virtual IEnumerable<PropertyDto> CustomerProperties { get; set; } = [];
        public IEnumerable<string> AddressCodes { get; set; } = [];
        public virtual IEnumerable<AddressDto> Addresses { get; set; } = [];*/

        return entityDto;
    }

    public override async Task<bool> ExistAsync(CustomerDto entity)
    {
        return await _repository.AnyAsync(p => p.Code == entity.Code);
    }

    public override Expression<Func<CustomerDto, object>>[]? GetIncludes() =>
    [
        //dto => dto.CustomerContacts
    ];
}
