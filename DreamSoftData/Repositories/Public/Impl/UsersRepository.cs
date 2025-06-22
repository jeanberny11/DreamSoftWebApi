using DreamSoftData.Context;
using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Public.Interface;
using Microsoft.EntityFrameworkCore;

namespace DreamSoftData.Repositories.Public.Impl;

public class UsersRepository(DreamSoftDbContext dbContext)
    : GenericRepository<Users, int>(dbContext), IUsersRepository
{
    public Task<Users?> GetAuthUser(string username, string password)
    {
        return DbSet
            .Include(u => u.Account)
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
    }

    public override Task<Users?> GetByIdAsync(int id)
    {
        return DbSet
            .Include(u => u.Account)
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == id);
    }
}