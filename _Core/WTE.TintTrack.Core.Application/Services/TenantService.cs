using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SMEAppHouse.Core.CodeKits.Helpers;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Common.Exceptions;
using WTE.TintTrack.Common.Interfaces;
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Application.Services;

public class TenantService(IMapper mapper, ILogger<TenantService> logger,
                        IMessageProviderService messageProviderService,
                        IOptions<ApplicationSettings> appSettings,
                        ITenantDatabaseCreator databaseCreator,

                        IUserRepository userRepository,
                        ITenantRepository tenantRepository,
                        ITenantSubscriptionRepository tenantSubscriptionRepository,

                        IImageKitUploadService imageKitUploadService)
    : MappedLoggingService<ITenantService>(mapper, logger, messageProviderService), ITenantService
{
    private readonly ApplicationSettings _appSettings = appSettings.Value;
    private readonly IImageKitUploadService _imageKitUploadService = imageKitUploadService;

    private readonly ITenantDatabaseCreator _databaseCreator = databaseCreator;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITenantRepository _tenantRepository = tenantRepository;
    private readonly ITenantSubscriptionRepository _tenantSubscriptionRepository = tenantSubscriptionRepository;

    public async Task<TenantDto?> RegisterTenantAsync(TenantDto createTenantDTO)
    {
        try
        {
            Tenant tenant = Mapper.Map<Tenant>(createTenantDTO);

            if (tenant == null)
                return null;

            // Create the tenant database
            await _databaseCreator.CreateDatabaseAsync(tenant.ConnectionString);

            await _tenantRepository.AddAsync(tenant);
            await _tenantRepository.CommitAsync();

            TenantDto TenantDto = Mapper.Map<TenantDto>(tenant);
            return TenantDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<bool> DeleteAsync(string tenantCode)
    {
        try
        {
            if (!await _tenantRepository.AnyAsync(p => p.TenantCode == tenantCode))
                throw RecordNotFoundException("ERR008");

            await _tenantRepository.DeleteAsync(p => p.TenantCode == tenantCode);
            await _tenantRepository.CommitAsync();

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

    public async Task<IEnumerable<TenantDto>?> GetAllAsync()
    {
        try
        {
            var tenants = await _tenantRepository.GetListAsync();
            var tenantDTOs = Mapper.Map<IEnumerable<TenantDto>>(tenants);
            return tenantDTOs;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<TenantDto?> GetAsync(Guid id)
    {
        try
        {
            var tenant = await _tenantRepository.GetByIdAsync(id);
            if (tenant == null)
                return null;

            var TenantDto = Mapper.Map<TenantDto>(tenant);
            return TenantDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task UpdateAsync(string tenantCode, TenantDto updateTenantDTO)
    {
        try
        {
            ValidateTenantCode(tenantCode);

            Tenant tenant = Mapper.Map<Tenant>(updateTenantDTO);

            if (tenant == null)
                return;

            await _tenantRepository.UpdateAsync(tenant);
            await _tenantRepository.CommitAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<TenantDto> GetTenantByCodeAsync(string tenantCode)
    {
        try
        {
            ValidateTenantCode(tenantCode);

            var tenant = await _tenantRepository.GetSingleAsync(tenant => tenant.TenantCode == tenantCode)
                          ?? throw RecordNotFoundException("ERR008");

            return Mapper.Map<TenantDto>(tenant);
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            // Log general exceptions and wrap them in a user-friendly exception
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task ApproveTenantAsync(string tenantCode, bool force = false)
    {
        try
        {
            ValidateTenantCode(tenantCode);

            var tenant = await _tenantRepository.GetSingleAsync(tenant => tenant.TenantCode == tenantCode)
                          ?? throw RecordNotFoundException("ERR008");

            if (tenant.TenantStatus == TenantStatusEnum.Active && !force)
                throw ServiceOperationException("ERR060");

            if (tenant != null)
            {
                var tenantSubscriptions = await _tenantSubscriptionRepository.GetByTenantAsync(tenant.TenantCode);
                if (tenantSubscriptions == null || !tenantSubscriptions.Any())
                    throw RecordNotFoundException("ERR011");

                var pendingTenantSubscription = tenantSubscriptions.FirstOrDefault(p => p.IsActive == true && p.SubscriptionStatus != SubscriptionStatusEnum.Active)
                    ?? throw RecordNotFoundException("ERR061");

                pendingTenantSubscription.SubscriptionStatus = SubscriptionStatusEnum.Active;
                await _tenantSubscriptionRepository.UpdateAsync(pendingTenantSubscription);
                await _tenantSubscriptionRepository.CommitAsync();

                tenant.TenantStatus = TenantStatusEnum.Active;
                await _tenantRepository.UpdateAsync(tenant);
                await _tenantRepository.CommitAsync();
            }
        }
        catch (ServiceOperationException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            // Log general exceptions and wrap them in a user-friendly exception
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }


    /// <summary>
    /// TODO: FIX THIS
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<TenantDto?> ResolveTenantAsync(HttpContext context)
    {
        try
        {
            var host = context.Request.Host.Host;

            // Regex pattern to match the subdomain part 
            var match = Regex.Match(host, @"^(?<tenant>[a-zA-Z0-9-]+)\.yourapp\.com$");
            if (!match.Success)
            {
                var apiMsg = MessageProviderService.GetMessage("ERR062");
                var errMsg = apiMsg.Message.Replace("{{host}}", host);
                Logger.LogWarning(errMsg);
                throw new ServiceOperationException(errMsg);
            }

            var tenantCode = match.Groups["tenant"].Value;

            // Retrieve the tenant from the database using the tenant code
            var tenant = await _tenantRepository.GetSingleAsync(tenant => tenant.TenantCode == tenantCode);

            if (tenant == null)
            {
                var apiMsg = MessageProviderService.GetMessage("ERR063");
                var errMsg = apiMsg.Message.Replace("{{tenantCode}}", tenantCode); 
                Logger.LogWarning(errMsg);
                throw new ServiceOperationException(errMsg);
            }

            var TenantDto = Mapper.Map<TenantDto>(tenant);

            return TenantDto;
        }
        catch (ServiceOperationException ex)
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

    public async Task<IEnumerable<TenantDto>?> GetTenantsOwnedByUserAsync(string userCode)
    {
        try
        {
            ValidateUserCode(userCode);

            var user = await _userRepository.GetByUserCodeAsync(userCode)
                                    ?? throw RecordNotFoundException("ERR064");

            var tenants = await _tenantRepository.GetTenantsForUserAsync(user.Id)
                                    ?? throw RecordNotFoundException("ERR065");

            var tenantDtos = Mapper.Map<List<TenantDto>>(tenants);
            return tenantDtos;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogWarning(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<IEnumerable<TenantDto>?> GetTenantsByUserEmailAsync(string emailAddress)
    {
        try
        {
            var tenants = await _tenantRepository.GetTenantsForUserEmailAddressAsync(emailAddress)
                                    ?? throw RecordNotFoundException("ERR066");

            var tenantDtos = Mapper.Map<List<TenantDto>>(tenants);
            return tenantDtos;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogWarning(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<bool> ValidateTenantAsync(string tenantCode)
    {
        try
        {
            return await _tenantRepository.AnyAsync(p => p.TenantCode == tenantCode);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<string> UploadLogoImage(string tenantCode, IFormFile logoImageFormFile)
    {
        try
        {
            var tenant = await _tenantRepository.GetByTenantCodeAsync(tenantCode)
                ?? throw RecordNotFoundException("ERR008");

            if (!string.IsNullOrEmpty(tenant.LogoImageUrl))
                await _imageKitUploadService.DeleteFileAsync(tenant.LogoImageUrl);

            var uploadFolderPath = _appSettings.ImgKitTenantLogosPath;
            var cdnUrlPath = await _imageKitUploadService.UploadFileAsync(logoImageFormFile, uploadFolderPath ?? string.Empty);

            tenant.LogoImageUrl = cdnUrlPath;
            await _tenantRepository.UpdateAsync(tenant);

            return cdnUrlPath;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }
}
