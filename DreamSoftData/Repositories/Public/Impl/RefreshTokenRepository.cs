using DreamSoftData.Context;
using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Public.Interface;
using Microsoft.EntityFrameworkCore;

namespace DreamSoftData.Repositories.Public.Impl;

public class RefreshTokenRepository(DreamSoftDbContext dbContext)
    : GenericRepository<RefreshTokens, int>(dbContext), IRefreshTokenRepository
{
    private readonly DreamSoftDbContext _dbContext = dbContext;

    // Optionally add custom methods here
    public Task<bool> CheckTokenExistence(string token)
    {
        return _dbContext.RefreshTokens.AnyAsync(rt => rt.Token == token);
    }

    public Task<RefreshTokens?> GetRefreshTokenByToken(string token)
    {
        return _dbContext.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task RevokeUserRefreshTokens(int userid, string ipaddress)
    {
        await _dbContext.RefreshTokens
            .Where(rt => rt.UserId == userid && rt.RevokedAt == null)
            .ExecuteUpdateAsync(s => s
                .SetProperty(rt => rt.RevokedAt, DateTime.UtcNow)
                .SetProperty(rt => rt.RevokedByIp, ipaddress));
    }
}