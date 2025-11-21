using AutoMapper;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Application.Services;

public class QuoteService(
    IMapper mapper,
    ILogger<QuoteService> logger,
    IMessageProviderService messageProviderService,
    IQuoteRepository repository)
    : MappedLoggingServiceWithCRUD<IQuoteService, IQuoteRepository, Quote, QuoteDto>(
        mapper, logger, messageProviderService, repository), IQuoteService
{
    public async Task<QuoteDto?> GetByCodeAsync(string code)
    {
        var entity = await Repository.GetSingleAsync(p => p.Code == code);
        return Mapper.Map<QuoteDto>(entity);
    }
}