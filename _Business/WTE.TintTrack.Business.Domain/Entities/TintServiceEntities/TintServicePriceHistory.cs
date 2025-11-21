using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Domain.Entities.TintServiceEntities;

public class TintServicePriceHistory : GuidKeyedAuditableEntity
{
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public DateTime ChangedOn { get; set; } = DateTime.UtcNow;

    public required Guid TintMaterialId { get; set; }
    public virtual TintService TintService { get; set; }
}