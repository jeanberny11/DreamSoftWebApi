using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Base;

namespace DreamSoftData.Repositories.Public.Interface;

public interface IAccountRepository : IGenericRepository<Accounts, int>
{
    public Task<bool> CheckAccountNumberExistence(string accountNumber);
    public Task<bool> CheckEmailExistence(string email);
}