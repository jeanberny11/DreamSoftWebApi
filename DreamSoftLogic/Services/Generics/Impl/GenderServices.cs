using AutoMapper;
using DreamSoftData.Entities.Generics;
using DreamSoftData.Repositories.Generics.Interfaces;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Generics.Interfaces;
using DreamSoftModel.Models.Generics;

namespace DreamSoftLogic.Services.Generics.Impl;

public class GenderServices(IGendersRepository repository, IMapper mapper)
    : ActiveGenericServices<Genders, Gender, int>(repository, mapper), IGenderServices
{
}
