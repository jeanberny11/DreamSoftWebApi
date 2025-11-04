using DreamSoftData.Context;
using DreamSoftData.Entities.Authentication;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Authentication.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DreamSoftData.Repositories.Authentication.Impl;

public class UsersRepository(DreamSoftDbContext dbContext)
    : ActiveGenericRepository<Users, int>(dbContext), IUsersRepository
{
    public Task<Users?> GetAuthUser(int accountId, string username)
    {
        return DbSet
            .Include(u => u.Account)
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.AccountId == accountId && u.UserName == username);
    }

    public Task<Users?> GetByUsername(string username)
    {
        return DbSet
            .Include(u => u.Account)
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserName == username);
    }

    public override Task<Users?> GetByIdAsync(int id)
    {
        return DbSet
            .Include(u => u.Account)
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == id);
    }

    public Task<bool> IsUserExists(string username)
    {
        return DbSet.AnyAsync(u => u.UserName == username);
    }
}