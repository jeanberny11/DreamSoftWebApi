using AutoMapper;
using DreamSoftData.Entities.Generics;
using DreamSoftData.Repositories.Generics.Interfaces;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Generics.Interfaces;
using DreamSoftModel.Models.Generics;

namespace DreamSoftLogic.Services.Generics.Impl;

public class ProvinceServices(IProvincesRepository repository, IMapper mapper)
    : GenericServices<Provinces, Province, int>(repository, mapper), IProvinceServices
{
}
