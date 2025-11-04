using AutoMapper;
using DreamSoftData.Entities.Base;
using DreamSoftData.Repositories.Base;

namespace DreamSoftLogic.Services.Base;

public abstract class GenericServices<TE, TM, TI>(IBaseRepository<TE, TI> repository, IMapper mapper)
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

public abstract class ActiveGenericServices<TE, TM, TI>(IActiveGenericRepository<TE, TI> repository, IMapper mapper)
    : GenericServices<TE, TM, TI>(repository, mapper), IActiveGenericServices<TM, TI>
    where TE : class, IEntity<TI>, IActiveEntity
    where TM : class
    where TI : notnull
{
    private readonly IActiveGenericRepository<TE, TI> repository = repository;
    private readonly IMapper mapper = mapper;

    public async Task<List<TM>> GetAllActiveAsync()
    {
        var res = await repository.GetByActiveAsync(true);
        return mapper.Map<List<TM>>(res);
    }

    public async Task<List<TM>> GetAllInactiveAsync()
    {
        var res = await repository.GetByActiveAsync(false);
        return mapper.Map<List<TM>>(res);
    }
}