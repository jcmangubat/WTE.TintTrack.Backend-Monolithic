using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging.Business.Requests.Quote;
using WTE.TintTrack.Api.Messaging.Business.Requests.WorkOrder;
using WTE.TintTrack.Api.Messaging.Business.Responses.Quote;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs.CommercialOffersModels;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

/// <summary>
/// Controller for handling quote operations.
/// </summary>
/// <remarks>
/// This controller manages quotes, which are formal price quotations provided to customers for products or services. It provides CRUD operations for creating, reading, updating, and deleting quotes, allowing businesses to generate, track, and manage pricing information for customer requests.
/// </remarks>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class QuoteController(
    ILogger<QuoteController> logger,
    IMapper mapper,
    IMessageProviderService messageProviderService,
    IQuoteService QuoteService,
    ICRUDExtender<QuoteDto, CreateQuoteRequest, UpdateWorkOrderItemRequest> entityCRUDExtender,
    IValidator<CreateQuoteRequest> entityCreateRequestValidator,
    IValidator<UpdateWorkOrderItemRequest> entityUpdateRequestValidator)
    : CodedEntityOperationsControllerBase<QuoteController, QuoteDto, QuoteResponse, CreateQuoteRequest, UpdateWorkOrderItemRequest>(
            logger, mapper, messageProviderService, QuoteService, entityCRUDExtender, entityCreateRequestValidator, entityUpdateRequestValidator)
{
}
