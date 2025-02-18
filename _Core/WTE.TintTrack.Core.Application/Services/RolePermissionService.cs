using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMEAppHouse.Core.CodeKits.Helpers;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Application.Services;

public class RolePermissionService(IMapper mapper,
                    ILogger<RolePermissionService> logger,
                    IMessageProviderService messageProviderService,
                    RoleManager<ApplicationRole> roleManager,
                    IPermissionRepository permissionRepository,
                    IRolePermissionRepository rolePermissionRepository)
    : MappedLoggingService<IRolePermissionService>(mapper, logger, messageProviderService), IRolePermissionService
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly IPermissionRepository _permissionRepository = permissionRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;

    /// <inheritdoc />
    public async Task<IEnumerable<string>> GetPermissionsForRolesAsync(IEnumerable<string> roles)
    {
        try
        {
            var rolePermissions = await _rolePermissionRepository.GetListAsync(
                                                        p => roles.Contains(p.Role.Name),
                                                        p => p.Include(x => x.Role).Include(x => x.Permission));

            return rolePermissions.Select(p => p.Permission.Name).ToList();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<string>> GetRolesForPermissionAsync(string permission)
    {
        try
        {
            var rolePermissions = await _rolePermissionRepository.GetListAsync(
                                                        p => p.Permission.Name == permission,
                                                        p => p.Include(x => x.Role).Include(x => x.Permission));

            return rolePermissions.Select(p => p.Role.Name ?? string.Empty)
                                            .Where(s => !string.IsNullOrEmpty(s))
                                            .Distinct()
                                            .Order();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task<bool> HasPermissionAsync(IEnumerable<string> roles, string permission)
    {
        try
        {
            var rolePermission = await _rolePermissionRepository.GetSingleAsync(
                                                rp => roles.Contains(rp.Role.Name) && rp.Permission.Name == permission,
                                                rp => rp.Include(x => x.Permission).Include(x => x.Role));
            return rolePermission != null;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task UpdatePermissionsAsync(FeaturesEnum feature, FeatureAccessPermissionsEnum permissionLevel, IEnumerable<string> roles)
    {
        try
        {
            var permissionEntity = await _permissionRepository.GetSingleAsync(p => p.Feature == feature && p.PermissionLevel == permissionLevel);

            if (permissionEntity == null)
            {
                var apiMsg = MessageProviderService.GetMessage("ERR048");
                permissionEntity = new Permission
                {
                    Feature = feature,
                    PermissionLevel = permissionLevel,
                    Name = $"{feature}.{permissionLevel}",
                    Description = apiMsg.Message
                                            .Replace("{{permissionLevel}}", permissionLevel.ToString().Replace("Can", string.Empty).ToLower())
                                            .Replace("{{feature}}", feature.ToString())
                };
                await _permissionRepository.AddAsync(permissionEntity);
                await _permissionRepository.CommitAsync();
            }

            var currentRoles = await _rolePermissionRepository.GetListAsync(p => p.PermissionId == permissionEntity.Id);
            await _rolePermissionRepository.DeleteAsync(currentRoles);

            var existingRoles = _roleManager.Roles.ToList() ?? [];
            var newRolePermissions = new List<RolePermission>();
            foreach (var roleName in roles)
            {
                var role = existingRoles.FirstOrDefault(r => r.Name == roleName);
                if (role == null) continue;
                newRolePermissions.Add(new RolePermission
                {
                    PermissionId = permissionEntity.Id,
                    RoleId = role.Id
                });
            }

            await _rolePermissionRepository.AddAsync(newRolePermissions);
            await _rolePermissionRepository.CommitAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }
}