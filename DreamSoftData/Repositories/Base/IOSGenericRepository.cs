using DreamSoftData.Entities.Base;

namespace DreamSoftData.Repositories.Base
{
    public interface IOSGenericRepository<TE, in T>:IGenericRepository<TE,T> where TE : class, IOSEntity<T> where T : notnull
    {
        Task<IEnumerable<TE>> GetAllByAccountIdAsync(int accountId);               
    }
}
