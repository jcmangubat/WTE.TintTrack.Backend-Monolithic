using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities;

public class Contact : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }
    public GendersEnum? Gender { get; set; }
    public MaritalStatusEnum? MaritalStatus { get; set; }

    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? AltPhone { get; set; }
    
    public string? JobTitle { get; set; }

    //public required IEnumerable<ContactTypesEnum> ContactTypes { get; set; }

    public IEnumerable<string>? Tags { get; set; }
    public string? Notes { get; set; }
    public bool? IsImported { get; set; }

    // Navigation property for associated entities
    public ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
    public ICollection<CustomerContact> CustomerContacts { get; set; } = new HashSet<CustomerContact>();
}