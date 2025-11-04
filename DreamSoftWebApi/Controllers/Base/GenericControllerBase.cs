using DreamSoftLogic.Services.Base;
using DreamSoftWebApi.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Base;

[ApiController]
[Route("dreamsoftapi/[controller]")]
public abstract class GenericControllerBase<TModel, TId>(IGenericServices<TModel, TId> services)
    : ControllerBase, IGenericControllerBase<TModel, TId> where TModel : class where TId : notnull
{
    [HttpGet("[action]/{id}")]
    [Permission(action: PermissionAction.Read)]
    public virtual async Task<ActionResult<TModel>> GetById(TId id)
    {
        return await services.GetByIdAsync(id) ??
               throw new KeyNotFoundException($"No se encontro departamento con el id {id}");
    }

    [HttpGet("[action]")]
    [Permission(action: PermissionAction.Read)]
    public virtual async Task<ActionResult<List<TModel>>> GetAll()
    {
        return await services.GetAllAsync();
    }

    [HttpPost("[action]")]
    [Permission(action: PermissionAction.Create)]
    public virtual async Task<ActionResult<TModel>> Create([FromBody] TModel t)
    {
        var res = await services.CreateAsync(t);
        if (res != null) return res;

        throw new InvalidOperationException("An error occurs saving the data");
    }

    [HttpPut("[action]")]
    [Permission(action: PermissionAction.Update)]
    public virtual async Task<ActionResult<TModel>> Update([FromBody] TModel t)
    {
        var res = await services.UpdateAsync(t);
        if (res != null) return res;

        throw new InvalidOperationException("An error occurs updating the data");
    }

    [HttpDelete("[action]")]
    [Permission(action: PermissionAction.Delete)]
    public virtual async Task<ActionResult<TModel>> Delete([FromBody] TModel t)
    {
        var res = await services.DeleteAsync(t);
        if (res != null) return res;

        throw new InvalidOperationException("An error occurs deleting the data");
    }
}

[ApiController]
[Route("dreamsoftapi/[controller]")]
public abstract class ActiveGenericControllerBase<TModel, TId>(IActiveGenericServices<TModel, TId> services)
    : GenericControllerBase<TModel,TId>(services), IActiveGenericControllerBase<TModel, TId> where TModel : class where TId : notnull
{
    [HttpGet("[action]")]
    [Permission(action: PermissionAction.Read)]
    public virtual async Task<ActionResult<List<TModel>>> GetAllActive()
    {
        return await services.GetAllActiveAsync();
    }

    [HttpGet("[action]")]
    [Permission(action: PermissionAction.Read)]
    public virtual async Task<ActionResult<List<TModel>>> GetAllInActive()
    {
        return await services.GetAllActiveAsync();
    }
}