using AutoMapper;
using DreamSoftData.Entities.Authentication;
using DreamSoftData.Repositories.Authentication.Interfaces;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Authentication.Interfaces;
using DreamSoftModel.Models.Authentication;

namespace DreamSoftLogic.Services.Authentication.Impl;

public class UserServices(IUsersRepository repository, IMapper mapper)
    : GenericServices<Users, User, int>(repository, mapper), IUserServices
{
}
