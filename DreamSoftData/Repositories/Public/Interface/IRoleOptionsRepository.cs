using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Base;

namespace DreamSoftData.Repositories.Public.Interface;

public interface IRoleOptionsRepository : IGenericRepository<RoleOptions, int>
{
    Task<IEnumerable<RoleOptions>> GetRolePermittedOptionsAsync(int roleId);
    Task<IEnumerable<RoleOptions>> GetRoleMenuAsync(int roleId);
}