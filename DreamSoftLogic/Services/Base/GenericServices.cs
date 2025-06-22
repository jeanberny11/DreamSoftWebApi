using AutoMapper;
using DreamSoftData.Entities.Base;
using DreamSoftData.Repositories.Base;

namespace DreamSoftLogic.Services.Base;

public abstract class GenericServices<TE, TM, TI>(IGenericRepository<TE, TI> repository, IMapper mapper)
    : IGenericServices<TM, TI>
    where TE : class, IEntity<TI>
    where TM : class
    where TI : notnull
{
    public async Task<TM?> CreateAsync(TM model)
    {
        var entity = mapper.Map<TE>(model);
        var res = await repository.CreateAsync(entity);
        return mapper.Map<TM>(res);
    }

    public async Task<List<TM>> GetAllAsync()
    {
        var res = await repository.GetAllAsync();
        return mapper.Map<List<TM>>(res);
    }

    public async Task<TM?> GetByIdAsync(TI id)
    {
        var res = await repository.GetByIdAsync(id);
        return res != null ? mapper.Map<TM>(res) : null;
    }

    public async Task<TM?> UpdateAsync(TM model)
    {
        var entity = mapper.Map<TE>(model);
        var res = await repository.UpdateAsync(entity);
        return mapper.Map<TM>(res);
    }

    public async Task<TM?> DeleteAsync(TM model)
    {
        var entity = mapper.Map<TE>(model);
        var res = await repository.DeleteAsync(entity);
        return mapper.Map<TM>(res);
    }
}