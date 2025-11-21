using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;

namespace WTE.TintTrack.Business.Application.DTOs.old;

public class ProductPriceOverrideDto : GuidKeyedAuditableModel
{
    public required Guid CustomerId { get; set; }
    public decimal CustomPrice { get; set; } // Override FinalPrice for this customer

    public required Guid ProductPriceScheduleId { get; set; }
    public virtual ProductPriceScheduleDto ProductPriceSchedule { get; set; }
}
