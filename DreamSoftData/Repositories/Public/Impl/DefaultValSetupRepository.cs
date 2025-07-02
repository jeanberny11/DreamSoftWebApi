using DreamSoftData.Context;
using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Public.Interface;

namespace DreamSoftData.Repositories.Public.Impl
{
    public class DefaultValSetupRepository(DreamSoftDbContext dbContext) : GenericRepository<DefaultValSetups, int>(dbContext), IDefaultValSetupsRepository
    {
    }
}
