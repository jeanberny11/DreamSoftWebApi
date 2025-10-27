using DreamSoftLogic.Services.Authentication.Interfaces;
using DreamSoftModel.Models.Authentication;
using DreamSoftWebApi.Controllers.Base;
using DreamSoftWebApi.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Authentication;

[ApiController]
[Route("dreamsoftapi/[controller]")]
[Authorize]
[Permission("1", "Super User")]
public class RoleController(IRoleServices services) : GenericControllerBase<Role, int>(services)
{
}
