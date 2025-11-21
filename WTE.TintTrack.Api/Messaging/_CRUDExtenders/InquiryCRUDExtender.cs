using AutoMapper;
using SMEAppHouse.Core.CodeKits.Helpers;
using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Api.Messaging.Business.Requests.Inquiry;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Common.Exceptions;
using WTE.TintTrack.Common.Helpers;

namespace WTE.TintTrack.Api.Messaging._CRUDExtenders;

public class InquiryCRUDExtender(ILogger<InquiryCRUDExtender> logger, IMapper mapper, IInquiryRepository repository, ICustomerService customerService)
    : CRUDExtenderBase<IInquiryRepository, InquiryDto, CreateInquiryRequest, UpdateInquiryRequest>(logger, mapper, repository)
{
    private readonly ICustomerService _customerService = customerService;

    public override InquiryDto TransformForUpdate(InquiryDto entityDto, UpdateInquiryRequest entityUpdateRequest)
    {
        entityDto = base.TransformForUpdate(entityDto, entityUpdateRequest);

        if (entityUpdateRequest.LeadSource != null) entityDto.LeadSource = entityUpdateRequest.LeadSource.Value;
        if (entityUpdateRequest.ConsultationDate != null) entityDto.ConsultationDate = entityUpdateRequest.ConsultationDate.Value;
        if (entityUpdateRequest.Subject != null) entityDto.Subject = entityUpdateRequest.Subject;
        if (entityUpdateRequest.Details != null) entityDto.Details = entityUpdateRequest.Details;
        if (entityUpdateRequest.PropertyType != null) entityDto.PropertyType = entityUpdateRequest.PropertyType.Value;

        if (entityUpdateRequest.Budget != null) entityDto.Budget = entityUpdateRequest.Budget;
        if (entityUpdateRequest.TintType != null) entityDto.TintType = entityUpdateRequest.TintType;
        if (entityUpdateRequest.SpecialRequests != null) entityDto.SpecialRequests = entityUpdateRequest.SpecialRequests;
        if (entityUpdateRequest.FollowUpNeeded != null) entityDto.FollowUpNeeded = entityUpdateRequest.FollowUpNeeded;
        //if (entityUpdateRequest.ProposalCode != null) entityDto.ProposalCode = entityUpdateRequest.ProposalCode;

        if (entityUpdateRequest.SalesRepUserCode != null) entityDto.SalesRepUserCode = entityUpdateRequest.SalesRepUserCode;

        return entityDto;
    }

    public override async Task<(bool Success, InquiryDto? createdEntity)> ExecuteAlternativeAsync(CreateInquiryRequest createEntityRequest)
    {
        try
        {
            CustomerDto? customer = null;
            if (!string.IsNullOrEmpty(createEntityRequest.CustomerCode))
                customer = await _customerService.FindSingleAsync(p => p.Code == createEntityRequest.CustomerCode)
                    ?? throw new RecordNotFoundException("Customer not found by the code in the request.");
            else
            {
                try
                {
                    var custCode = CodeGenerator.GenerateUniqueCode($"{createEntityRequest.Name}{createEntityRequest.GeneralEmail}", FieldLengths.Customer.Code);
                    customer = await _customerService.FindSingleAsync(p => p.Code == custCode);
                    if (customer == null)
                    {
                        customer = new CustomerDto
                        {
                            Code = custCode,
                            Id = Guid.NewGuid(),
                            Name = createEntityRequest.Name,
                            MainPhone = createEntityRequest.MainPhone,
                            GeneralEmail = createEntityRequest.GeneralEmail,
                            CustomerStatus = Consts.CustomerStatusEnum.Lead
                        };
                        await _customerService.AddAsync(customer);
                    }
                }
                catch (Exception ex)
                {
                    throw new ServiceOperationException("Error creating the customer entity.", ex);
                }
            }

            var entityDto = _mapper.Map<InquiryDto>(createEntityRequest);

            //entityDto.CustomerCode= customer.Code;

            return (true, entityDto);
        }
        catch (RecordNotFoundException ex)
        {
            _logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (ServiceOperationException ex)
        {
            _logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
    }
}
