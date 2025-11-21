using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.old;

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

    public IEnumerable<string>? Tags { get; set; } = [];

    public TaxExemptionReasonsEnum? TaxExemptionReason { get; set; } = TaxExemptionReasonsEnum.NotExempt;

    /// <summary>
    /// Navigation properties for related entities
    /// </summary>
    public virtual IEnumerable<ContactDto> Contacts { get; set; } = [];

    /// <summary>
    /// Navigation properties for related entities
    /// </summary>  
    public virtual IEnumerable<PropertyAssetDto> CustomerProperties { get; set; } = [];


    public IEnumerable<string> AddressCodes { get; set; } = [];
    public virtual IEnumerable<AddressDto> Addresses { get; set; } = [];
    public virtual IEnumerable<ProductPriceOverrideDto> ProductPriceOverrides { get; set; } = [];

    /*
        public virtual ICollection<CustomerContact> CustomerContacts { get; set; } = new HashSet<CustomerContact>();
        public virtual ICollection<PropertyAsset> PropertyAssets { get; set; } = new HashSet<PropertyAsset>();
        public virtual ICollection<Address> Addresses { get; set; } = new HashSet<Address>();

        public virtual ICollection<CustomerInquiry> CustomerInquiries { get; set; } = new HashSet<CustomerInquiry>();

        public virtual ICollection<ProductPriceOverride> ProductPriceOverrides { get; set; } = new HashSet<ProductPriceOverride>();
     */
}

