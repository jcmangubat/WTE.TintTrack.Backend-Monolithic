using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities;

public class Customer : GuidKeyedAuditableEntity, ICodedEntity
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
    public ICollection<CustomerContact> CustomerContacts { get; set; } = new HashSet<CustomerContact>();
    public ICollection<PropertyAsset> PropertyAssets { get; set; } = new HashSet<PropertyAsset>();
    public ICollection<Address> Addresses { get; set; } = new HashSet<Address>();

    public ICollection<TintMaterialPriceOverride> TintMaterialPriceOverrides { get; set; } = new HashSet<TintMaterialPriceOverride>();

}

