using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs.TintMaterialModels;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories.TintMaterialRepos;

namespace WTE.TintTrack.Business.Application.Interfaces;

public interface ITintMaterialPriceHistoryService : IMappedLoggingServiceWithCRUD<ITintMaterialPriceHistoryService, ITintMaterialPriceHistoryRepository, TintMaterialPriceHistory, TintMaterialPriceHistoryDto>
{
}
