using DreamSoftLogic.Services.Generics.Interfaces;
using DreamSoftModel.Models.Generics;
using DreamSoftWebApi.Controllers.Base;
using DreamSoftWebApi.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Generics;

[ApiController]
[Route("dreamsoftapi/[controller]")]
public class MunicipalityController(IMunicipalityServices services) : ActiveGenericControllerBase<Municipality, int>(services)
{
    private readonly IMunicipalityServices _municipalityServices = services;

    [HttpPost("[action]")]
    [Permission(action: PermissionAction.Create)]
    [Authorize]
    public override async Task<ActionResult<Municipality>> Create([FromBody] Municipality t)
    {
        return await base.Create(t);
    }

    [HttpPut("[action]")]
    [Permission(action: PermissionAction.Update)]
    [Authorize]
    public override async Task<ActionResult<Municipality>> Update([FromBody] Municipality t)
    {
        return await base.Update(t);
    }

    [HttpDelete("[action]")]
    [Permission(action: PermissionAction.Delete)]
    [Authorize]
    public override async Task<ActionResult<Municipality>> Delete([FromBody] Municipality t)
    {
        return await base.Delete(t);
    }

    [HttpGet("[action]/{id}")]
    public override async Task<ActionResult<Municipality>> GetById(int id)
    {
        return await base.GetById(id);
    }

    [HttpGet("[action]")]
    public override async Task<ActionResult<List<Municipality>>> GetAll()
    {
        return await base.GetAll();
    }

    [HttpGet("[action]")]
    public override async Task<ActionResult<List<Municipality>>> GetAllActive()
    {
        return await base.GetAllActive();
    }

    [HttpGet("[action]")]
    public override async Task<ActionResult<List<Municipality>>> GetAllInActive()
    {
        return await base.GetAllInActive();
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<List<Municipality>>> GetMunicipalitiesByProvinceId(int provinceId)
    {
        var municipalities = await _municipalityServices.GetAllMunicipalitiesByProvinceidAsync(provinceId);
        return Ok(municipalities);
    }
}
