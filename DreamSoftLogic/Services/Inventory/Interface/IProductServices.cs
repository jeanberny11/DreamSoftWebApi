using DreamSoftLogic.Services.Base;
using DreamSoftModel.Models.Inventory;

namespace DreamSoftLogic.Services.Inventory.Interface;

public interface IProductServices : IGenericServices<Product, int>
{
    // Add custom Product-specific service methods here if needed
}