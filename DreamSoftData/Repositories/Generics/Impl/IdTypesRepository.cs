using DreamSoftData.Context;
using DreamSoftData.Entities.Generics;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Generics.Interfaces;

namespace DreamSoftData.Repositories.Generics.Impl
{
    public class IdTypesRepository(DreamSoftDbContext dbContext) : ActiveGenericRepository<IdTypes, int>(dbContext), IIdtypesRepository
    {
    }
}
