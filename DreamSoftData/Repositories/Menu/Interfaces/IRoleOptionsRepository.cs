using DreamSoftData.Entities.Menu;
using DreamSoftData.Repositories.Base;

namespace DreamSoftData.Repositories.Menu.Interfaces;

public interface IRoleOptionsRepository : IGenericRepository<RoleOptions, int>
{
    Task<IEnumerable<RoleOptions>> GetRolePermittedOptionsAsync(int roleId);
    Task<IEnumerable<RoleOptions>> GetRoleMenuAsync(int roleId);
}