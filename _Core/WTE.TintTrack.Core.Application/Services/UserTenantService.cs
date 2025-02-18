using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SMEAppHouse.Core.CodeKits.Helpers;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Common.Exceptions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Application.Services;

public class UserTenantService(IMapper mapper,
                    ILogger<UserTenantService> logger,
                    IMessageProviderService messageProviderService,
                    RoleManager<ApplicationRole> roleManager,
                    IUserRepository userRepository,
                    ITenantRepository tenantRepository,
                    IUserTenantRepository userTenantRepository)
    : MappedLoggingService<IUserTenantService>(mapper, logger, messageProviderService), IUserTenantService
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITenantRepository _tenantRepository = tenantRepository;
    private readonly IUserTenantRepository _userTenantRepository = userTenantRepository;

    /// <inheritdoc />
    public async Task<UserTenantDto?> GetByUserAndTenantAsync(string userCode, string tenantCode, bool includeUserTenantRoles = false)
    {
        try
        {
            var user = await _userRepository.GetByUserCodeAsync(userCode)
                            ?? throw RecordNotFoundException("ERR064");

            var tenant = await _tenantRepository.GetByTenantCodeAsync(tenantCode)
                            ?? throw RecordNotFoundException("ERR074");

            var userTenant = await _userTenantRepository.GetByUserAndTenantAsync(user.Id, tenant.Id, includeUserTenantRoles);
            var userTenantDto = Mapper.Map<UserTenantDto>(userTenant);
            return userTenantDto;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<UserTenantDto>> GetTenantsForUserAsync(string userCode)
    {
        try
        {
            var user = await _userRepository.GetByUserCodeAsync(userCode)
                            ?? throw RecordNotFoundException("ERR064");

            var userTenants = await _userTenantRepository.GetTenantsForUserAsync(user.Id);
            var userTenantsDto = Mapper.Map<List<UserTenantDto>>(userTenants);
            return userTenantsDto;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<UserTenantDto>> GetUsersForTenantAsync(string tenantCode)
    {
        try
        {
            var tenant = await _tenantRepository.GetByTenantCodeAsync(tenantCode)
                            ?? throw RecordNotFoundException("ERR008");

            var userTenants = await _userTenantRepository.GetUsersForTenantAsync(tenant.Id);
            var userTenantsDto = Mapper.Map<List<UserTenantDto>>(userTenants);
            return userTenantsDto;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task<bool> IsUserInTenantAsync(string userCode, string tenantCode)
    {
        try
        {
            var user = await _userRepository.GetByUserCodeAsync(userCode)
                            ?? throw RecordNotFoundException("ERR064");

            var tenant = await _tenantRepository.GetByTenantCodeAsync(tenantCode)
                            ?? throw RecordNotFoundException("ERR008");

            return await _userTenantRepository.IsUserInTenantAsync(user.Id, tenant.Id);
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task AddUserToTenantAsync(UserTenantDto userTenantDto)
    {
        try
        {
            var userTenant = Mapper.Map<UserTenant>(userTenantDto);
            await _userTenantRepository.AddUserToTenantAsync(userTenant);
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task<bool> RemoveUserFromTenantAsync(string userCode, string tenantCode)
    {
        try
        {
            var user = await _userRepository.GetByUserCodeAsync(userCode)
                            ?? throw RecordNotFoundException("ERR064");

            var tenant = await _tenantRepository.GetByTenantCodeAsync(tenantCode)
                            ?? throw RecordNotFoundException("ERR008");

            var userTenant = await _userTenantRepository.GetByUserAndTenantAsync(user.Id, tenant.Id) ??
                             throw CustomInvalidOperationException("ERR075");

            await _userTenantRepository.RemoveUserFromTenantAsync(user.Id, tenant.Id);

            return true;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task<bool> UpdateUserTenantAsync(UserTenantDto userTenantDto)
    {
        try
        {
            var existingUserTenant = await _userTenantRepository.GetByUserAndTenantAsync(userTenantDto.UserId, userTenantDto.TenantId) ??
                                     throw CustomKeyNotFoundException("ERR076");

            var userTenant = Mapper.Map<UserTenant>(userTenantDto);

            await _userTenantRepository.UpdateUserTenantAsync(userTenant);

            return true;
        }
        catch (KeyNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<UserTenantRoleDto>> GetUserRolesInTenantAsync(string userCode, string tenantCode)
    {
        try
        {
            var user = await _userRepository.GetByUserCodeAsync(userCode)
                            ?? throw RecordNotFoundException("ERR064");

            var tenant = await _tenantRepository.GetByTenantCodeAsync(tenantCode)
                            ?? throw RecordNotFoundException("ERR008");

            var userTenantRoles = await _userTenantRepository.GetUserRolesInTenantAsync(user.Id, tenant.Id);

            var userTenantRolesDto = Mapper.Map<IEnumerable<UserTenantRoleDto>>(userTenantRoles);

            return userTenantRolesDto;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task AssignRolesToUserInTenantAsync(string userCode, string tenantCode, string[] userRoles)
    {
        try
        {
            var user = await _userRepository.GetByUserCodeAsync(userCode)
                            ?? throw RecordNotFoundException("ERR064");

            var tenant = await _tenantRepository.GetByTenantCodeAsync(tenantCode)
                            ?? throw RecordNotFoundException("ERR008");

            var userTenant = await _userTenantRepository.GetByUserAndTenantAsync(user.Id, tenant.Id)
                            ?? throw CustomInvalidOperationException("ERR077");

            var errorRoles = new List<string>();
            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole.ToString());
                if (role == null)
                    errorRoles.Add(userRole.ToString());
                else
                    await _userTenantRepository.AssignRoleToUserInTenantAsync(user.Id, tenant.Id, role.Id);
            }

            if (errorRoles.Count > 0)
                throw CustomInvalidOperationException("ERR078", new() { { "{{errorRoles}}", string.Join(", ", errorRoles) } });

            await _userTenantRepository.CommitAsync();

        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (InvalidOperationException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task<bool> AssignRoleToUserInTenantAsync(string userCode, string tenantCode, Consts.UserRolesEnum userRole)
    {
        try
        {
            var user = await _userRepository.GetByUserCodeAsync(userCode)
                            ?? throw RecordNotFoundException("ERR064");

            var tenant = await _tenantRepository.GetByTenantCodeAsync(tenantCode)
                            ?? throw RecordNotFoundException("ERR008");

            var userTenant = await _userTenantRepository.GetByUserAndTenantAsync(user.Id, tenant.Id)
                            ?? throw CustomInvalidOperationException("ERR077");

            var role = await _roleManager.FindByNameAsync(userRole.ToString())
                            ?? throw CustomInvalidOperationException("ERR079");

            await _userTenantRepository.AssignRoleToUserInTenantAsync(user.Id, tenant.Id, role.Id);
            await _userTenantRepository.CommitAsync();

            return true;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (InvalidOperationException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task<bool> RemoveRoleFromUserInTenantAsync(string userCode, string tenantCode, string roleName)
    {
        if (!Enum.TryParse<Consts.UserRolesEnum>(roleName, out var userRole))
            throw new InvalidOperationException($"No role found associated with the name provided.");

        return await RemoveRoleFromUserInTenantAsync(userCode, tenantCode, userRole);
    }

    /// <inheritdoc />
    public async Task<bool> RemoveRoleFromUserInTenantAsync(string userCode, string tenantCode, Consts.UserRolesEnum userRole)
    {
        try
        {
            var user = await _userRepository.GetByUserCodeAsync(userCode)
                            ?? throw RecordNotFoundException("ERR064");

            var tenant = await _tenantRepository.GetByTenantCodeAsync(tenantCode)
                            ?? throw RecordNotFoundException("ERR074");

            var roles = await _userTenantRepository.GetUserRolesInTenantAsync(user.Id, tenant.Id) ??
                        throw CustomInvalidOperationException("ERR090");

            var role = await _roleManager.FindByNameAsync(userRole.ToString())
                            ?? throw CustomInvalidOperationException("ERR079");

            await _userTenantRepository.RemoveRoleFromUserInTenantAsync(user.Id, tenant.Id, role.Id);

            return true;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (InvalidOperationException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task<bool> RemoveRolesToUserInTenantAsync(string userCode, string tenantCode, string[] userRoles)
    {
        try
        {
            var user = await _userRepository.GetByUserCodeAsync(userCode)
                            ?? throw RecordNotFoundException("ERR064");

            var tenant = await _tenantRepository.GetByTenantCodeAsync(tenantCode)
                            ?? throw RecordNotFoundException("ERR008");

            var userTenant = await _userTenantRepository.GetByUserAndTenantAsync(user.Id, tenant.Id)
                            ?? throw CustomInvalidOperationException("ERR077");

            var errorRoles = new List<string>();
            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole.ToString());
                if (role == null)
                    errorRoles.Add(userRole.ToString());
                else
                    await _userTenantRepository.RemoveRoleFromUserInTenantAsync(user.Id, tenant.Id, role.Id);
            }

            if (errorRoles.Count > 0)
                throw CustomInvalidOperationException("ERR078", new() { { "{{errorRoles}}", string.Join(", ", errorRoles) } });

            await _userTenantRepository.CommitAsync();
            return true;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (InvalidOperationException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task<bool> AssignRoleToUserInTenantAsync(string userCode, string tenantCode, string userRole)
    {
        try
        {
            var user = await _userRepository.GetByUserCodeAsync(userCode)
                            ?? throw RecordNotFoundException("ERR064");

            var tenant = await _tenantRepository.GetByTenantCodeAsync(tenantCode)
                            ?? throw RecordNotFoundException("ERR008");

            var userTenant = await _userTenantRepository.GetByUserAndTenantAsync(user.Id, tenant.Id)
                            ?? throw CustomInvalidOperationException("ERR077");

            var role = await _roleManager.FindByNameAsync(userRole)
                            ?? throw CustomInvalidOperationException("ERR079");

            await _userTenantRepository.AssignRoleToUserInTenantAsync(user.Id, tenant.Id, role.Id);
            await _userTenantRepository.CommitAsync();

            return true;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (InvalidOperationException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }
}
