using DreamSoftData.Context;
using DreamSoftData.Entities.Generics;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Generics.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DreamSoftData.Repositories.Generics.Impl;

public class MunicipalitiesRepository(DreamSoftDbContext dbContext)
    : ActiveGenericRepository<Municipalities, int>(dbContext), IMunicipalitiesRepository
{
    public async Task<IEnumerable<Municipalities>> GetByProvinceCodeAsync(int provinceCode)
    {
        return await DbSet.Where(m => m.ProvinceId == provinceCode && m.Active).ToListAsync();
    }
}