using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs.CommercialOffersModels;
using WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories.CommercialOfferRepos;

namespace WTE.TintTrack.Business.Application.Interfaces;

public interface IProposalService : IMappedLoggingServiceWithCRUD<IProposalService, IProposalRepository, Proposal, ProposalDto>
{
    Task<ProposalDto?> GetByCodeAsync(string code);
}
