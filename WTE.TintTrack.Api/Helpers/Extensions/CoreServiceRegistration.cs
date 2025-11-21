using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Application.Services;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;
using WTE.TintTrack.Core.Infrastructure.Repositories;

namespace WTE.TintTrack.Api.Helpers.Extensions;

/// <summary>
/// Extension methods for registering Core domain services and repositories
/// </summary>
public static class CoreServiceRegistration
{
    /// <summary>
    /// Registers all Core domain repositories
    /// </summary>
    public static IServiceCollection AddCoreRepositories(this IServiceCollection services)
    {
        // User and authentication repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserBillingProfileRepository, UserBillingProfileRepository>();
        services.AddScoped<IUserTenantRepository, UserTenantRepository>();
        services.AddScoped<IUserTenantInvitationRepository, UserTenantInvitationRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        
        // Permission and role repositories
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();

        // Subscription repositories
        services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
        services.AddScoped<ISubscriptionPlanFeatureRepository, SubscriptionPlanFeatureRepository>();
        services.AddScoped<ISubscriptionPlanFeatureAssociationRepository, SubscriptionPlanFeatureAssociationRepository>();
        services.AddScoped<ISubscriptionPlanDiscountRepository, SubscriptionPlanDiscountRepository>();

        // Tenant repositories
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<ITenantSubscriptionRepository, TenantSubscriptionRepository>();
        services.AddScoped<ITenantSubscriptionInvoiceRepository, TenantSubscriptionInvoiceRepository>();
        services.AddScoped<ITenantSubscriptionPaymentRepository, TenantSubscriptionPaymentRepository>();

        return services;
    }

    /// <summary>
    /// Registers all Core domain services
    /// </summary>
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        // User and authentication services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserTenantService, UserTenantService>();
        services.AddScoped<IUserTenantInvitationService, UserTenantInvitationService>();
        services.AddScoped<IUserBillingProfileService, UserBillingProfileService>();
        services.AddScoped<IRolePermissionService, RolePermissionService>();

        // Subscription services
        services.AddScoped<ISubscriptionPlanService, SubscriptionPlanService>();
        services.AddScoped<ISubscriptionPlanDiscountService, SubscriptionPlanDiscountService>();
        services.AddScoped<ISubscriptionPlanFeatureService, SubscriptionPlanFeatureService>();

        // Tenant services
        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<ITenantSubscriptionService, TenantSubscriptionService>();
        services.AddScoped<ITenantSubscriptionInvoiceService, TenantSubscriptionInvoiceService>();
        services.AddScoped<ITenantSubscriptionPaymentService, TenantSubscriptionPaymentService>();

        return services;
    }
}

