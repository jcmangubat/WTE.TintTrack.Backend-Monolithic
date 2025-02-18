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
public class QuoteController(
    ILogger<QuoteController> logger,
    IMapper mapper,
    IQuoteService quoteService)
    : CodedEntityOperationsControllerBase<QuoteController, QuoteDto>(logger, mapper, quoteService)
{
}