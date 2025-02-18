using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Domain.Interfaces.Repositories;

public interface ITokenRepository : IRepositoryForKeyedEntity<Token, Guid>
{
    Task DeleteTokenAsync(Guid tokenId);
    Task DeleteTokensByUserIdAsync(Guid userId);

    Task<Token?> GetTokenByUserAsync(Guid userId, Guid? tenantId = null, Consts.ActiveInclusionOptionsEnum ActiveInclusionOption = Consts.ActiveInclusionOptionsEnum.ACTIVE_ONLY);
    Task<IEnumerable<Token>> GetTokensByUserAsync(Guid userId, Guid? tenantId = null, Consts.ActiveInclusionOptionsEnum ActiveInclusionOption = Consts.ActiveInclusionOptionsEnum.ACTIVE_ONLY);
    Task<IEnumerable<Token>> GetTokensByUserAsync(string emailAddress, string? tenantCode = null, Consts.ActiveInclusionOptionsEnum ActiveInclusionOption = Consts.ActiveInclusionOptionsEnum.ACTIVE_ONLY);

    Task<Token?> GetTokenAsync(string refreshToken);
}
