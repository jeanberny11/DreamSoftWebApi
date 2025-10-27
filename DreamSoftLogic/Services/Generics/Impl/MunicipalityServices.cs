using AutoMapper;
using DreamSoftData.Entities.Generics;
using DreamSoftData.Repositories.Generics.Interfaces;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Generics.Interfaces;
using DreamSoftModel.Models.Generics;

namespace DreamSoftLogic.Services.Generics.Impl;

public class MunicipalityServices(IMunicipalitiesRepository repository, IMapper mapper)
    : GenericServices<Municipalities, Municipality, int>(repository, mapper), IMunicipalityServices
{
}
