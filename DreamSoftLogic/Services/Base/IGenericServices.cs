namespace DreamSoftLogic.Services.Base;

public interface IGenericServices<TM, in TI> where TM : class where TI : notnull
{
    Task<List<TM>> GetAllAsync();
    Task<TM?> GetByIdAsync(TI id);
    Task<TM?> CreateAsync(TM model);
    Task<TM?> UpdateAsync(TM model);
    Task<TM?> DeleteAsync(TM model);
}

public interface IActiveGenericServices<TM, in TI> : IGenericServices<TM,TI> where TM : class where TI : notnull
{
    Task<List<TM>> GetAllActiveAsync();
    Task<List<TM>> GetAllInactiveAsync();
}