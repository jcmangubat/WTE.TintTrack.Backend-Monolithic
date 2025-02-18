using Microsoft.AspNetCore.Http;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

namespace WTE.TintTrack.Core.Application.Interfaces;

public interface ITenantService : IMappedLoggingService<ITenantService>
{
    Task<TenantDto?> RegisterTenantAsync(TenantDto createModel);
    Task<bool> DeleteAsync(string tenantCode);
    Task<IEnumerable<TenantDto>?> GetAllAsync();
    Task<TenantDto?> GetAsync(Guid id);
    Task UpdateAsync(string tenantCode, TenantDto updateModel);
    Task<TenantDto> GetTenantByCodeAsync(string tenantCode);
    Task<TenantDto?> ResolveTenantAsync(HttpContext context);
    Task<IEnumerable<TenantDto>?> GetTenantsOwnedByUserAsync(string userCode);
    Task<IEnumerable<TenantDto>?> GetTenantsByUserEmailAsync(string emailAddress);
    Task<bool> ValidateTenantAsync(string tenantCode);
    Task<string> UploadLogoImage(string tenantCode, IFormFile logoImageFormFile);
    Task ApproveTenantAsync(string tenantCode, bool force = false);
}
