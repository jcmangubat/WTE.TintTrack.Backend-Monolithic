using WTE.TintTrack.Domain.Shared;

namespace WTE.TintTrack.Business.Domain.Entities;

public class Address : CommonAddress, ICodedEntity
{
    public required string Code { get; set; }

    // Navigation property for associated entities
    public Guid? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }

    public Guid? ContactId { get; set; }
    public virtual Contact? Contact { get; set; }
}

