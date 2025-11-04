using DreamSoftLogic.Services.Generics.Interfaces;
using DreamSoftModel.Models.Generics;
using DreamSoftWebApi.Controllers.Base;
using DreamSoftWebApi.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Generics;

[ApiController]
[Route("dreamsoftapi/[controller]")]
public class IdTypeController(IIdTypeServices services) : ActiveGenericControllerBase<IdType, int>(services)
{
    [HttpPost("[action]")]
    [Permission(action: PermissionAction.Create)]
    [Authorize]
    public override async Task<ActionResult<IdType>> Create([FromBody] IdType t)
    {
        return await base.Create(t);
    }

    [HttpPut("[action]")]
    [Permission(action: PermissionAction.Update)]
    [Authorize]
    public override async Task<ActionResult<IdType>> Update([FromBody] IdType t)
    {
        return await base.Update(t);
    }

    [HttpDelete("[action]")]
    [Permission(action: PermissionAction.Delete)]
    [Authorize]
    public override async Task<ActionResult<IdType>> Delete([FromBody] IdType t)
    {
        return await base.Delete(t);
    }

    [HttpGet("[action]/{id}")]
    public override async Task<ActionResult<IdType>> GetById(int id)
    {
        return await base.GetById(id);
    }

    [HttpGet("[action]")]
    public override async Task<ActionResult<List<IdType>>> GetAll()
    {
        return await base.GetAll();
    }

    [HttpGet("[action]")]
    public override async Task<ActionResult<List<IdType>>> GetAllActive()
    {
        return await base.GetAllActive();
    }

    [HttpGet("[action]")]
    public override async Task<ActionResult<List<IdType>>> GetAllInActive()
    {
        return await base.GetAllInActive();
    }
}
