using DreamSoftData.Context;
using DreamSoftData.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace DreamSoftData.Repositories.Base
{
    public abstract class OSGenericRepository<TE, T>(DreamSoftDbContext dbContext) : BaseRepository<TE, T>(dbContext), IOSGenericRepository<TE, T>
   where TE : class, IOSEntity<T>
   where T : notnull
    {
        /// <summary>
        /// Retrieve all entities of type TE from the database by its AccountIds.
        /// </summary>
        /// <param name="accountId">The id of the account to be filtered.</param>               
        /// <returns>A list of TE entities.</returns>
        public async Task<IEnumerable<TE>> GetAllByAccountIdAsync(int accountId)
        {
            return await DbSet
                .Where(e => e.AccountId == accountId).ToListAsync();
        }
    }
}
