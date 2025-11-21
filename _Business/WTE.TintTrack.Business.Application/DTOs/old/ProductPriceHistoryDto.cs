using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;

namespace WTE.TintTrack.Business.Application.DTOs.old;

public class ProductPriceHistoryDto : GuidKeyedAuditableModel
{
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public DateTime ChangedOn { get; set; } = DateTime.UtcNow;

    public required Guid ProductId { get; set; }
    public virtual ProductDto Product { get; set; }
}