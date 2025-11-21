using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Domain.Shared;

namespace WTE.TintTrack.Business.Application.DTOs;

public class InvoiceDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }

    public required string InvoiceNumber { get; set; }
    public required DateTime InvoiceDate { get; set; }
    public required decimal TotalAmount { get; set; }
    public decimal? AmountPaid { get; set; }
    public DateTime? PaymentDate { get; set; }

    // Foreign Key: An Invoice is linked to a specific Project
    public required Guid ProjectId { get; set; }
    public virtual Project Project { get; set; }
}