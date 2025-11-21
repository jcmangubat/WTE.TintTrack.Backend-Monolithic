using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging.Business.Requests.Customer;
using WTE.TintTrack.Api.Messaging.Business.Requests.CustomerContact;
using WTE.TintTrack.Api.Messaging.Business.Responses.Customer;
using WTE.TintTrack.Api.Messaging.Business.Responses.CustomerContact;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Application.Shared.Messaging.Interface;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

/// <summary>
/// Controller for handling customer management operations.
/// </summary>
/// <remarks>
/// This controller provides comprehensive customer management functionality, including CRUD operations for customers and the ability to associate contacts with customers. It supports managing customer relationships, contact associations, and maintaining customer data throughout the business lifecycle.
/// </remarks>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class CustomerController(
        ILogger<CustomerController> logger,
        IMapper mapper,
        IMessageProviderService messageProviderService,
        ICustomerService customerService,
        IContactService contactService,
        ICustomerContactService customerContactService,
        
        ICRUDExtender<CustomerDto, CreateCustomerRequest, UpdateCustomerRequest> entityCRUDExtender,
        IValidator<CreateCustomerRequest> entityCreateRequestValidator,
        IValidator<UpdateCustomerRequest> entityUpdateRequestValidator,
        IValidator<CreateCustomerContactRequest> addCustomerContactRequestValidator
    )
    : CodedEntityOperationsControllerBase<CustomerController, CustomerDto, CustomerResponse, CreateCustomerRequest, UpdateCustomerRequest>(
            logger, mapper, messageProviderService, customerService, entityCRUDExtender, entityCreateRequestValidator, entityUpdateRequestValidator)
{
    private readonly IValidator<CreateCustomerContactRequest> _addCustomerContactRequestValidator = addCustomerContactRequestValidator;

    private readonly IContactService _contactService = contactService;
    private readonly ICustomerContactService _customerContactService = customerContactService;

    [HttpPost("add-contact")]
    [ProducesResponseType(typeof(IApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationFailureApiResponse<dynamic>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceFailureApiResponse<IEnumerable<string>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceFailureApiResponse<dynamic>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddCustomerContact(CreateCustomerContactRequest request)
    {
        var validationResult = await _addCustomerContactRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<CreateCustomerContactRequest>(request, validationResult);
            return CreateApiResponse(validationResponse);
        }

        var contact = await _contactService.FindSingleAsync(p => p.Code == request.ContactCode);
        if (contact == null)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR020");
            var failureResponse = new ServiceFailureApiResponse<CreateCustomerContactRequest>(
                                            request,
                                            apiMsg.Code, apiMsg.Message,
                                            statusCode: StatusCodes.Status400BadRequest
                                        );

            return CreateApiResponse(failureResponse);
        }

        var customer = await _crudService.FindSingleAsync(p => p.Code == request.CustomerCode);
        if (customer == null)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR021");
            var failureResponse = new ServiceFailureApiResponse<CreateCustomerContactRequest>(
                                            request,
                                            apiMsg.Code, apiMsg.Message,
                                            statusCode: StatusCodes.Status400BadRequest
                                        );

            return CreateApiResponse(failureResponse);
        }

        var customerContact = await _customerContactService.FindSingleAsync(dto => dto.ContactId == contact.Id && dto.CustomerId == customer.Id);

        if (customerContact != null)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR022");
            var failureResponse = new ServiceFailureApiResponse<CreateCustomerContactRequest>(
                                            request,
                                            apiMsg.Code, apiMsg.Message,
                                            statusCode: StatusCodes.Status400BadRequest
                                        );

            return CreateApiResponse(failureResponse);
        }

        customerContact = new CustomerContactDto()
        {
            Id = Guid.NewGuid(),
            ContactId = contact.Id,
            CustomerId = customer.Id,
            RelationshipType = request.RelationshipType
        };
        customerContact = await _customerContactService.AddAsync(customerContact);

        var customerContactResponse = Mapper.Map<CustomerContactResponse>(customerContact);
        customerContactResponse.CustomerCode = customer.Code;
        customerContactResponse.ContactCode = contact.Code;

        var infApiMsg = MessageProviderService.GetMessage("INF005");
        var successResponse = new DefaultApiResponse<CustomerContactResponse>(customerContactResponse, StatusCodes.Status201Created, infApiMsg.Message);
        return CreateApiResponse(successResponse);
    }
}
