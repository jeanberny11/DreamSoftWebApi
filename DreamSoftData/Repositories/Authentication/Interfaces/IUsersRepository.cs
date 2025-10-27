
using DreamSoftData.Entities.Authentication;
using DreamSoftData.Repositories.Base;

namespace DreamSoftData.Repositories.Authentication.Interfaces;

public interface IUsersRepository : IGenericRepository<Users, int>
{
    Task<Users?> GetAuthUser(int accountId, string username);
    Task<Users?> GetByUsername(string username);

    public Task<bool> IsUserExists(string username);
}