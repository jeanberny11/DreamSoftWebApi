using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Base;

public interface IGenericControllerBase<TModel, in TId> where TModel : class where TId : notnull
{
    Task<ActionResult<TModel>> GetById(TId id);
    Task<ActionResult<List<TModel>>> GetAll();
    Task<ActionResult<TModel>> Create(TModel model);
    Task<ActionResult<TModel>> Update(TModel model);
    Task<ActionResult<TModel>> Delete(TModel model);
}

public interface IActiveGenericControllerBase<TModel, in TId> : IGenericControllerBase<TModel,TId> where TModel : class where TId : notnull
{
    Task<ActionResult<List<TModel>>> GetAllActive();
    Task<ActionResult<List<TModel>>> GetAllInActive();
}