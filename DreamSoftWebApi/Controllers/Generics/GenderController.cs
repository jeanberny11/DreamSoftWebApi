using DreamSoftLogic.Services.Generics.Interfaces;
using DreamSoftModel.Models.Generics;
using DreamSoftWebApi.Controllers.Base;
using DreamSoftWebApi.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Generics;

[ApiController]
[Route("dreamsoftapi/[controller]")]
[Authorize]
[Permission("2", "Generos")]
public class GenderController(IGenderServices services) : GenericControllerBase<Gender, int>(services)
{
}
