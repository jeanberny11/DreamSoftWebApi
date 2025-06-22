using DreamSoftData.Context;
using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Public.Interface;

namespace DreamSoftData.Repositories.Public.Impl;

public class MenuOptionsRepository(DreamSoftDbContext dbContext)
    : GenericRepository<MenuOptions, int>(dbContext), IMenuOptionsRepository
{
}