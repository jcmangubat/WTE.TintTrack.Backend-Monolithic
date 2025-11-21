using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;

public class InvoiceAdjustment : GuidKeyedAuditableEntity
{
    // The type of adjustment (e.g., Discount, Price Correction, etc.)
    public AdjustmentTypesEnum AdjustmentType { get; set; }

    // A description or reason for the adjustment
    public string Description { get; set; }

    // The amount of the adjustment
    public decimal AdjustmentAmount { get; set; }
    
    // The date when the adjustment was applied
    public DateTime AdjustmentDate { get; set; }

    
    // Foreign key to the Invoice this adjustment belongs to
    public Guid InvoiceId { get; set; }
    public virtual Invoice Invoice { get; set; }
}
