using AutoMapper;
using DreamSoftData.Entities.Generics;
using DreamSoftData.Repositories.Generics.Interfaces;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Generics.Interfaces;
using DreamSoftModel.Models.Generics;

namespace DreamSoftLogic.Services.Generics.Impl;

public class CountryServices(ICountriesRepository repository, IMapper mapper)
    : ActiveGenericServices<Countries, Country, int>(repository, mapper), ICountryServices
{
}
