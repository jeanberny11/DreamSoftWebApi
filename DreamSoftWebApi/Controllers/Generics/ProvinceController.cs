using DreamSoftLogic.Services.Generics.Interfaces;
using DreamSoftModel.Models.Generics;
using DreamSoftWebApi.Controllers.Base;
using DreamSoftWebApi.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Generics;

[ApiController]
[Route("dreamsoftapi/[controller]")]
public class ProvinceController(IProvinceServices services) : ActiveGenericControllerBase<Province, int>(services)
{
    private readonly IProvinceServices _provinceServices = services;

    [HttpPost("[action]")]
    [Permission(action: PermissionAction.Create)]
    [Authorize]
    public override async Task<ActionResult<Province>> Create([FromBody] Province t)
    {
        return await base.Create(t);
    }

    [HttpPut("[action]")]
    [Permission(action: PermissionAction.Update)]
    [Authorize]
    public override async Task<ActionResult<Province>> Update([FromBody] Province t)
    {
        return await base.Update(t);
    }

    [HttpDelete("[action]")]
    [Permission(action: PermissionAction.Delete)]
    [Authorize]
    public override async Task<ActionResult<Province>> Delete([FromBody] Province t)
    {
        return await base.Delete(t);
    }

    [HttpGet("[action]/{id}")]
    public override async Task<ActionResult<Province>> GetById(int id)
    {
        return await base.GetById(id);
    }

    [HttpGet("[action]")]
    public override async Task<ActionResult<List<Province>>> GetAll()
    {
        return await base.GetAll();
    }

    [HttpGet("[action]")]
    public override async Task<ActionResult<List<Province>>> GetAllActive()
    {
        return await base.GetAllActive();
    }

    [HttpGet("[action]")]
    public override async Task<ActionResult<List<Province>>> GetAllInActive()
    {
        return await base.GetAllInActive();
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<List<Province>>> GetProvincesByCountryId(int countryId)
    {
        var provinces = await _provinceServices.GetAllProvincesByCountryidAsync(countryId);
        return Ok(provinces);
    }
}
