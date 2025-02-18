using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Core.Domain.Entities;

public class UserTenant : GuidKeyedAuditableEntity
{
    public required Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

    public required Guid TenantId { get; set; }
    public virtual Tenant Tenant { get; set; }

    public bool? IsDefault {  get; set; }
    public bool? UserIsOwner { get; set; }

    /// <summary>
    /// This user while in associated with a tenant, may have serveral roles assigned.
    /// </summary>
    public virtual ICollection<UserTenantRole> UserTenantRoles { get; set; } = new HashSet<UserTenantRole>();
}
