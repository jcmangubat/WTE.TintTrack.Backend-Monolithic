using AutoMapper;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Business.Application.Services;

public class ProposalService(
    IMapper mapper,
    ILogger<ProposalService> logger,
    IMessageProviderService messageProviderService,
    IProposalRepository repository)
    : MappedLoggingServiceWithCRUD<IProposalService, IProposalRepository, Proposal, ProposalDto>(
        mapper, logger, messageProviderService, repository), IProposalService
{
    public async Task<ProposalDto?> GetByCodeAsync(string code)
    {
        var entity = await Repository.GetSingleAsync(p => p.Code == code);
        return Mapper.Map<ProposalDto>(entity);
    }
}
