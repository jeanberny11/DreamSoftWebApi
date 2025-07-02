using DreamSoftData.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DreamSoftData.Repositories.Base;

public interface IGenericRepository<TE, in T> where TE : class, IEntity<T> where T : notnull
{
    Task<IEnumerable<TE>> GetAllAsync();

    Task<TE?> GetByIdAsync(T id);

    Task<TE> CreateAsync(TE entity, bool autoPersist = true);

    Task<TE> UpdateAsync(TE entity, bool autoPersist = true);

    Task<TE> DeleteAsync(TE entity, bool autoPersist = true);
    Task SaveChangesAsync();
    Task<IDbContextTransaction> GetTransaction();
}