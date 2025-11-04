using DreamSoftData.Entities.Inventory;
using DreamSoftData.Repositories.Base;

namespace DreamSoftData.Repositories.Inventory.Interface;

public interface IProductsRepository : IActiveGenericRepository<Products, int>
{
}