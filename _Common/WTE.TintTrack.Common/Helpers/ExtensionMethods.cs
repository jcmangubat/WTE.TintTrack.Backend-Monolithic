using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Common.Helpers;

public static class ExtensionMethods
{
    public static bool IsRoleInternal(this UserRolesEnum userRole) =>
        InternalRoles.Any(p => p == userRole);
}
