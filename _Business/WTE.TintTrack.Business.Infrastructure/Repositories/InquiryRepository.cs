using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Infrastructure.Repositories;

public class InquiryRepository(TenantDbContext dbContext)
    : RepositoryForGuidKeyedEntity<Inquiry>(dbContext), IInquiryRepository
{ }
