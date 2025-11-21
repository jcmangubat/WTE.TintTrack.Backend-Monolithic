using AutoMapper;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Application.Services;

public class InvoiceService(
    IMapper mapper,
    ILogger<InvoiceService> logger,
    IMessageProviderService messageProviderService,
    IInvoiceRepository repository)
    : MappedLoggingServiceWithCRUD<IInvoiceService, IInvoiceRepository, Invoice, InvoiceDto>(
        mapper, logger, messageProviderService, repository), IInvoiceService
{
    public async Task<InvoiceDto?> GetByCodeAsync(string code)
    {
        var entity = await Repository.GetSingleAsync(p => p.Code == code);
        return Mapper.Map<InvoiceDto>(entity);
    }
}
