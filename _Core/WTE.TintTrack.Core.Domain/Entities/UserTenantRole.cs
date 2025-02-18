using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Core.Domain.Entities;

/// <summary>
/// This user while in associated with a tenant, may have serveral roles assigned.
/// </summary>
public class UserTenantRole : GuidKeyedAuditableEntity
{
    public required Guid UserTenantId { get; set; }
    public virtual UserTenant UserTenant { get; set; }

    public required Guid RoleId { get; set; }
    public virtual ApplicationRole Role { get; set; }
}
