using AutoMapper;
using DreamSoftData.Entities.Menu;
using DreamSoftData.Repositories.Menu.Interfaces;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Menu.Interfaces;
using DreamSoftModel.Models.Menu;

namespace DreamSoftLogic.Services.Menu.Impl;

public class ModuleServices(IModulesRepository repository, IMapper mapper)
    : ActiveGenericServices<Modules, Module, int>(repository, mapper), IModuleServices
{
}
