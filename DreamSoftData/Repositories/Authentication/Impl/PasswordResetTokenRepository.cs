using DreamSoftData.Context;
using DreamSoftData.Entities.Authentication;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Authentication.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DreamSoftData.Repositories.Authentication.Impl;

public class PasswordResetTokenRepository(DreamSoftDbContext dbContext)
    : BaseRepository<PasswordResetTokens, int>(dbContext), IPasswordResetTokenRepository
{
    public Task<PasswordResetTokens?> GetByTokenHashAsync(string tokenHash)
    {
        return DbSet
            .Include(t => t.User)
            .Include(t => t.Account)
            .FirstOrDefaultAsync(t => t.TokenHash == tokenHash);
    }

    public Task<int> CountRecentTokensAsync(string email, string username, DateTime since)
    {
        return DbSet
            .Where(t => t.Email.Equals(email)
                     && t.Username.Equals(username)
                     && t.CreatedAt >= since)
            .CountAsync();
    }

    public async Task RevokeUserTokensAsync(int userId)
    {
        await DbSet
            .Where(t => t.UserId == userId && !t.IsUsed && t.ExpiresAt > DateTime.UtcNow)
            .ExecuteUpdateAsync(s => s.SetProperty(t => t.IsUsed, true));
    }
}
