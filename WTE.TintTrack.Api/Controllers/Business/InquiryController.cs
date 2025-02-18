using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging.Business.Request;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InquiryController(
    ILogger<InquiryController> logger,
    IMapper mapper,
    IMessageProviderService messageProviderService,
    IInquiryService inquiryService,
    ICRUDExtender<InquiryDto, CreateInquiryRequest, UpdateInquiryRequest> entityCRUDExtender,
    IValidator<CreateInquiryRequest> entityCreateRequestValidator)
    : EntityOperationsControllerBase<InquiryController, InquiryDto, InquiryResponse, CreateInquiryRequest, UpdateInquiryRequest>(
        logger, mapper, messageProviderService, inquiryService, entityCreateRequestValidator, entityCRUDExtender
    )
{
}
