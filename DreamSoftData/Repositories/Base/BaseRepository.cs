using DreamSoftData.Context;
using DreamSoftData.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DreamSoftData.Repositories.Base;

public abstract class BaseRepository<TE, T>(DreamSoftDbContext dbContext) : IBaseRepository<TE, T>
    where TE : class, IEntity<T>
    where T : notnull
{
    protected readonly DbSet<TE> DbSet = dbContext.Set<TE>();

    /// <summary>
    /// Adds a new entity to the context and optionally saves changes to the database.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <param name="autoPersist">
    /// If set to <c>true</c> (default), the changes are immediately saved to the database by calling <see cref="SaveChangesAsync"/>.
    /// If <c>false</c>, you must call <see cref="SaveChangesAsync"/> manually to persist the changes.
    /// </param>
    /// <returns>The added entity.</returns>
    public virtual async Task<TE> CreateAsync(TE entity,bool autoPersist = true)
    {
        await DbSet.AddAsync(entity);
        if (autoPersist)
        {
            await SaveChangesAsync();
        }
        return entity;
    }

    /// <summary>
    /// Delete an existing entity in the context and optionally saves changes to the database.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="autoPersist">
    /// If set to <c>true</c> (default), the changes are immediately saved to the database by calling <see cref="SaveChangesAsync"/>.
    /// If <c>false</c>, you must call <see cref="SaveChangesAsync"/> manually to persist the changes.
    /// </param>
    /// <returns>The deleted entity.</returns>
    public virtual async Task<TE> DeleteAsync(TE entity, bool autoPersist = true)
    {
        var result = DbSet.Remove(entity);
        if (autoPersist)
        {
            await SaveChangesAsync();
        }
        return result.Entity;
    }

    /// <summary>
    /// Persist all change to the database. Must call SaveChangesAsync() to persist.
    /// </summary>
    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieve a database transaction.
    /// </summary>
    public Task<IDbContextTransaction> GetTransaction()
    {
        return dbContext.Database.BeginTransactionAsync();
    }

    /// <summary>
    /// Retrieve all entities of type TE from the database.
    /// </summary>
    public virtual async Task<IEnumerable<TE>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    /// <summary>
    /// Retrieve an entity by its ID.
    /// </summary>
    public virtual Task<TE?> GetByIdAsync(T id)
    {
        return DbSet.FindAsync(id).AsTask();
    }

    /// <summary>
    /// Update an existing entity in the context and optionally saves changes to the database.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="autoPersist">
    /// If set to <c>true</c> (default), the changes are immediately saved to the database by calling <see cref="SaveChangesAsync"/>.
    /// If <c>false</c>, you must call <see cref="SaveChangesAsync"/> manually to persist the changes.
    /// </param>
    /// <returns>The updated entity.</returns>
    public virtual async Task<TE> UpdateAsync(TE entity, bool autoPersist = true)
    {
        var result = DbSet.Update(entity);
        if (autoPersist)
        {
            await SaveChangesAsync();
        }
        return result.Entity;
    }
}

public abstract class ActiveGenericRepository<TE, T>(DreamSoftDbContext dbContext)
    : BaseRepository<TE, T>(dbContext), IActiveGenericRepository<TE, T>
    where TE : class, IEntity<T>, IActiveEntity
    where T : notnull
{
    /// <summary>
    /// Retrieve all entities filtered by the Active field.
    /// </summary>
    /// <param name="active">The value to filter by (true for active entities, false for inactive).</param>
    /// <returns>A collection of entities matching the active status.</returns>
    public virtual async Task<IEnumerable<TE>> GetByActiveAsync(bool active)
    {
        return await DbSet.Where(e => e.Active == active).ToListAsync();
    }
}