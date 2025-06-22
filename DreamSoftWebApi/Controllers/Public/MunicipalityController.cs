using DreamSoftLogic.Services.Public.Interface;
using DreamSoftModel.Models.Public;
using DreamSoftWebApi.Controllers.Base;
using DreamSoftWebApi.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Public;

[ApiController]
[Route("dreamsoftapi/[controller]")]
[Authorize]
[Permission("1", "Super User")]
public class MunicipalityController(IMunicipalityServices services) : GenericControllerBase<Municipality, int>(services)
{
}