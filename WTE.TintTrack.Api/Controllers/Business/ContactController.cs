using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging.Business.Request;
using WTE.TintTrack.Api.Messaging.Business.Responses;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

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