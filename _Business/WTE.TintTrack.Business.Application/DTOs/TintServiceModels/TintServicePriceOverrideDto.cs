using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;

namespace WTE.TintTrack.Business.Application.DTOs.TintServiceModels;

public class TintServicePriceOverrideDto : GuidKeyedAuditableModel
{
    public decimal CustomPrice { get; set; } // Override FinalPrice for this customer

    public required Guid TintTintServicePriceScheduleId { get; set; }
    public virtual TintServicePriceScheduleDto TintTintServicePriceSchedule { get; set; }


    public required Guid CustomerId { get; set; }
    public virtual CustomerDto Customer { get; set; }
}
