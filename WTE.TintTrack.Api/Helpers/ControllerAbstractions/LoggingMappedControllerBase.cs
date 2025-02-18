using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging.Interface;
using WTE.TintTrack.Common.Exceptions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Helpers.ControllerAbstractions;

public class LoggingMappedControllerBase<TController>(ILogger<TController> logger,
                                                        IMapper mapper,
                                                        IMessageProviderService messageProviderService) : ControllerBase
{
    protected readonly IMessageProviderService MessageProviderService = messageProviderService
        ?? throw new ArgumentNullException(nameof(messageProviderService));

    protected readonly ILogger<TController> Logger = logger
        ?? throw new ArgumentNullException(nameof(logger));

    protected IMapper Mapper { get; } = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    protected IActionResult CreateApiResponse(IApiResponse response) =>
        new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };

    /// <summary>
    /// Get the authenticated user's email from the token claims
    /// </summary>
    /// <returns></returns>
    protected (string? RefreshToken,
                string? Email,
                string? UserCode,
                string? TenantCode,
                string? SubscriptionPlanCode,
                List<UserRolesEnum> Roles) GetUserClaimsInfo()
    {
        var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var userCode = User.Claims.FirstOrDefault(c => c.Type == "user_code")?.Value;
        var tenantCode = User.Claims.FirstOrDefault(c => c.Type == "tenant_code")?.Value;
        var planCode = User.Claims.FirstOrDefault(c => c.Type == "plan_code")?.Value;
        var refreshToken = User.Claims.FirstOrDefault(c => c.Type == "refreshtoken")?.Value;
        var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role)
                                    .Select(p => Enum.Parse<UserRolesEnum>(p.Value))
                                    .ToList() ?? [];
        return (refreshToken, userEmail, userCode, tenantCode, planCode, roles);
    }

    protected List<UserRolesEnum> CurrentUserRoles =>
        User.Claims.Where(c => c.Type == ClaimTypes.Role)
                                    .Select(p => Enum.Parse<UserRolesEnum>(p.Value))
                                    .ToList() ?? [];

    protected bool UserRoleIsInternal() =>
            User.IsInRole(UserRolesEnum.GlobalAdmin.ToString()) ||
            User.IsInRole(UserRolesEnum.GlobalTechSupport.ToString()) ||
            User.IsInRole(UserRolesEnum.GlobalAccountMgr.ToString()) ||
            User.IsInRole(UserRolesEnum.GlobalViewer.ToString());

    protected bool UserRoleIsBelowTenantAdminRole() =>
            !UserRoleIsInternal() &&
            !CurrentUserRoles.Any(cur => cur == UserRolesEnum.TenantOwner || cur == UserRolesEnum.TenantSystemAdmin);

    protected void ValidateTenantCode(string tenantCode)
    {
        if (string.IsNullOrEmpty(tenantCode))
        {
            var apiMsg = MessageProviderService.GetMessage("ERR037");
            throw new CustomValidationException(apiMsg.Message);
        }
    }

    protected void ValidateUserCode(string userCode)
    {
        if (string.IsNullOrEmpty(userCode))
        {
            var apiMsg = MessageProviderService.GetMessage("ERR038");
            throw new CustomValidationException(apiMsg.Message);
        }
    }
}
