using DreamSoftData.Context;
using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Public.Interface;
using Microsoft.EntityFrameworkCore;

namespace DreamSoftData.Repositories.Public.Impl;

public class AccountRepository(DreamSoftDbContext dbContext)
    : GenericRepository<Accounts, int>(dbContext), IAccountRepository
{
    public Task<bool> CheckEmailExistence(string email)
    {
        return DbSet.AnyAsync(a => a.Email == email);
    }

    public Task<bool> CheckAccountNumberExistence(string accountNumber)
    {
        return DbSet.AnyAsync(a => a.Email == accountNumber);
    }
}