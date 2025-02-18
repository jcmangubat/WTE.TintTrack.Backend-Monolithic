using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InvoiceController(
    ILogger<InvoiceController> logger,
    IMapper mapper,
    IInvoiceService invoiceService)
    : CodedEntityOperationsControllerBase<InvoiceController, InvoiceDto>(logger, mapper, invoiceService)
{
}
