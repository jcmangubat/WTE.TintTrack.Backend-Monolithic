using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Application.DTOs.old.SalesAndQuoting;

public class LateFeeDto : GuidKeyedAuditableEntity
{
    // The late fee amount charged
    public decimal LateFeeAmount { get; set; }

    // The date the late fee was applied
    public DateTime AppliedDate { get; set; }

    // A description of the late fee (e.g., "Late fee for overdue payment")
    public string Description { get; set; }

    // The due date of the original payment
    public DateTime OriginalDueDate { get; set; }


    public Guid InvoiceId { get; set; }
    public virtual InvoiceDto Invoice { get; set; }
}