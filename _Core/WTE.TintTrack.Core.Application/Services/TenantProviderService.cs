using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Core.Application.Interfaces;

namespace WTE.TintTrack.Core.Application.Services;

public class TenantProviderService(IHttpContextAccessor httpContextAccessor,
    ITokenService tokenService,
    IMessageProviderService messageProviderService,
    IOptions<ApplicationSettings> appSettings) : ITenantProviderService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IMessageProviderService _messageProviderService = messageProviderService;
    private readonly ApplicationSettings _appSettings = appSettings.Value;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<string?> GetTenantCodeAsync()
    {
        //var userCode = User.Claims.FirstOrDefault(c => c.Type == "user_code")?.Value;
        var accessToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
        if (accessToken == null || string.IsNullOrEmpty(accessToken.Replace("Bearer ", string.Empty)))
            return null;

        var accessTokenResult = await _tokenService.GetDetailsFromAccessTokenAsync(accessToken.Replace("Bearer ", string.Empty));
        if (accessTokenResult.Value.Tenant == null)
            throw messageProviderService.ServiceOperationException("ERR059");

        return accessTokenResult.Value.Tenant.TenantCode;
    }

    public async Task<string?> GetTenantSQLDbConnectionAsync()
    {
        if (string.IsNullOrEmpty(_appSettings.TenantConnStrTemplate))
            return string.Empty;

        var tenantCode = await GetTenantCodeAsync();
        if (string.IsNullOrEmpty(tenantCode))
            return string.Empty;

        return _appSettings.TenantConnStrTemplate.Replace("{TENANTCODE}", tenantCode);
    }
}