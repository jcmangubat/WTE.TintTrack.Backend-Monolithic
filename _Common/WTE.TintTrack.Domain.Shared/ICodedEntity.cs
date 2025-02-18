using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Interfaces;

namespace WTE.TintTrack.Domain.Shared;

public interface ICodedEntity : IKeyedEntity<Guid>
{
    string Code { get; set; }
}
