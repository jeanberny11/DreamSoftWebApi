using DreamSoftLogic.Services.Public.Interface;
using DreamSoftModel.Models.Public;
using DreamSoftModel.Models.Public.Menu;
using DreamSoftWebApi.Controllers.Base;
using DreamSoftWebApi.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Public;

[ApiController]
[Route("dreamsoftapi/[controller]")]
[Authorize]
[Permission("1", "Super User")]
public class RoleOptionController(IRoleOptionsServices services) : GenericControllerBase<RoleOption, int>(services)
{
    [HttpGet("[action]/{roleid}")]
    [Permission(action: PermissionAction.Read)]
    public Task<List<RoleOption>> GetRolePermittedOptions(int roleid)
    {
        return services.GetRolePermittedOptionsAsync(roleid);
    }

    [HttpGet("[action]/{roleid}")]
    [Permission(action: PermissionAction.Read)]
    public Task<Menu> GetRoleMenu(int roleid)
    {
        return services.GetRoleMenu(roleid);
    }
}