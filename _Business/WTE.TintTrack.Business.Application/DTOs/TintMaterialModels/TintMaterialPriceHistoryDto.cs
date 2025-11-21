using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;

namespace WTE.TintTrack.Business.Application.DTOs.TintMaterialModels;

public class TintMaterialPriceHistoryDto : GuidKeyedAuditableModel
{
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public DateTime ChangedOn { get; set; } = DateTime.UtcNow;

    public required Guid TintMaterialId { get; set; }
    public virtual TintMaterialDto TintMaterial { get; set; }
}