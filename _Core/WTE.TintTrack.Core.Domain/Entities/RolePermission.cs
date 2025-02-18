using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Core.Domain.Entities;

public class RolePermission : GuidKeyedAuditableEntity
{
    public Guid RoleId { get; set; }
    public virtual ApplicationRole Role { get; set; } = default!;

    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; } = default!;
}