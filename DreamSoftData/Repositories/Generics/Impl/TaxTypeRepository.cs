using DreamSoftData.Context;
using DreamSoftData.Entities.Generics;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Generics.Interfaces;

namespace DreamSoftData.Repositories.Generics.Impl;

public class TaxTypeRepository(DreamSoftDbContext dbContext)
    : GenericRepository<TaxType, int>(dbContext), ITaxTypeRepository
{
}