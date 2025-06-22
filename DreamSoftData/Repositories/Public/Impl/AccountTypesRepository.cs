using DreamSoftData.Context;
using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Public.Interface;

namespace DreamSoftData.Repositories.Public.Impl;

public class AccountTypesRepository(DreamSoftDbContext dbContext)
    : GenericRepository<AccountTypes, int>(dbContext), IAccountTypesRepository
{
}