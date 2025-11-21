using WTE.TintTrack.Application.Shared.ServiceAbstractions;

namespace WTE.TintTrack.Business.Application.Interfaces;

public interface IWorkOrderService : IMappedLoggingServiceWithCRUD<IWorkOrderService, IWorkOrderRepository, WorkOrder, WorkOrderDto>
{
    Task<WorkOrderDto?> GetByCodeAsync(string code);
}
