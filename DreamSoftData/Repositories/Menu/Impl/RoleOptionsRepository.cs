using DreamSoftData.Context;
using DreamSoftData.Entities.Menu;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Menu.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DreamSoftData.Repositories.Menu.Impl;

public class RoleOptionsRepository(DreamSoftDbContext dbContext)
    : BaseRepository<RoleOptions, int>(dbContext), IRoleOptionsRepository
{
    public async Task<IEnumerable<RoleOptions>> GetRolePermittedOptionsAsync(int roleId)
    {
        return await DbSet
            .Include(o => o.MenuOption)
            .Where(o => o.RoleId == roleId && o.MenuOption.Active && o.Role.Active && o.MenuOption.Module.Active && o.MenuOption.MenuGroup.Active)
            .ToListAsync();
    }

    public async Task<IEnumerable<RoleOptions>> GetRoleMenuAsync(int roleId)
    {
        return await DbSet
            .Include(o => o.Role)
            .Include(o => o.MenuOption)
            .Include(o => o.MenuOption.MenuGroup)
            .Include(o => o.MenuOption.Module)
            .Where(o => o.RoleId == roleId && o.MenuOption.Active && o.Role.Active && o.MenuOption.Module.Active &&
                        o.MenuOption.MenuGroup.Active)
            .OrderBy(o => o.MenuOption.SortOrder)
            .ToListAsync();
    }
}