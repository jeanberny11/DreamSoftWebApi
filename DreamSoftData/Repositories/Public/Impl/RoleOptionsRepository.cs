using DreamSoftData.Context;
using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Public.Interface;
using Microsoft.EntityFrameworkCore;

namespace DreamSoftData.Repositories.Public.Impl;

public class RoleOptionsRepository(DreamSoftDbContext dbContext)
    : GenericRepository<RoleOptions, int>(dbContext), IRoleOptionsRepository
{
    public async Task<IEnumerable<RoleOptions>> GetRolePermitedOptions(int roleId)
    {
        return await DbSet
            .Include(o => o.Role)
            .Include(o => o.MenuOption)
            .Where(o => o.RoleId == roleId && o.MenuOption.Active && o.Role.Active).ToListAsync();
    }
}