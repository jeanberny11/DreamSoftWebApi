using DreamSoftData.Context;
using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Public.Interface;

namespace DreamSoftData.Repositories.Public.Impl;

public class CountriesRepository(DreamSoftDbContext dbContext)
    : GenericRepository<Countries, int>(dbContext), ICountriesRepository
{
}