using DreamSoftData.Context;
using DreamSoftData.Entities.Menu;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Menu.Interfaces;

namespace DreamSoftData.Repositories.Menu.Impl;

public class MenuGroupsRepository(DreamSoftDbContext dbContext)
    : ActiveGenericRepository<MenuGroups, int>(dbContext), IMenuGroupsRepository
{
}