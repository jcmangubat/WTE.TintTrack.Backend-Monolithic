using AutoMapper;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Application.Services;

public class CustomerService(
    IMapper mapper,
    ILogger<CustomerService> logger,
    IMessageProviderService messageProviderService,
    ICustomerRepository repository)
    : MappedLoggingServiceWithCRUD<ICustomerService, ICustomerRepository, Customer, CustomerDto>(
        mapper, logger, messageProviderService, repository), ICustomerService
{
    public async Task<CustomerDto?> GetByCodeAsync(string code)
    {
        var entity = await Repository.GetSingleAsync(p => p.Code == code);
        return Mapper.Map<CustomerDto>(entity);
    }
}
