using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging.Business.Requests.Inquiry;
using WTE.TintTrack.Api.Messaging.Business.Responses.Inquiry;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

/// <summary>
/// Controller for handling inquiry operations.
/// </summary>
/// <remarks>
/// This controller manages inquiries, which represent customer requests for information or initial interest in products or services. It provides operations for creating, reading, updating, and deleting inquiry records, enabling businesses to track and manage potential leads and customer interest.
/// </remarks>
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
