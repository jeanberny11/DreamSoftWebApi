using DreamSoftData.Entities.Authentication;
using DreamSoftData.Repositories.Base;

namespace DreamSoftData.Repositories.Authentication.Interfaces;

public interface IPasswordResetTokenRepository : IBaseRepository<PasswordResetTokens, int>
{
    Task<PasswordResetTokens?> GetByTokenHashAsync(string tokenHash);
    Task<int> CountRecentTokensAsync(string email, string username, DateTime since);
    Task RevokeUserTokensAsync(int userId);
}
