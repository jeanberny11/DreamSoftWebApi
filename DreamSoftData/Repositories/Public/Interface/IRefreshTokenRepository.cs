using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Base;

namespace DreamSoftData.Repositories.Public.Interface;

public interface IRefreshTokenRepository : IGenericRepository<RefreshTokens, int>
{
    Task<bool> CheckTokenExistence(string token);
    Task<RefreshTokens?> GetRefreshTokenByToken(string token);

    Task RevokeUserRefreshTokens(int userid, string ipaddress);
}