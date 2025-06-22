using DreamSoftData.Context;
using DreamSoftData.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace DreamSoftData.Repositories.Base;

public abstract class GenericRepository<TE, T>(DreamSoftDbContext dbContext) : IGenericRepository<TE, T>
    where TE : class, IEntity<T>
    where T : notnull
{
    protected readonly DbSet<TE> DbSet = dbContext.Set<TE>();

    public virtual async Task<TE> CreateAsync(TE entity)
    {
        await DbSet.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TE> DeleteAsync(TE entity)
    {
        DbSet.Remove(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<IEnumerable<TE>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public virtual Task<TE?> GetByIdAsync(T id)
    {
        return DbSet.FindAsync(id).AsTask();
    }

    public virtual async Task<TE> UpdateAsync(TE entity)
    {
        DbSet.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }
}