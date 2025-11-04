using AutoMapper;
using DreamSoftData.Entities.Generics;
using DreamSoftData.Repositories.Generics.Interfaces;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Generics.Interfaces;
using DreamSoftModel.Models.Generics;

namespace DreamSoftLogic.Services.Generics.Impl;

public class IdTypeServices(IIdtypesRepository repository, IMapper mapper)
    : ActiveGenericServices<IdTypes, IdType, int>(repository, mapper), IIdTypeServices
{
}
