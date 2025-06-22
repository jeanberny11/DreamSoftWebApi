using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Base;

namespace DreamSoftData.Repositories.Public.Interface;

public interface IRoleOptionsRepository : IGenericRepository<RoleOptions, int>
{
    Task<IEnumerable<RoleOptions>> GetRolePermitedOptions(int roleId);
}