using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Base;

namespace DreamSoftData.Repositories.Public.Interface;

public interface IUsersRepository : IGenericRepository<Users, int>
{
    Task<Users?> GetAuthUser(string username, string password);
    Task<Users?> GetByUsername(string username);

    public Task<bool> IsUserExists(string username);
}