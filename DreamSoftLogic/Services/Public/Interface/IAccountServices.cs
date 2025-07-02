using DreamSoftLogic.Services.Base;
using DreamSoftModel.Models.Public;

namespace DreamSoftLogic.Services.Public.Interface;

public interface IAccountServices : IGenericServices<Account, int>
{
    Task<Account> CreateNewAccount(AccountCreate account);
}