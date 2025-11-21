using WTE.TintTrack.Domain.Shared;

namespace WTE.TintTrack.Business.Application.DTOs.old;

public class AddressDto : CommonAddress, ICodedEntity
{
    public required string Code { get; set; }

    // Navigation property for associated entities
    public Guid? CustomerId { get; set; }
    public virtual CustomerDto? Customer { get; set; }

    public Guid? ContactId { get; set; }
    public virtual ContactDto? Contact { get; set; }
}

