using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;

namespace WTE.TintTrack.Business.Application.DTOs.TintMaterialModels;

public class TintMaterialPriceOverrideDto : GuidKeyedAuditableModel
{
    public decimal CustomPrice { get; set; } // Override FinalPrice for this customer

    public required Guid TintMaterialPriceScheduleId { get; set; }
    public virtual TintMaterialPriceScheduleDto TintMaterialPriceSchedule { get; set; }


    public required Guid CustomerId { get; set; }
    public virtual CustomerDto Customer { get; set; }
}
