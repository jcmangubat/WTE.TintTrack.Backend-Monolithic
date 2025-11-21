using WTE.TintTrack.Application.Shared.ServiceAbstractions;

namespace WTE.TintTrack.Business.Application.Interfaces;

public interface IWorkOrderItemService : IMappedLoggingServiceWithCRUD<IWorkOrderItemService, IWorkOrderItemRepository, WorkOrderItem, WorkOrderItemDto>
{
    //Task<EstimateDto?> GetByCodeAsync(string code);
}
