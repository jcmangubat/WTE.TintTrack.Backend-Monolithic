using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;

public class TintMaterialPriceHistory : GuidKeyedAuditableEntity
{
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public DateTime ChangedOn { get; set; } = DateTime.UtcNow;

    public required Guid TintMaterialId { get; set; }
    public virtual TintMaterial TintMaterial { get; set; }
}