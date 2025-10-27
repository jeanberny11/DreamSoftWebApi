using DreamSoftData.Context;
using DreamSoftData.Entities.Authentication;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Authentication.Interfaces;

namespace DreamSoftData.Repositories.Authentication.Impl;

public class RolesRepository(DreamSoftDbContext dbContext)
    : GenericRepository<Roles, int>(dbContext), IRolesRepository
{
}