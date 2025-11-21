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
            await _databaseCreator.CreateDatabaseAsync(tenant.ConnectionString, CancellationToken.None).ConfigureAwait(false);

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

    /// <summary>
    /// Verifies that a subscription has been fully paid before approval
    /// </summary>
    /// <param name="subscription">The subscription to verify</param>
    /// <returns>True if subscription has at least one fully paid invoice, false otherwise</returns>
    private async Task<bool> VerifySubscriptionPaymentAsync(TenantSubscription subscription)
    {
        try
        {
            // Load subscription with invoices and payments
            var subscriptionWithInvoices = await _tenantSubscriptionRepository
                .GetByIdWithInvoicesAndPaymentsAsync(subscription.Id);

            if (subscriptionWithInvoices == null)
            {
                Logger.LogWarning(
                    "Subscription {SubscriptionId} not found for payment verification",
                    subscription.Id);
                return false;
            }

            if (subscriptionWithInvoices.TenantSubscriptionInvoices == null ||
                !subscriptionWithInvoices.TenantSubscriptionInvoices.Any())
            {
                Logger.LogWarning(
                    "No invoices found for subscription {SubscriptionId}",
                    subscription.Id);
                return false;
            }

            // Check if at least one invoice is fully paid
            foreach (var invoice in subscriptionWithInvoices.TenantSubscriptionInvoices)
            {
                var successfulPayments = invoice.TenantSubscriptionPayments
                    ?.Where(p => p.PaymentStatus == PaymentStatusEnum.Successful)
                    .Sum(p => p.Amount) ?? 0;

                // Calculate total due (invoice amount + late fees if applicable)
                var totalDue = invoice.Amount + (invoice.LateFeeAmount ?? 0);

                if (successfulPayments >= totalDue)
                {
                    Logger.LogInformation(
                        "Subscription {SubscriptionId} has fully paid invoice {InvoiceNo}. " +
                        "Total due: {TotalDue}, Payments: {Payments}",
                        subscription.Id,
                        invoice.InvoiceNo,
                        totalDue,
                        successfulPayments);
                    return true;
                }
                else
                {
                    Logger.LogDebug(
                        "Invoice {InvoiceNo} for subscription {SubscriptionId} is not fully paid. " +
                        "Total due: {TotalDue}, Payments: {Payments}",
                        invoice.InvoiceNo,
                        subscription.Id,
                        totalDue,
                        successfulPayments);
                }
            }

            Logger.LogWarning(
                "Subscription {SubscriptionId} does not have any fully paid invoices",
                subscription.Id);
            return false;
        }
        catch (Exception ex)
        {
            Logger.LogError(
                ex,
                "Error verifying payment for subscription {SubscriptionId}: {Error}",
                subscription.Id,
                ex.Message);
            return false;
        }
    }

    public async Task ApproveTenantAsync(string tenantCode, bool force = false, CancellationToken cancellationToken = default)
    {
        try
        {
            ValidateTenantCode(tenantCode);

            var tenant = await _tenantRepository.GetSingleAsync(tenant => tenant.TenantCode == tenantCode)
                          ?? throw RecordNotFoundException("ERR008");

            if (tenant.TenantStatus == TenantStatusEnum.Active && !force)
                throw ServiceOperationException("ERR060");

            // tenant is guaranteed to be non-null after null-coalescing operator above
            {
                var tenantSubscriptions = await _tenantSubscriptionRepository.GetByTenantAsync(tenant.TenantCode);
                if (tenantSubscriptions == null || !tenantSubscriptions.Any())
                    throw RecordNotFoundException("ERR011");

                var pendingTenantSubscription = tenantSubscriptions.FirstOrDefault(p => p.IsActive == true && p.SubscriptionStatus != SubscriptionStatusEnum.Active)
                    ?? throw RecordNotFoundException("ERR061");

                // Verify payment before provisioning database
                Logger.LogInformation(
                    "Verifying payment for subscription {SubscriptionId} before approving tenant {TenantCode}",
                    pendingTenantSubscription.Id,
                    tenantCode);

                var isPaid = await VerifySubscriptionPaymentAsync(pendingTenantSubscription);

                if (!isPaid)
                {
                    Logger.LogWarning(
                        "Cannot approve tenant {TenantCode}: Subscription payment not verified. " +
                        "Subscription must have at least one fully paid invoice before approval.",
                        tenantCode);

                    throw new ServiceOperationException(
                        "Subscription payment must be verified before tenant approval",
                        new Dictionary<string, string[]>
                        {
                            { "Payment", new[] { "Subscription must have a fully paid invoice before approval" } }
                        });
                }

                Logger.LogInformation(
                    "Payment verified for subscription {SubscriptionId}. Proceeding with database provisioning for tenant {TenantCode}",
                    pendingTenantSubscription.Id,
                    tenantCode);

                // Create and provision tenant database ONLY after payment verification
                // This ensures the tenant has a fully functional database when activated
                try
                {
                    Logger.LogInformation(
                        "Provisioning database for tenant {TenantCode} during approval process",
                        tenantCode);

                    var connectionString = _appSettings.TenantConnStrTemplate
                        .Replace("{TENANTCODE}", tenantCode);

                    if (string.IsNullOrEmpty(connectionString))
                    {
                        throw new InvalidOperationException(
                            "TenantConnStrTemplate is not configured in ApplicationSettings");
                    }

                    await _databaseCreator.CreateDatabaseAsync(connectionString, cancellationToken).ConfigureAwait(false);

                    Logger.LogInformation(
                        "Successfully provisioned database for tenant {TenantCode}",
                        tenantCode);
                }
                catch (Exception dbEx)
                {
                    Logger.LogError(
                        dbEx,
                        "Failed to provision database for tenant {TenantCode}: {Error}",
                        tenantCode,
                        dbEx.Message);

                    // Re-throw as a service operation exception to maintain consistency
                    throw new ServiceOperationException(
                        $"Failed to provision tenant database: {dbEx.Message}",
                        new Dictionary<string, string[]>
                        {
                            { "DatabaseProvisioning", new[] { dbEx.Message } }
                        });
                }

                // Activate subscription
                pendingTenantSubscription.SubscriptionStatus = SubscriptionStatusEnum.Active;
                await _tenantSubscriptionRepository.UpdateAsync(pendingTenantSubscription);
                await _tenantSubscriptionRepository.CommitAsync();

                // Activate tenant
                tenant.TenantStatus = TenantStatusEnum.Active;
                await _tenantRepository.UpdateAsync(tenant);
                await _tenantRepository.CommitAsync();

                Logger.LogInformation(
                    "Successfully approved and activated tenant {TenantCode}",
                    tenantCode);
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
    /// Resolves tenant from HTTP context using subdomain pattern matching.
    /// Uses configurable domain pattern from ApplicationSettings.TenantDomainPattern.
    /// </summary>
    /// <param name="context">The HTTP context containing request host information</param>
    /// <returns>TenantDto if tenant is found and resolved, null otherwise</returns>
    /// <exception cref="ServiceOperationException">Thrown when tenant cannot be resolved from host</exception>
    public async Task<TenantDto?> ResolveTenantAsync(HttpContext context)
    {
        try
        {
            var host = context.Request.Host.Host;

            // Get configurable domain pattern from settings, with sensible default
            var domainPattern = _appSettings.TenantDomainPattern 
                ?? @"^(?<tenant>[a-zA-Z0-9-]+)\.yourapp\.com$";

            Logger.LogDebug(
                "Resolving tenant from host '{Host}' using pattern '{Pattern}'",
                host,
                domainPattern);

            // Match tenant code from subdomain using configurable pattern
            var match = Regex.Match(host, domainPattern, RegexOptions.IgnoreCase);
            
            if (!match.Success)
            {
                var apiMsg = MessageProviderService.GetMessage("ERR062");
                var errMsg = apiMsg.Message.Replace("{{host}}", host);
                Logger.LogWarning(
                    "Failed to resolve tenant from host '{Host}'. Pattern '{Pattern}' did not match.",
                    host,
                    domainPattern);
                throw new ServiceOperationException(errMsg);
            }

            var tenantCode = match.Groups["tenant"].Value;

            if (string.IsNullOrWhiteSpace(tenantCode))
            {
                var apiMsg = MessageProviderService.GetMessage("ERR062");
                var errMsg = apiMsg.Message.Replace("{{host}}", host);
                Logger.LogWarning(
                    "Tenant code extracted from host '{Host}' is empty. Pattern: '{Pattern}'",
                    host,
                    domainPattern);
                throw new ServiceOperationException(errMsg);
            }

            Logger.LogDebug("Extracted tenant code '{TenantCode}' from host '{Host}'", tenantCode, host);

            // Retrieve the tenant from the database using the tenant code
            var tenant = await _tenantRepository.GetSingleAsync(tenant => tenant.TenantCode == tenantCode);

            if (tenant == null)
            {
                var apiMsg = MessageProviderService.GetMessage("ERR063");
                var errMsg = apiMsg.Message.Replace("{{tenantCode}}", tenantCode);
                Logger.LogWarning(
                    "Tenant with code '{TenantCode}' not found in database. Host: '{Host}'",
                    tenantCode,
                    host);
                throw new ServiceOperationException(errMsg);
            }

            var tenantDto = Mapper.Map<TenantDto>(tenant);

            Logger.LogInformation(
                "Successfully resolved tenant '{TenantCode}' ({TenantName}) from host '{Host}'",
                tenantCode,
                tenant.Name,
                host);

            return tenantDto;
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
