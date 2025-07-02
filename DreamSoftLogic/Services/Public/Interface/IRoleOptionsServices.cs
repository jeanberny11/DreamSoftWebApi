using DreamSoftLogic.Services.Base;
using DreamSoftModel.Models.Public;
using DreamSoftModel.Models.Public.Menu;

namespace DreamSoftLogic.Services.Public.Interface;

public interface IRoleOptionsServices : IGenericServices<RoleOption, int>
{
    Task<List<RoleOption>> GetRolePermittedOptionsAsync(int roleid);

    Task<Menu> GetRoleMenu(int roleId);
}