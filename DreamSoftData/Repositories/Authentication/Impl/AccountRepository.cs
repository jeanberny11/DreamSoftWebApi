using DreamSoftData.Context;
using DreamSoftData.Entities.Authentication;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Authentication.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DreamSoftData.Repositories.Authentication.Impl;

public class AccountRepository(DreamSoftDbContext dbContext)
    : ActiveGenericRepository<Accounts, int>(dbContext), IAccountRepository
{
    public Task<bool> CheckEmailExistence(string email)
    {
        return DbSet.AnyAsync(a => a.Email == email);
    }

    public Task<bool> CheckAccountNumberExistence(string accountNumber)
    {
        return DbSet.AnyAsync(a => a.Email == accountNumber);
    }
    public Task<Accounts?> GetAccountByEmail(string email)
    {
        return DbSet.FirstOrDefaultAsync(a => a.Email == email);
    }
}