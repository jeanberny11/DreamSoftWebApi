using DreamSoftData.Context;
using DreamSoftData.Entities.Inventory;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Inventory.Interface;

namespace DreamSoftData.Repositories.Inventory.Impl;

public class ProductsRepository(DreamSoftDbContext dbContext)
    : GenericRepository<Products, int>(dbContext), IProductsRepository
{
}