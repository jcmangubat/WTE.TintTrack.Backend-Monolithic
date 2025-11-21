using AutoMapper;
using WTE.TintTrack.Api.Messaging.Business.Requests.Contact;
using WTE.TintTrack.Api.Messaging.Business.Requests.Customer;
using WTE.TintTrack.Api.Messaging.Business.Requests.Inquiry;
using WTE.TintTrack.Api.Messaging.Business.Requests.TintMaterial;
using WTE.TintTrack.Api.Messaging.Business.Responses.Contact;
using WTE.TintTrack.Api.Messaging.Business.Responses.Customer;
using WTE.TintTrack.Api.Messaging.Business.Responses.CustomerContact;
using WTE.TintTrack.Api.Messaging.Business.Responses.Inquiry;
using WTE.TintTrack.Api.Messaging.Business.Responses.PropertyAsset;
using WTE.TintTrack.Api.Messaging.Business.Responses.TintMaterial;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.DTOs.PropertySpecificationModels;
using WTE.TintTrack.Business.Application.DTOs.TintMaterialModels;

namespace WTE.TintTrack.Api.Messaging._Mappings;

public class BusinessDomainMappingProfile : Profile
{
    public BusinessDomainMappingProfile()
    {
        CreateMap<CustomerDto, CustomerResponse>();
        CreateMap<CreateCustomerRequest, CustomerDto>()
            .ForMember(p => p.Id, opt => opt.MapFrom(p => Guid.NewGuid()));

        CreateMap<ContactDto, ContactResponse>();
        CreateMap<CreateContactRequest, ContactDto>()
            .ForMember(p => p.Id, opt => opt.MapFrom(p => Guid.NewGuid()));

        CreateMap<CustomerContactDto, CustomerContactResponse>()
            .ForMember(p => p.CustomerCode, opt => opt.MapFrom(p => p.Customer == null ? string.Empty : p.Customer.Code))
            .ForMember(p => p.ContactCode, opt => opt.MapFrom(p => p.Contact == null ? string.Empty : p.Contact.Code));

        CreateMap<PropertyAssetDto, PropertyAssetResponse>()
            .Include<ArchitecturalPropertyAssetDto, PropertyAssetResponse>()
            .Include<AutomotivePropertyAssetDto, PropertyAssetResponse>()
            .Include<ResidentialPropertyAssetDto, PropertyAssetResponse>()
            .Include<CommercialPropertyAssetDto, PropertyAssetResponse>()
            .Include<SpecialtyPropertyAssetDto, PropertyAssetResponse>()
            .Include<GlassFilmPropertyAssetDto, PropertyAssetResponse>()
            .Include<EnergyEfficientPropertyAssetDto, PropertyAssetResponse>()
            .Include<CustomPropertyAssetDto, PropertyAssetResponse>()
            .Include<SignagePropertyAssetDto, PropertyAssetResponse>()
            .Include<OutdoorPropertyAssetDto, PropertyAssetResponse>();

        CreateMap<PropertyAssetResponse, PropertyAssetDto>()
            .Include<PropertyAssetResponse, ArchitecturalPropertyAssetDto>()
            .Include<PropertyAssetResponse, AutomotivePropertyAssetDto>()
            .Include<PropertyAssetResponse, ResidentialPropertyAssetDto>()
            .Include<PropertyAssetResponse, CommercialPropertyAssetDto>()
            .Include<PropertyAssetResponse, SpecialtyPropertyAssetDto>()
            .Include<PropertyAssetResponse, GlassFilmPropertyAssetDto>()
            .Include<PropertyAssetResponse, EnergyEfficientPropertyAssetDto>()
            .Include<PropertyAssetResponse, CustomPropertyAssetDto>()
            .Include<PropertyAssetResponse, SignagePropertyAssetDto>()
            .Include<PropertyAssetResponse, OutdoorPropertyAssetDto>();

        // Specific mappings
        CreateMap<ArchitecturalPropertyAssetDto, PropertyAssetResponse>();
        CreateMap<PropertyAssetResponse, ArchitecturalPropertyAssetDto>();

        CreateMap<AutomotivePropertyAssetDto, PropertyAssetResponse>();
        CreateMap<PropertyAssetResponse, AutomotivePropertyAssetDto>();

        CreateMap<ResidentialPropertyAssetDto, PropertyAssetResponse>();
        CreateMap<PropertyAssetResponse, ResidentialPropertyAssetDto>();

        CreateMap<CommercialPropertyAssetDto, PropertyAssetResponse>();
        CreateMap<PropertyAssetResponse, CommercialPropertyAssetDto>();

        CreateMap<SpecialtyPropertyAssetDto, PropertyAssetResponse>();
        CreateMap<PropertyAssetResponse, SpecialtyPropertyAssetDto>();

        CreateMap<GlassFilmPropertyAssetDto, PropertyAssetResponse>();
        CreateMap<PropertyAssetResponse, GlassFilmPropertyAssetDto>();

        CreateMap<EnergyEfficientPropertyAssetDto, PropertyAssetResponse>();
        CreateMap<PropertyAssetResponse, EnergyEfficientPropertyAssetDto>();

        CreateMap<CustomPropertyAssetDto, PropertyAssetResponse>();
        CreateMap<PropertyAssetResponse, CustomPropertyAssetDto>();

        CreateMap<SignagePropertyAssetDto, PropertyAssetResponse>();
        CreateMap<PropertyAssetResponse, SignagePropertyAssetDto>();

        CreateMap<OutdoorPropertyAssetDto, PropertyAssetResponse>();
        CreateMap<PropertyAssetResponse, OutdoorPropertyAssetDto>();

        CreateMap<InquiryDto, InquiryResponse>();
        CreateMap<CreateInquiryRequest, InquiryDto>()
            .ForMember(p => p.Id, opt => opt.MapFrom(p => Guid.NewGuid()));

        CreateMap<TintMaterialDto, TintMaterialResponse>();
        CreateMap<CreateTintMaterialRequest, TintMaterialDto>()
            .ForMember(p => p.Id, opt => opt.MapFrom(p => Guid.NewGuid()));
    }
}
