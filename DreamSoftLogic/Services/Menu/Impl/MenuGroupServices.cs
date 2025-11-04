using AutoMapper;
using DreamSoftData.Entities.Menu;
using DreamSoftData.Repositories.Menu.Interfaces;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Menu.Interfaces;
using DreamSoftModel.Models.Menu;

namespace DreamSoftLogic.Services.Menu.Impl;

public class MenuGroupServices(IMenuGroupsRepository repository, IMapper mapper)
    : ActiveGenericServices<MenuGroups, MenuGroup, int>(repository, mapper), IMenuGroupServices
{
}
