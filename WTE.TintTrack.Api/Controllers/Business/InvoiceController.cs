using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging.Business.Requests.Invoice;
using WTE.TintTrack.Api.Messaging.Business.Requests.WorkOrder;
using WTE.TintTrack.Api.Messaging.Business.Responses.Invoice;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs.SalesAndQuotingModels;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

/// <summary>
/// Controller for handling invoice operations.
/// </summary>
/// <remarks>
/// This controller manages invoices, which are billing documents issued to customers for products or services provided. It provides CRUD operations for creating, reading, updating, and deleting invoice records, enabling businesses to generate invoices, track billing information, and manage financial transactions with customers.
/// </remarks>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InvoiceController(
    ILogger<InvoiceController> logger,
    IMapper mapper,
    IMessageProviderService messageProviderService,
    IInvoiceService InvoiceService,

    ICRUDExtender<InvoiceDto, CreateInvoiceRequest, UpdateWorkOrderItemRequest> entityCRUDExtender,
    IValidator<CreateInvoiceRequest> entityCreateRequestValidator,
    IValidator<UpdateWorkOrderItemRequest> entityUpdateRequestValidator)
    : CodedEntityOperationsControllerBase<InvoiceController, InvoiceDto, InvoiceResponse, CreateInvoiceRequest, UpdateWorkOrderItemRequest>(
            logger, mapper, messageProviderService, InvoiceService, entityCRUDExtender, entityCreateRequestValidator, entityUpdateRequestValidator)
{
}
