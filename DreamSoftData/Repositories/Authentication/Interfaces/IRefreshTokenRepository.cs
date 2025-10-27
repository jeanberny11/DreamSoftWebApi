using DreamSoftData.Entities.Authentication;
using DreamSoftData.Repositories.Base;

namespace DreamSoftData.Repositories.Authentication.Interfaces;

public interface IRefreshTokenRepository : IGenericRepository<RefreshTokens, int>
{
    Task<bool> CheckTokenExistence(string token);
    Task<RefreshTokens?> GetRefreshTokenByToken(string token);

    Task RevokeUserRefreshTokens(int userid, string ipaddress);
}