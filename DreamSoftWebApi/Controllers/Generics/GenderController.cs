using DreamSoftLogic.Services.Generics.Interfaces;
using DreamSoftModel.Models.Generics;
using DreamSoftWebApi.Controllers.Base;
using DreamSoftWebApi.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Generics;

[ApiController]
[Route("dreamsoftapi/[controller]")]
public class GenderController(IGenderServices services) : ActiveGenericControllerBase<Gender, int>(services)
{
    [HttpPost("[action]")]
    [Permission(action: PermissionAction.Create)]
    [Authorize]
    public override async Task<ActionResult<Gender>> Create([FromBody] Gender t)
    {
        return await base.Create(t);
    }

    [HttpPut("[action]")]
    [Permission(action: PermissionAction.Update)]
    [Authorize]
    public override async Task<ActionResult<Gender>> Update([FromBody] Gender t)
    {
        return await base.Update(t);
    }

    [HttpDelete("[action]")]
    [Permission(action: PermissionAction.Delete)]
    [Authorize]
    public override async Task<ActionResult<Gender>> Delete([FromBody] Gender t)
    {
        return await base.Delete(t);
    }

    [HttpGet("[action]/{id}")]
    public override async Task<ActionResult<Gender>> GetById(int id)
    {
        return await base.GetById(id);
    }

    [HttpGet("[action]")]
    public override async Task<ActionResult<List<Gender>>> GetAll()
    {
        return await base.GetAll();
    }

    [HttpGet("[action]")]
    public override async Task<ActionResult<List<Gender>>> GetAllActive()
    {
        return await base.GetAllActive();
    }

    [HttpGet("[action]")]
    public override async Task<ActionResult<List<Gender>>> GetAllInActive()
    {
        return await base.GetAllInActive();
    }
}
