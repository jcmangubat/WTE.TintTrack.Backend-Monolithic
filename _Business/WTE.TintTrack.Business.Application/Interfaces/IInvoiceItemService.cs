using WTE.TintTrack.Application.Shared.ServiceAbstractions;

namespace WTE.TintTrack.Business.Application.Interfaces;

public interface IInvoiceItemService : IMappedLoggingServiceWithCRUD<IInvoiceItemService, IInvoiceItemRepository, InvoiceItem, InvoiceItemDto>
{
    //Task<EstimateDto?> GetByCodeAsync(string code);
}
