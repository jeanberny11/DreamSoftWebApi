namespace DreamSoftLogic.Services.Base;

public interface IGenericServices<TM, in TI> where TM : class where TI : notnull
{
    Task<List<TM>> GetAllAsync();
    Task<TM?> GetByIdAsync(TI id);
    Task<TM?> CreateAsync(TM model);
    Task<TM?> UpdateAsync(TM model);
    Task<TM?> DeleteAsync(TM model);
}