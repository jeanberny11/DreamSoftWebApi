using DreamSoftData.Context;
using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Public.Interface;

namespace DreamSoftData.Repositories.Public.Impl;

public class TaxTypeRepository(DreamSoftDbContext dbContext)
    : GenericRepository<TaxType, int>(dbContext), ITaxTypeRepository
{
}