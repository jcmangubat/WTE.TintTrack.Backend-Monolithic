using WTE.TintTrack.Business.Application.Interfaces;
using WTE.TintTrack.Business.Application.Services;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories.TintMaterialRepos;
using WTE.TintTrack.Business.Infrastructure.Repositories;
using WTE.TintTrack.Business.Infrastructure.Repositories.TintMaterialRepos;

namespace WTE.TintTrack.Api.Helpers.Extensions;

/// <summary>
/// Extension methods for registering Business domain services and repositories
/// </summary>
public static class BusinessServiceRegistration
{
    /// <summary>
    /// Registers all Business domain repositories
    /// </summary>
    public static IServiceCollection AddBusinessRepositories(this IServiceCollection services)
    {
        // Core business repositories
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IPropertyAssetRepository, PropertyAssetRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<ICustomerContactRepository, CustomerContactRepository>();

        // Inventory and materials repositories
        services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
        services.AddScoped<ITintMaterialRepository, TintMaterialRepository>();
        services.AddScoped<ITintMaterialPriceScheduleRepository, TintMaterialPriceScheduleRepository>();
        services.AddScoped<ITintMaterialPriceTierRepository, TintMaterialPriceTierRepository>();
        services.AddScoped<ITintMaterialPriceHistoryRepository, TintMaterialPriceHistoryRepository>();
        services.AddScoped<ITintMaterialPriceOverrideRepository, TintMaterialPriceOverrideRepository>();
        
        // Inquiry repository
        services.AddScoped<IInquiryRepository, InquiryRepository>();

        // Note: Commented repositories can be uncommented when features are implemented
        //services.AddScoped<IProposalRepository, ProposalRepository>();
        //services.AddScoped<ICustomerOwnershipRepository, CustomerOwnershipRepository>();
        //services.AddScoped<IQuoteRepository, QuoteRepository>();
        //services.AddScoped<IProjectRepository, ProjectRepository>();
        //services.AddScoped<IInvoiceRepository, InvoiceRepository>();

        return services;
    }

    /// <summary>
    /// Registers all Business domain services
    /// </summary>
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        // Core business services
        services.AddScoped<IAuditLogService, AuditLogService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<ICustomerContactService, CustomerContactService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IPropertyAssetService, PropertyService>();

        // Inquiry services
        services.AddScoped<IInquiryService, InquiryService>();
        //services.AddScoped<IProposalService, ProposalService>();

        // Tint material services
        services.AddScoped<ITintMaterialService, TintMaterialService>();
        services.AddScoped<ITintMaterialPriceHistoryService, TintMaterialPriceHistoryService>();
        services.AddScoped<ITintMaterialPriceOverrideService, TintMaterialPriceOverrideService>();
        services.AddScoped<ITintMaterialPriceScheduleService, TintMaterialPriceScheduleService>();
        services.AddScoped<ITintMaterialPriceTierService, TintMaterialPriceTierService>();

        // Note: Commented services can be uncommented when features are implemented
        //services.AddScoped<ICustomerOwnershipService, CustomerOwnershipService>();
        //services.AddScoped<IInvoiceService, InvoiceService>();
        //services.AddScoped<IProjectService, ProjectService>();
        //services.AddScoped<IQuoteService, QuoteService>();

        return services;
    }
}

