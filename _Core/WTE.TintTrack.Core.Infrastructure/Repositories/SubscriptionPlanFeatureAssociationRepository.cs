using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Infrastructure.Repositories;

public class SubscriptionPlanFeatureAssociationRepository(ApplicationDbContext dbContext)
    : RepositoryGeneric<SubscriptionPlanFeatureAssociation, Guid>(dbContext), ISubscriptionPlanFeatureAssociationRepository
{
}
