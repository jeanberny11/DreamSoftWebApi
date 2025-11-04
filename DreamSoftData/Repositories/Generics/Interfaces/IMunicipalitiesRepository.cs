using DreamSoftData.Entities.Generics;
using DreamSoftData.Repositories.Base;

namespace DreamSoftData.Repositories.Generics.Interfaces;

public interface IMunicipalitiesRepository : IActiveGenericRepository<Municipalities, int>
{
    Task<IEnumerable<Municipalities>> GetByProvinceCodeAsync(int provinceCode);
}