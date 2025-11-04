using DreamSoftData.Entities.Authentication;
using DreamSoftData.Repositories.Base;

namespace DreamSoftData.Repositories.Authentication.Interfaces;

public interface IRolesRepository : IActiveGenericRepository<Roles, int>
{
}