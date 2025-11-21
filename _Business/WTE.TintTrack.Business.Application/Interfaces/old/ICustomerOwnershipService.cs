using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Application.Interfaces;

public interface ICustomerOwnershipService : IMappedLoggingServiceWithCRUD<ICustomerOwnershipService, ICustomerOwnershipRepository, CustomerOwnership, CustomerOwnershipDto> { }
