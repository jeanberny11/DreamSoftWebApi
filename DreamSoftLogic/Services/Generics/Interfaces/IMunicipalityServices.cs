using DreamSoftLogic.Services.Base;
using DreamSoftModel.Models.Generics;

namespace DreamSoftLogic.Services.Generics.Interfaces;

public interface IMunicipalityServices : IActiveGenericServices<Municipality, int>
{
    Task<List<Municipality>> GetAllMunicipalitiesByProvinceidAsync(int provinceid);
}
