using DreamSoftLogic.Services.Base;
using DreamSoftModel.Models.Menu;

namespace DreamSoftLogic.Services.Menu.Interfaces;

public interface IRoleOptionsServices : IGenericServices<RoleOption, int>
{
    Task<List<RoleOption>> GetRolePermittedOptionsAsync(int roleid);

    Task<DreamSoftModel.Models.Menu.Menu.Menu> GetRoleMenu(int roleId);
}
