using DreamSoftData.Entities.Base;

namespace DreamSoftData.Repositories.Base;

public interface IGenericRepository<TE, in T> where TE : class, IEntity<T> where T : notnull
{
    Task<IEnumerable<TE>> GetAllAsync();

    Task<TE?> GetByIdAsync(T id);

    Task<TE> CreateAsync(TE entity);

    Task<TE> UpdateAsync(TE entity);

    Task<TE> DeleteAsync(TE entity);
}