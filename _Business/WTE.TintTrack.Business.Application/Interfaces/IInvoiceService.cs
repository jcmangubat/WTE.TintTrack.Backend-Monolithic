using WTE.TintTrack.Application.Shared.ServiceAbstractions;

namespace WTE.TintTrack.Business.Application.Interfaces;

public interface IInvoiceService : IMappedLoggingServiceWithCRUD<IInvoiceService, IInvoiceRepository, Invoice, InvoiceDto>
{
    Task<InvoiceDto?> GetByCodeAsync(string code);
}
