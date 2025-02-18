namespace WTE.TintTrack.Application.Shared.ModelAbstraction;

public class AuditableEntity
{
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime? DateModified { get; set; } = DateTime.Now;
    public bool? IsArchived { get; set; }
    public DateTime? DateArchived { get; set; }
    public string? ReasonArchived { get; set; }
}
