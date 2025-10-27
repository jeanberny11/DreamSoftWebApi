using DreamSoftLogic.Services.Base;
using DreamSoftModel.Models.Authentication;

namespace DreamSoftLogic.Services.Authentication.Interfaces;

public interface IAccountServices : IGenericServices<Account, int>
{
    Task<Account> CreateNewAccount(AccountCreate account);
}
