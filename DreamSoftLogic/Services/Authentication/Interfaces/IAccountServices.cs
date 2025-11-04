using DreamSoftLogic.Services.Base;
using DreamSoftModel.Models.Authentication;

namespace DreamSoftLogic.Services.Authentication.Interfaces;

public interface IAccountServices : IActiveGenericServices<Account, int>
{
    Task<Account> CreateNewAccount(AccountCreate account);
    Task<bool> CheckEmailExistence(string email);
}
