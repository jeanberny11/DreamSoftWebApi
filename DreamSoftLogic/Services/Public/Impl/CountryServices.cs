using AutoMapper;
using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Public.Interface;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Public.Interface;
using DreamSoftModel.Models.Public;

namespace DreamSoftLogic.Services.Public.Impl;

public class CountryServices(ICountriesRepository repository, IMapper mapper)
    : GenericServices<Countries, Country, int>(repository, mapper), ICountryServices
{
}