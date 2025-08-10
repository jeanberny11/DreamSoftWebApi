using AutoMapper;
using DreamSoftData.Entities.Inventory;
using DreamSoftData.Repositories.Inventory.Interface;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Inventory.Interface;
using DreamSoftModel.Models.Inventory;

namespace DreamSoftLogic.Services.Inventory.Impl;

public class ProductServices(IProductsRepository repository, IMapper mapper)
    : GenericServices<Products, Product, int>(repository, mapper), IProductServices
{
    // Add custom Product-specific business logic here if needed
}