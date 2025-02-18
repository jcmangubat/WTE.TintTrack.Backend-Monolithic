using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Application.EntityRecordValidators;

public class InvoiceRecordValidator(IInvoiceRepository invoiceRepository)
    : IEntityRecordValidator<InvoiceDto>
{
    private readonly IInvoiceRepository _InvoiceRepository = invoiceRepository;

    public async Task<bool> ExistAsync(InvoiceDto entity) =>
        await _InvoiceRepository.AnyAsync(p => p.Code == entity.Code);

   /* public async Task<InvoiceDto> TransformAsync(InvoiceDto entityDto)
    {
        return entityDto;
        //throw new NotImplementedException();
    }*/

    public InvoiceDto ValidateRecordForInsert(InvoiceDto entityDto)
    {
        if (entityDto.Id == Guid.Empty)
            entityDto.Id = Guid.NewGuid();

        /*if (string.IsNullOrEmpty(entityDto.Code))
            entityDto.Code = CodeGenerator.GenerateUniqueCode($"{entityDto.Email}{entityDto.FirstName}{entityDto.LastName}", FieldLengths.General.CODE);*/

        return entityDto;
    }
}
