using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Domain.Entities;

public class Permission : GuidKeyedAuditableEntity
{
    public required FeaturesEnum Feature { get; set; }
    public required FeatureAccessPermissionsEnum PermissionLevel { get; set; }
    public required string Name { get; set; } = default!;
    public required string Description { get; set; } = default!;
    public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
    
}
