using DreamSoftLogic.Services.Generics.Interfaces;
using DreamSoftModel.Models.Generics;
using DreamSoftWebApi.Controllers.Base;
using DreamSoftWebApi.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Generics;

[ApiController]
[Route("dreamsoftapi/[controller]")]
public class CountryController(ICountryServices services) : ActiveGenericControllerBase<Country, int>(services)
{
    [HttpPost("[action]")]
    [Permission(action: PermissionAction.Create)]
    [Authorize]
    public override async Task<ActionResult<Country>> Create([FromBody] Country t)
    {
        return await base.Create(t);
    }

    [HttpPut("[action]")]
    [Permission(action: PermissionAction.Update)]
    [Authorize]
    public override async Task<ActionResult<Country>> Update([FromBody] Country t)
    {
        return await base.Update(t);
    }

    [HttpDelete("[action]")]
    [Permission(action: PermissionAction.Delete)]
    [Authorize]
    public override async Task<ActionResult<Country>> Delete([FromBody] Country t)
    {
        return await base.Delete(t);
    }

    [HttpGet("[action]/{id}")]
    public override async Task<ActionResult<Country>> GetById(int id)
    {
        return await base.GetById(id);
    }

    [HttpGet("[action]")]
    public override async Task<ActionResult<List<Country>>> GetAll()
    {
        return await base.GetAll();
    }

    [HttpGet("[action]")]
    public override async Task<ActionResult<List<Country>>> GetAllActive()
    {
        return await base.GetAllActive();
    }

    [HttpGet("[action]")]
    public override async Task<ActionResult<List<Country>>> GetAllInActive()
    {
        return await base.GetAllInActive();
    }
}
