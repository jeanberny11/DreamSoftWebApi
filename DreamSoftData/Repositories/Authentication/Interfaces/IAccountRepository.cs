using DreamSoftData.Entities.Authentication;
using DreamSoftData.Repositories.Base;

namespace DreamSoftData.Repositories.Authentication.Interfaces;

public interface IAccountRepository : IActiveGenericRepository<Accounts, int>
{
    public Task<bool> CheckAccountNumberExistence(string accountNumber);
    public Task<bool> CheckEmailExistence(string email);
    Task<Accounts?> GetAccountByEmail(string email);
}