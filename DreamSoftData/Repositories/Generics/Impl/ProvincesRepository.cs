using DreamSoftData.Context;
using DreamSoftData.Entities.Generics;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Generics.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DreamSoftData.Repositories.Generics.Impl;

public class ProvincesRepository(DreamSoftDbContext dbContext)
    : ActiveGenericRepository<Provinces, int>(dbContext), IProvincesRepository
{
    public async Task<IEnumerable<Provinces>> GetByCountryCodeAsync(int countryCode)
    {
        return await DbSet.Where(p => p.CountryId == countryCode && p.Active).ToListAsync();
    }
}