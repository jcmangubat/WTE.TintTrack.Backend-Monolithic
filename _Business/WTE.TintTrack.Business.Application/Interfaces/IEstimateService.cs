using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs.CommercialOffersModels;
using WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories.CommercialOfferRepos;

namespace WTE.TintTrack.Business.Application.Interfaces;

public interface IEstimateService : IMappedLoggingServiceWithCRUD<IEstimateService, IEstimateRepository, Estimate, EstimateDto>
{
    Task<EstimateDto?> GetByCodeAsync(string code);
}
