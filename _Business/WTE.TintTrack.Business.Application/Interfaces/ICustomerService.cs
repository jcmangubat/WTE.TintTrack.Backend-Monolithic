using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Application.Interfaces;

public interface ICustomerService : IMappedLoggingServiceWithCRUD<ICustomerService, ICustomerRepository, Customer, CustomerDto>
{
    Task<CustomerDto?> GetByCodeAsync(string code);
}
