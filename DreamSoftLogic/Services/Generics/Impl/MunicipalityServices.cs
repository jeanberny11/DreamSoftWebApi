using AutoMapper;
using DreamSoftData.Entities.Generics;
using DreamSoftData.Repositories.Generics.Interfaces;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Generics.Interfaces;
using DreamSoftModel.Models.Generics;

namespace DreamSoftLogic.Services.Generics.Impl;

public class MunicipalityServices(IMunicipalitiesRepository repository, IMapper mapper)
    : ActiveGenericServices<Municipalities, Municipality, int>(repository, mapper), IMunicipalityServices
{
    private readonly IMunicipalitiesRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<Municipality>> GetAllMunicipalitiesByProvinceidAsync(int provinceid)
    {
        var res = await _repository.GetByProvinceCodeAsync(provinceid);
        return _mapper.Map<List<Municipality>>(res);
    }
}
