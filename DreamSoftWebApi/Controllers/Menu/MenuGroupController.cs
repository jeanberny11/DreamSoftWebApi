using DreamSoftLogic.Services.Menu.Interfaces;
using DreamSoftModel.Models.Menu;
using DreamSoftWebApi.Controllers.Base;
using DreamSoftWebApi.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Menu;

[ApiController]
[Route("dreamsoftapi/[controller]")]
[Authorize]
[Permission("1", "Super User")]
public class MenuGroupController(IMenuGroupServices services) : ActiveGenericControllerBase<MenuGroup, int>(services)
{
}
