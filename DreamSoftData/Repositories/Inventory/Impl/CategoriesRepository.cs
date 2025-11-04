using DreamSoftData.Context;
using DreamSoftData.Entities.Inventory;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Inventory.Interface;

namespace DreamSoftData.Repositories.Inventory.Impl;

public class CategoriesRepository(DreamSoftDbContext dbContext)
    : ActiveGenericRepository<Categories, int>(dbContext), ICategoriesRepository
{
}