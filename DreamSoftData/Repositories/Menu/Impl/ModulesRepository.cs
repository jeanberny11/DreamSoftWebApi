using DreamSoftData.Context;
using DreamSoftData.Entities.Menu;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Menu.Interfaces;

namespace DreamSoftData.Repositories.Menu.Impl;

public class ModulesRepository(DreamSoftDbContext dbContext)
    : ActiveGenericRepository<Modules, int>(dbContext), IModulesRepository
{
}