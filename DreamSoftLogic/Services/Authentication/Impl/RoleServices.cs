using AutoMapper;
using DreamSoftData.Entities.Authentication;
using DreamSoftData.Repositories.Authentication.Interfaces;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Authentication.Interfaces;
using DreamSoftModel.Models.Authentication;

namespace DreamSoftLogic.Services.Authentication.Impl;

public class RoleServices(IRolesRepository repository, IMapper mapper)
    : ActiveGenericServices<Roles, Role, int>(repository, mapper), IRoleServices
{
}
