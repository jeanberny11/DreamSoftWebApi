using AutoMapper;
using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Public.Interface;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Public.Interface;
using DreamSoftModel.Models.Public;

namespace DreamSoftLogic.Services.Public.Impl;

public class MenuOptionServices(IMenuOptionsRepository repository, IMapper mapper)
    : GenericServices<MenuOptions, MenuOption, int>(repository, mapper), IMenuOptionServices
{
}