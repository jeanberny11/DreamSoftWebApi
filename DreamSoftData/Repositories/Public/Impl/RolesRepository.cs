using DreamSoftData.Context;
using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Public.Interface;

namespace DreamSoftData.Repositories.Public.Impl;

public class RolesRepository(DreamSoftDbContext dbContext)
    : GenericRepository<Roles, int>(dbContext), IRolesRepository
{
}