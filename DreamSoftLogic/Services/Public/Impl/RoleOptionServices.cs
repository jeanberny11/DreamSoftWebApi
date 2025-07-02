using AutoMapper;
using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Public.Interface;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Public.Interface;
using DreamSoftModel.Models.Public;
using DreamSoftModel.Models.Public.Menu;

namespace DreamSoftLogic.Services.Public.Impl;

public class RoleOptionServices(IRoleOptionsRepository repository, IMapper mapper)
    : GenericServices<RoleOptions, RoleOption, int>(repository, mapper), IRoleOptionsServices
{
    private readonly IMapper _mapper = mapper;

    public async Task<List<RoleOption>> GetRolePermittedOptionsAsync(int roleid)
    {
        var result = await repository.GetRoleMenuAsync(roleid);
        return _mapper.Map<List<RoleOption>>(result);
    }

    public async Task<Menu> GetRoleMenu(int roleId)
    {
        var roleOptions = await repository.GetRoleMenuAsync(roleId);
        var menuOptions = roleOptions.Select(r => r.MenuOption);
        var groupedMenu = menuOptions
            .GroupBy(mo => mo.Module)
            .Select(moduleGroup => new MenuModule()
            {
                ModuleId = moduleGroup.Key.ModuleId,
                Name = moduleGroup.Key.Name,
                Icon = moduleGroup.Key.Icon,
                SortOrder = moduleGroup.Key.SortOrder,
                GroupMenus = moduleGroup
                    .GroupBy(mo => mo.MenuGroup)
                    .Select(menuGroup => new GroupMenu()
                    {
                        GroupId = menuGroup.Key.MenuGroupId,
                        Name = menuGroup.Key.Name,
                        Icon = menuGroup.Key.Icon,
                        SortOrder = menuGroup.Key.SortOrder,
                        Options = menuGroup.Select(mo => new OptionMenu()
                        {
                            OptionId = mo.MenuOptionId,
                            Name = mo.Name,
                            Url = mo.Url,
                            Icon = mo.Icon,
                            SortOrder = mo.SortOrder
                        }).OrderBy(mo => mo.SortOrder).ToList()
                    }).ToList()
            })
            .OrderBy(m => m.ModuleId)
            .ToList();
        return new Menu
        {
            MenuModules = groupedMenu
        };
    }
}