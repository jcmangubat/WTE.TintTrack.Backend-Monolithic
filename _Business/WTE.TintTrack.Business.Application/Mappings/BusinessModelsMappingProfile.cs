using AutoMapper;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Domain.Entities;

namespace WTE.TintTrack.Business.Application.Mappings;

public class BusinessModelsMappingProfile : Profile
{
    public BusinessModelsMappingProfile()
    {
        CreateMap<AuditLog, AuditLogDto>()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));

        CreateMap<Contact, ContactDto>()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty))
            .ForMember(dest => dest.CustomerCodes, opt => opt.MapFrom(src => src.CustomerContacts == null ?
                                                                                new List<string>() :
                                                                                src.CustomerContacts
                                                                                        .Where(cc => cc.Customer != null)
                                                                                        .Select(x => x.Customer.Code)))
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));

        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty))
            .ForMember(dest => dest.ContactCodes, opt => opt.MapFrom(src => src.CustomerContacts == null ?
                                                                                new List<string>() :
                                                                                src.CustomerContacts
                                                                                        .Where(cc => cc.Contact != null)
                                                                                        .Select(x => x.Contact.Code)))
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));

        CreateMap<Inquiry, InquiryDto>()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));

        CreateMap<CustomerContact, CustomerContactDto>()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));

        CreateMap<CustomerOwnership, CustomerOwnershipDto>()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));

        CreateMap<Property, PropertyDto>()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));

        CreateMap<Invoice, InvoiceDto>()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));

        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));

        CreateMap<Proposal, ProposalDto>()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));

        CreateMap<Quote, QuoteDto>()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));
    }
}
