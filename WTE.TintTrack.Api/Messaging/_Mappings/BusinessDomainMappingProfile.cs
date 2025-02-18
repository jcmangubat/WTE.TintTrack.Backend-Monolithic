using AutoMapper;
using WTE.TintTrack.Api.Messaging.Business.Request;
using WTE.TintTrack.Api.Messaging.Business.Responses;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.DTOs.PropertySpecifications;

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

        CreateMap<InquiryDto, InquiryResponse>();
        CreateMap<CreateInquiryRequest, InquiryDto>()
            .ForMember(p => p.Id, opt => opt.MapFrom(p => Guid.NewGuid()));

        // Base mapping
        CreateMap<PropertyDto, PropertyResponse>()
            .Include<ArchitecturalPropertyDto, PropertyResponse>()
            .Include<AutomotivePropertyDto, PropertyResponse>()
            .Include<ResidentialPropertyDto, PropertyResponse>()
            .Include<CommercialPropertyDto, PropertyResponse>()
            .Include<SpecialtyPropertyDto, PropertyResponse>()
            .Include<GlassFilmPropertyDto, PropertyResponse>()
            .Include<EnergyEfficientPropertyDto, PropertyResponse>()
            .Include<CustomPropertyDto, PropertyResponse>()
            .Include<SignagePropertyDto, PropertyResponse>()
            .Include<OutdoorPropertyDto, PropertyResponse>();

        CreateMap<PropertyResponse, PropertyDto>()
            .Include<PropertyResponse, ArchitecturalPropertyDto>()
            .Include<PropertyResponse, AutomotivePropertyDto>()
            .Include<PropertyResponse, ResidentialPropertyDto>()
            .Include<PropertyResponse, CommercialPropertyDto>()
            .Include<PropertyResponse, SpecialtyPropertyDto>()
            .Include<PropertyResponse, GlassFilmPropertyDto>()
            .Include<PropertyResponse, EnergyEfficientPropertyDto>()
            .Include<PropertyResponse, CustomPropertyDto>()
            .Include<PropertyResponse, SignagePropertyDto>()
            .Include<PropertyResponse, OutdoorPropertyDto>();

        // Specific mappings
        CreateMap<ArchitecturalPropertyDto, PropertyResponse>();
        CreateMap<PropertyResponse, ArchitecturalPropertyDto>();

        CreateMap<AutomotivePropertyDto, PropertyResponse>();
        CreateMap<PropertyResponse, AutomotivePropertyDto>();

        CreateMap<ResidentialPropertyDto, PropertyResponse>();
        CreateMap<PropertyResponse, ResidentialPropertyDto>();

        CreateMap<CommercialPropertyDto, PropertyResponse>();
        CreateMap<PropertyResponse, CommercialPropertyDto>();

        CreateMap<SpecialtyPropertyDto, PropertyResponse>();
        CreateMap<PropertyResponse, SpecialtyPropertyDto>();

        CreateMap<GlassFilmPropertyDto, PropertyResponse>();
        CreateMap<PropertyResponse, GlassFilmPropertyDto>();

        CreateMap<EnergyEfficientPropertyDto, PropertyResponse>();
        CreateMap<PropertyResponse, EnergyEfficientPropertyDto>();

        CreateMap<CustomPropertyDto, PropertyResponse>();
        CreateMap<PropertyResponse, CustomPropertyDto>();

        CreateMap<SignagePropertyDto, PropertyResponse>();
        CreateMap<PropertyResponse, SignagePropertyDto>();

        CreateMap<OutdoorPropertyDto, PropertyResponse>();
        CreateMap<PropertyResponse, OutdoorPropertyDto>();
    }
}
