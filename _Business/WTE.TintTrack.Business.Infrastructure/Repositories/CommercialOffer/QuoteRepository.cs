using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories.CommercialOfferRepos;

namespace WTE.TintTrack.Business.Infrastructure.Repositories.CommercialOffer;

public class QuoteRepository(TenantDbContext dbContext) : RepositoryForGuidKeyedEntity<Quote>(dbContext), IQuoteRepository { }



