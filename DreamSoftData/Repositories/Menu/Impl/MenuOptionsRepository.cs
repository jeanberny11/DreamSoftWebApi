using DreamSoftData.Context;
using DreamSoftData.Entities.Menu;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Menu.Interfaces;

namespace DreamSoftData.Repositories.Menu.Impl;

public class MenuOptionsRepository(DreamSoftDbContext dbContext)
    : ActiveGenericRepository<MenuOptions, int>(dbContext), IMenuOptionsRepository
{
}