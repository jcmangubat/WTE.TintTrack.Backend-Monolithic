using AutoMapper;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.DTOs.TintMaterialModels;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;

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
            /*.ForMember(dest => dest.CustomerCodes, opt => opt.MapFrom(src => src.CustomerContacts == null ?
                                                                                new List<string>() :
                                                                                src.CustomerContacts
                                                                                        .Where(cc => cc.Customer != null)
                                                                                        .Select(x => x.Customer.Code)))*/
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));

         CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty))
            /*.ForMember(dest => dest.ContactCodes, opt => opt.MapFrom(src => src.CustomerContacts == null ?
                                                                                new List<string>() :
                                                                                src.CustomerContacts
                                                                                        .Where(cc => cc.Contact != null)
                                                                                        .Select(x => x.Contact.Code)))*/
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));

        CreateMap<CustomerContact, CustomerContactDto>()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));

        CreateMap<PropertyAsset, PropertyAssetDto>()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));

        
        CreateMap<Inquiry, InquiryDto>().ReverseMap();
        CreateMap<InventoryItem, InventoryItemDto>().ReverseMap();

        CreateMap<TintMaterial, TintMaterialDto>().ReverseMap();
        CreateMap<TintMaterialPriceHistory, TintMaterialPriceHistoryDto>().ReverseMap();
        CreateMap<TintMaterialPriceOverride, TintMaterialPriceOverrideDto>().ReverseMap();
        CreateMap<TintMaterialPriceSchedule, TintMaterialPriceScheduleDto>().ReverseMap();
        CreateMap<TintMaterialPriceTier, TintMaterialPriceTierDto>().ReverseMap();

        //CreateMap<Proposal, ProposalDto>();
        //CreateMap<Quote, QuoteDto>().ReverseMap();
        //CreateMap<Estimate, EstimateDto>().ReverseMap();

        //CreateMap<WorkOrder, WorkOrderDto>();
        //CreateMap<WorkOrderItem, WorkOrderItemDto>();

        //CreateMap<Invoice, InvoiceDto>();
        //CreateMap<InvoiceItem, InvoiceItemDto>();
        


        /*CreateMap<CustomerOwnership, CustomerOwnershipDto>()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));

        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.ReasonArchived, opt => opt.MapFrom(src => src.ReasonArchived ?? string.Empty));*/


    }
}
