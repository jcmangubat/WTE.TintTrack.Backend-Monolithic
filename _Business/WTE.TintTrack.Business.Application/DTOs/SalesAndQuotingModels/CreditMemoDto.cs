using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.SalesAndQuotingModels;

public class CreditMemoDto : GuidKeyedAuditableModel
{
    // The credit memo number
    public string MemoNumber { get; set; }

    // The amount of credit issued in this memo
    public decimal CreditAmount { get; set; }

    // A description for why the credit was issued
    public string Description { get; set; }

    // Date when the credit memo was issued
    public DateTime IssuedDate { get; set; }

    // The status of the credit memo (e.g., Applied, Pending)
    public CreditMemoStatusEnum Status { get; set; }


    public Guid InvoiceId { get; set; }
    public virtual InvoiceDto Invoice { get; set; }
}
