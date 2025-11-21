using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs;

public class CustomerDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }

    public required string Name { get; set; }

    public string? IndustryType { get; set; } // Only for businesses

    public string? GeneralEmail { get; set; }
    public required string MainPhone { get; set; } // General company number (optional)
    
    public string? Website { get; set; }

    
    public CustomerStatusEnum CustomerStatus { get; set; }



    public bool? IsImported { get; set; }
    public string? Notes { get; set; }

    public IEnumerable<string>? Tags { get; set; }

    public TaxExemptionReasonsEnum? TaxExemptionReason { get; set; } = TaxExemptionReasonsEnum.NotExempt;

    // Navigation properties for related entities
    public ICollection<CustomerContactDto> CustomerContacts { get; set; } = new HashSet<CustomerContactDto>();
    public ICollection<PropertyAssetDto> PropertyAssets { get; set; } = new HashSet<PropertyAssetDto>();
    public ICollection<AddressDto> Addresses { get; set; } = new HashSet<AddressDto>();

    public ICollection<TintMaterialPriceOverride> TintMaterialPriceOverrides { get; set; } = new HashSet<TintMaterialPriceOverride>();

}

