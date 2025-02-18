using WTE.TintTrack.Application.Shared.ModelAbstraction;

namespace WTE.TintTrack.Api.Messaging._Abstractions;

public class ApiMessageResponse : IAuditableEntity
{
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
    public bool? IsArchived { get; set; }
    public DateTime? DateArchived { get; set; }
    public string? ReasonArchived { get; set; }
    public bool? IsActive { get; set; }
}