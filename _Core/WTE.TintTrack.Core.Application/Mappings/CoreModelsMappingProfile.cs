using AutoMapper;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Validators;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Application.Mappings;

public class CoreModelsMappingProfile : Profile
{
    public CoreModelsMappingProfile()
    {
        CreateMap<ApplicationRole, ApplicationRoleDto>().ReverseMap();
        CreateMap<ApplicationRole, ApplicationRoleDtoValidator>().ReverseMap();

        CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
        CreateMap<SubscriptionPlan, SubscriptionPlanDto>().ReverseMap();
        CreateMap<SubscriptionPlanFeature, SubscriptionPlanFeatureDto>().ReverseMap();
        CreateMap<SubscriptionPlanFeatureAssociation, SubscriptionPlanFeatureAssociationDto>().ReverseMap();
        CreateMap<SubscriptionPlanDiscount, SubscriptionPlanDiscountDto>().ReverseMap();

        //CreateMap<ApplicationFeature, ApplicationFeatureDto>().ReverseMap();

        CreateMap<Tenant, TenantDto>().ReverseMap();
        CreateMap<TenantSubscription, TenantSubscriptionDto>()
            .ForMember(dest => dest.Tenant, opt => opt.Condition(src => src.Tenant != null))
            .ForMember(dest => dest.SubscriptionPlan, opt => opt.Condition(src => src.SubscriptionPlan != null))
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty)) // Set default if null
            .ReverseMap()
            .ForMember(dest => dest.Tenant, opt => opt.Condition(src => src.Tenant != null))
            .ForMember(dest => dest.SubscriptionPlan, opt => opt.Condition(src => src.SubscriptionPlan != null))
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty)); // Provide default if null in dto

        CreateMap<TenantSubscriptionInvoice, TenantSubscriptionInvoiceDto>().ReverseMap();
        CreateMap<TenantSubscriptionPayment, TenantSubscriptionPaymentDto>().ReverseMap();

        CreateMap<UserTenant, UserTenantDto>().ReverseMap();
        CreateMap<UserTenantRole, UserTenantRoleDto>().ReverseMap();
        CreateMap<UserTenantInvitation, UserTenantInvitationDto>().ReverseMap();

        CreateMap<Token, TokenDto>().ReverseMap();

        //CreateMap <>
    }
}
