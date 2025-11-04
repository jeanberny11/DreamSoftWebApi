using DreamSoftData.Entities.Generics;
using DreamSoftData.Repositories.Base;

namespace DreamSoftData.Repositories.Generics.Interfaces;

public interface IProvincesRepository : IActiveGenericRepository<Provinces, int>
{
    Task<IEnumerable<Provinces>> GetByCountryCodeAsync(int countryCode);
}