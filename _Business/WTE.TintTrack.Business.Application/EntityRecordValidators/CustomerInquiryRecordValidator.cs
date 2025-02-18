using AutoMapper;
using SMEAppHouse.Core.Patterns.EF.Exceptions;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Common.Helpers;

namespace WTE.TintTrack.Business.Application.EntityRecordValidators;

public class CustomerInquiryRecordValidator(IMapper mapper, ICustomerInquiryRepository customerInquiryRepository)
    : IEntityRecordValidator<CustomerInquiryDto>
{
    private readonly ICustomerInquiryRepository _CustomerInquiryRepository = customerInquiryRepository;

    public async Task<bool> ExistAsync(CustomerInquiryDto entity) =>
        /*await _CustomerInquiryRepository.AnyAsync(p =>
            p.ConsultationDetails == entity.ConsultationDetails &&
            p.Budget == entity.Budget &&
            p.ContactMethod == entity.ContactMethod &&
            p.ConsultationDate == entity.ConsultationDate
        );*/
        await _CustomerInquiryRepository.AnyAsync(p =>
            p.Id == entity.Id
        );

    /*public async Task<CustomerInquiryDto> TransformAsync(CustomerInquiryDto entityDto)
    {
        var entity = await _CustomerInquiryRepository.GetSingleAsync(p => p.Id == entityDto.Id)
            ?? throw new EntityNotFoundException<CustomerInquiry>($"{nameof(CustomerInquiry)} is not found.");

        entity.DateModified = DateTime.UtcNow;
        if (entityDto.ConsultationDetails != entity.ConsultationDetails) entity.ConsultationDetails = entityDto.ConsultationDetails;
        if (entityDto.ConsultationDate != entity.ConsultationDate) entity.ConsultationDate = entityDto.ConsultationDate;
        if (entityDto.Budget != null) entity.Budget = entityDto.Budget;
        if (entityDto.ContactMethod != entity.ContactMethod) entity.ContactMethod = entityDto.ContactMethod;
        if (entityDto.FollowUpNeeded != null) entity.FollowUpNeeded = entityDto.FollowUpNeeded;
        if (entityDto.SalesRepUserCode != null) entity.SalesRepUserCode = entityDto.SalesRepUserCode;
        if (entityDto.SpecialRequests != null) entity.SpecialRequests = entityDto.SpecialRequests;
        if (entityDto.TintType != null) entity.TintType = entityDto.TintType;
        if (entityDto.ProposalCode != null) entity.ProposalCode = entityDto.ProposalCode;
        
        if (entityDto.IsActive != null) entity.IsActive = entityDto.IsActive;
        if (entityDto.IsArchived != null) entity.IsArchived = entityDto.IsArchived;
        if (entityDto.ReasonArchived != null) entity.ReasonArchived = entityDto.ReasonArchived;

        return mapper.Map<CustomerInquiryDto>(entity);
    }*/

    public CustomerInquiryDto ValidateRecordForInsert(CustomerInquiryDto entityDto)
    {
        if (entityDto.Id == Guid.Empty)
            entityDto.Id = Guid.NewGuid();

        /*if (string.IsNullOrEmpty(entityDto.Code))
            entityDto.Code = CodeGenerator.GenerateUniqueCode($"{entityDto.Email}{entityDto.FirstName}{entityDto.LastName}", FieldLengths.General.CODE);*/

        return entityDto;
    }
}
