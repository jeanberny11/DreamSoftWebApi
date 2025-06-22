using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Base;

namespace DreamSoftData.Repositories.Public.Interface;

public interface IUsersRepository : IGenericRepository<Users, int>
{
    Task<Users?> GetAuthUser(string username, string password);
}