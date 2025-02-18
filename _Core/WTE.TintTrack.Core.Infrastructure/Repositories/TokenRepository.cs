using Microsoft.EntityFrameworkCore;
using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Infrastructure.Repositories;

public class TokenRepository(ApplicationDbContext context) :
    RepositoryForGuidKeyedEntity<Token>(context), ITokenRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Token?> GetTokenAsync(string refreshToken)
    {
        return await _context.Tokens
            .FirstOrDefaultAsync(t => t.RefreshToken == refreshToken);
    }

    public async Task DeleteTokenAsync(Guid tokenId)
    {
        var token = await _context.Tokens.FindAsync(tokenId);
        if (token != null)
            _context.Tokens.Remove(token);
    }

    public async Task DeleteTokensByUserIdAsync(Guid userId)
    {
        var tokens = await _context.Tokens
            .Where(t => t.UserId == userId)
            .ToListAsync();

        _context.Tokens.RemoveRange(tokens);
    }

    public async Task<IEnumerable<Token>> GetTokensByUserAsync(Guid userId, Guid? tenantId = null, Consts.ActiveInclusionOptionsEnum ActiveInclusionOption = Consts.ActiveInclusionOptionsEnum.ACTIVE_ONLY)
    {
        var results = await _context.Tokens
                        .Include(t => t.User).Include(t => t.Tenant)
                        .OrderBy(t => t.DateCreated)
                        .Where(t => t.UserId == userId &&
                            (tenantId == null || tenantId.HasValue && tenantId.Value == t.TenantId) &&
                            (ActiveInclusionOption == Consts.ActiveInclusionOptionsEnum.ALL ||
                            ActiveInclusionOption == Consts.ActiveInclusionOptionsEnum.ACTIVE_ONLY && t.IsActive == true && t.RefreshTokenExpiration <= DateTime.Now ||
                            ActiveInclusionOption == Consts.ActiveInclusionOptionsEnum.INACTIVE_ONLY && t.IsActive == false && t.RefreshTokenExpiration > DateTime.Now)
                        )
                        .ToListAsync();
        return results;
    }

    public async Task<Token?> GetTokenByUserAsync(Guid userId, Guid? tenantId = null, Consts.ActiveInclusionOptionsEnum ActiveInclusionOption = Consts.ActiveInclusionOptionsEnum.ACTIVE_ONLY)
    {
        return await _context.Tokens
                        .Include(t => t.User).Include(t => t.Tenant)
                        .OrderBy(t => t.DateCreated)
                        .FirstOrDefaultAsync(t => t.UserId == userId &&
                            (tenantId == null || tenantId.HasValue && tenantId.Value == t.TenantId) &&
                            (ActiveInclusionOption == Consts.ActiveInclusionOptionsEnum.ALL ||
                            ActiveInclusionOption == Consts.ActiveInclusionOptionsEnum.ACTIVE_ONLY && t.IsActive == true && t.RefreshTokenExpiration <= DateTime.Now ||
                            ActiveInclusionOption == Consts.ActiveInclusionOptionsEnum.INACTIVE_ONLY && t.IsActive == false && t.RefreshTokenExpiration > DateTime.Now)
                        );
    }

    public async Task<IEnumerable<Token>> GetTokensByUserAsync(string emailAddress, string? tenantCode = null, Consts.ActiveInclusionOptionsEnum ActiveInclusionOption = Consts.ActiveInclusionOptionsEnum.ACTIVE_ONLY)
    {
        var results = await _context.Tokens
                        .Include(t => t.User).Include(t => t.Tenant)
                        .OrderBy(t => t.DateCreated).ThenBy(t => t.DateModified)
                        .Where(t => t.User.Email == emailAddress &&
                                    (tenantCode == null && (t.Tenant == null || t.Tenant != null && t.Tenant.TenantCode == tenantCode) ||
                                    !string.IsNullOrEmpty(tenantCode) && t.Tenant != null && t.Tenant.TenantCode == tenantCode) &&
                                    (ActiveInclusionOption == Consts.ActiveInclusionOptionsEnum.ALL ||
                                    ActiveInclusionOption == Consts.ActiveInclusionOptionsEnum.ACTIVE_ONLY && t.IsActive == true && t.RefreshTokenExpiration > DateTime.UtcNow ||
                                    ActiveInclusionOption == Consts.ActiveInclusionOptionsEnum.INACTIVE_ONLY && t.IsActive == false && t.RefreshTokenExpiration <= DateTime.UtcNow)
                                )
                        .ToListAsync();
        return results;
    }
}