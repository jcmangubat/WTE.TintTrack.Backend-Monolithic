using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.old;

public class ContactDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }

    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    
    public DateTime? DateOfBirth { get; set; }
    public GendersEnum? Gender { get; set; }
    public MaritalStatusEnum? MaritalStatus { get; set; }
    
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? AltPhone { get; set; }
    
    public string? JobTitle { get; set; }

    public IEnumerable<string>? Tags { get; set; }
    public string? Notes { get; set; }

    public bool? IsImported { get; set; }

    // Navigation property for associated entities
    public virtual IEnumerable<AddressDto> Addresses { get; set; } = [];

    public virtual IEnumerable<CustomerDto> Customers { get; set; } = [];
}