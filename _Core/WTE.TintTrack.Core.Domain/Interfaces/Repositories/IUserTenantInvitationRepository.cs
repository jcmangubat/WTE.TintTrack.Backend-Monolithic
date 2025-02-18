using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Domain.Interfaces.Repositories;

public interface IUserTenantInvitationRepository : IRepositoryForKeyedEntity<UserTenantInvitation, Guid>
{ }