using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using WTE.TintTrack.Business.Domain.Entities;

namespace WTE.TintTrack.Business.Domain.Interfaces.Repositories;

public interface IContactRepository : IRepositoryForKeyedEntity<Contact, Guid> { }
