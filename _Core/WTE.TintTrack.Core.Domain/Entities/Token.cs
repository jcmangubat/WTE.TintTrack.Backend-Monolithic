using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Core.Domain.Entities;

public class Token : GuidKeyedAuditableEntity
{
    public required string RefreshToken { get; set; }
    public required DateTime RefreshTokenExpiration { get; set; }
    
    public required Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

    public Guid? TenantId { get; set; }
    public virtual Tenant Tenant { get; set; }
}