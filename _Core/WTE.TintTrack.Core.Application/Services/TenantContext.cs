using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Common.Interfaces;
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Core.Application.Interfaces;

namespace WTE.TintTrack.Core.Application.Services;

/// <summary>
/// Scoped tenant context service that resolves tenant information per HTTP request
/// </summary>
public class TenantContext : ITenantContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITokenService _tokenService;
    private readonly ITenantService _tenantService;
    private readonly IMessageProviderService _messageProviderService;
    private readonly ApplicationSettings _appSettings;
    private readonly ILogger<TenantContext> _logger;

    private string? _tenantCode;
    private Guid? _tenantId;
    private string? _tenantConnectionString;
    private bool _isResolved;

    public TenantContext(
        IHttpContextAccessor httpContextAccessor,
        ITokenService tokenService,
        ITenantService tenantService,
        IMessageProviderService messageProviderService,
        IOptions<ApplicationSettings> appSettings,
        ILogger<TenantContext> logger)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _tenantService = tenantService ?? throw new ArgumentNullException(nameof(tenantService));
        _messageProviderService = messageProviderService ?? throw new ArgumentNullException(nameof(messageProviderService));
        _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public string? TenantCode => _tenantCode;
    public Guid? TenantId => _tenantId;
    public string? TenantConnectionString => _tenantConnectionString;
    public bool IsResolved => _isResolved;

    public async Task ResolveAsync()
    {
        if (_isResolved)
            return;

        try
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                _logger.LogWarning("HttpContext is null, cannot resolve tenant context");
                return;
            }

            // Try to get tenant code from JWT token
            var accessToken = httpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(accessToken) && accessToken.StartsWith("Bearer "))
            {
                var token = accessToken["Bearer ".Length..].Trim();
                var tokenDetails = await _tokenService.GetDetailsFromAccessTokenAsync(token);
                
                if (tokenDetails.Value.Tenant != null)
                {
                    _tenantCode = tokenDetails.Value.Tenant.TenantCode;
                    _tenantId = tokenDetails.Value.Tenant.Id;
                    
                    // Build connection string
                    if (!string.IsNullOrEmpty(_appSettings.TenantConnStrTemplate) && !string.IsNullOrEmpty(_tenantCode))
                    {
                        _tenantConnectionString = _appSettings.TenantConnStrTemplate.Replace("{TENANTCODE}", _tenantCode);
                    }

                    _isResolved = true;
                    _logger.LogDebug("Tenant context resolved: TenantCode={TenantCode}, TenantId={TenantId}", _tenantCode, _tenantId);
                    return;
                }
            }

            // Try to get tenant code from query string or header (fallback)
            _tenantCode = httpContext.Request.Query["tenantCode"].FirstOrDefault() 
                       ?? httpContext.Request.Headers["X-Tenant-Code"].FirstOrDefault();

            if (!string.IsNullOrEmpty(_tenantCode))
            {
                try
                {
                    var tenantDto = await _tenantService.GetTenantByCodeAsync(_tenantCode);
                    _tenantId = tenantDto.Id;
                    if (!string.IsNullOrEmpty(_appSettings.TenantConnStrTemplate))
                    {
                        _tenantConnectionString = _appSettings.TenantConnStrTemplate.Replace("{TENANTCODE}", _tenantCode);
                    }
                    _isResolved = true;
                    _logger.LogDebug("Tenant context resolved from header/query: TenantCode={TenantCode}", _tenantCode);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to resolve tenant from code: {TenantCode}", _tenantCode);
                    // Don't throw, allow request to continue (may be a public endpoint)
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error resolving tenant context");
            throw;
        }
    }

    public void EnsureResolved()
    {
        if (!_isResolved || string.IsNullOrEmpty(_tenantCode))
        {
            var apiMsg = _messageProviderService.GetMessage("ERR059");
            throw new InvalidOperationException(apiMsg.Message ?? "Tenant context has not been resolved.");
        }
    }
}

