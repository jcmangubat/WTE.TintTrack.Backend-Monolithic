using AutoMapper;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Application.Services;

public class ContactService(
    IMapper mapper,
    ILogger<ContactService> logger,
    IMessageProviderService messageProviderService,
    IContactRepository repository)
    : MappedLoggingServiceWithCRUD<IContactService, IContactRepository, Contact, ContactDto>(mapper, logger, messageProviderService, repository),
                            IContactService
{
    public async Task<ContactDto?> GetByCodeAsync(string code)
    {
        var entity = await Repository.GetSingleAsync(p => p.Code == code);
        return Mapper.Map<ContactDto>(entity);
    }
}
