using DreamSoftLogic.Services.Base;
using DreamSoftModel.Models.Generics;

namespace DreamSoftLogic.Services.Generics.Interfaces;

public interface IProvinceServices : IActiveGenericServices<Province, int>
{
    Task<List<Province>> GetAllProvincesByCountryidAsync(int countryid);
}
