using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using static WTE.TintTrack.Common.Constants.Consts;
using WTE.TintTrack.Application.Shared.Validator.Attributes;

namespace WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

public class TenantSubscriptionPaymentDto : GuidKeyedAuditableModel
{
    [Required]
    public required decimal Amount { get; set; }

    [Required]
    public required DateTime PaymentDate { get; set; }

    [Required]
    public required PaymentStatusEnum PaymentStatus { get; set; }

    [Required]
    public required string InvoiceNo { get; set; }
}
