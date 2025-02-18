using AutoMapper;
using WTE.TintTrack.Api.Messaging.Core.Request;
using WTE.TintTrack.Api.Messaging.Core.Responses;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

namespace WTE.TintTrack.Api.Messaging._Mappings;

public class CoreDomainMappingProfile : Profile
{
    public CoreDomainMappingProfile()
    {
        CreateMap<UserRegisterRequest, ApplicationUserDto>();
        CreateMap<UserProfileDetailedRequest, ApplicationUserDto>()
            .ForMember(p => p.JobTitle, opt => opt.MapFrom(p => p.CompanyRole))
            .ReverseMap();

        CreateMap<UpdateUserProfileRequest, ApplicationUserDto>()
            .ForMember(p => p.JobTitle, opt => opt.MapFrom(p => p.CompanyRole))
            .ReverseMap();

        CreateMap<ApplicationUserDto, UserResponse>()
            .ForMember(p => p.CompanyRole, opt => opt.MapFrom(p => p.JobTitle));

        CreateMap<RegisterTenantRequest, TenantDto>();
        CreateMap<TenantDto, TenantResponse>();

        CreateMap<SubscriptionPlanRequest, SubscriptionPlanDto>();
        CreateMap<SubscriptionPlanDto, SubscriptionPlanResponse>();

        CreateMap<SubscriptionPlanDiscountRequest, SubscriptionPlanDiscountDto>();
        CreateMap<SubscriptionPlanDiscountDto, SubscriptionPlanDiscountResponse>();

        CreateMap<SubscriptionPlanFeatureDto, SubscriptionPlanFeatureResponse>();

        CreateMap<TenantSubscriptionDto, TenantSubscriptionResponse>()
            .ForMember(dest => dest.TenantCode, opt => opt.MapFrom(src => src.Tenant == null ? string.Empty : src.Tenant.TenantCode))
            .ForMember(dest => dest.SubscriptionPlanCode, opt => opt.MapFrom(src => src.SubscriptionPlan == null ? string.Empty : src.SubscriptionPlan.PlanCode))
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));

        CreateMap<UserTenantRoleDto, UserTenantRoleResponse>()
            .ForMember(dest => dest.TenantCode, opt => opt.MapFrom(src => src.UserTenant.Tenant == null ? string.Empty : src.UserTenant.Tenant.TenantCode))
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role == null ? string.Empty : src.Role.Name));


        CreateMap<TenantSubscriptionInvoiceDto, TenantSubscriptionInvoiceResponse>()
            .ForMember(dest => dest.PlanCode, opt => opt.MapFrom(src => src.TenantSubscription == null || src.TenantSubscription.SubscriptionPlan == null
                                                                        ? string.Empty
                                                                        : src.TenantSubscription.SubscriptionPlan.PlanCode));

        CreateMap<CreateTenantSubscriptionInvoiceRequest, TenantSubscriptionInvoiceDto>();
        CreateMap<UpdateTenantSubscriptionInvoiceRequest, TenantSubscriptionInvoiceDto>();

        CreateMap<UserBillingProfileRequest, UserBillingProfileDto>();
    }
}
