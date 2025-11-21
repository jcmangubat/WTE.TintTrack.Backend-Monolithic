using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging.Business.Requests.Contact;
using WTE.TintTrack.Api.Messaging.Business.Responses.Contact;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

/// <summary>
/// Controller for handling contact management operations.
/// </summary>
/// <remarks>
/// This controller provides CRUD operations for managing contacts in the system. It supports creating, reading, updating, and deleting contact records with coded entity operations. Contacts represent individuals or entities that can be associated with customers and other business entities.
/// </remarks>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ContactController(
    ILogger<ContactController> logger,
    IMapper mapper,
    IMessageProviderService messageProviderService,
    IContactService contactService,
    ICRUDExtender<ContactDto, CreateContactRequest, UpdateContactRequest> entityCRUDExtender,
    IValidator<CreateContactRequest> entityCreateRequestValidator,
    IValidator<UpdateContactRequest> entityUpdateRequestValidator)
    : CodedEntityOperationsControllerBase<ContactController, ContactDto, ContactResponse, CreateContactRequest, UpdateContactRequest>(
            logger, mapper, messageProviderService, contactService, entityCRUDExtender, entityCreateRequestValidator, entityUpdateRequestValidator
        )
{
}