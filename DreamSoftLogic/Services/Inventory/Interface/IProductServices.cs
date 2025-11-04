using DreamSoftLogic.Services.Base;
using DreamSoftModel.Models.Inventory;

namespace DreamSoftLogic.Services.Inventory.Interface;

public interface IProductServices : IActiveGenericServices<Product, int>
{
    // Add custom Product-specific service methods here if needed
}