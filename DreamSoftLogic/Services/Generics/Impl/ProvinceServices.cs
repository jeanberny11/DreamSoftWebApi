using AutoMapper;
using DreamSoftData.Entities.Generics;
using DreamSoftData.Repositories.Generics.Interfaces;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Generics.Interfaces;
using DreamSoftModel.Models.Generics;

namespace DreamSoftLogic.Services.Generics.Impl;

public class ProvinceServices(IProvincesRepository repository, IMapper mapper) : ActiveGenericServices<Provinces, Province, int>(repository, mapper), IProvinceServices
{
    private readonly IProvincesRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<Province>> GetAllProvincesByCountryidAsync(int countryid)
    {
        var res = await _repository.GetByCountryCodeAsync(countryid);
        return _mapper.Map<List<Province>>(res);
    }
}
