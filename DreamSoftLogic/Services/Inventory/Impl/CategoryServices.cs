using AutoMapper;
using DreamSoftData.Entities.Inventory;
using DreamSoftData.Repositories.Inventory.Interface;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Inventory.Interface;
using DreamSoftModel.Models.Inventory;

namespace DreamSoftLogic.Services.Inventory.Impl;

public class CategoryServices(ICategoriesRepository repository, IMapper mapper)
    : GenericServices<Categories, Category, int>(repository, mapper), ICategoryServices
{
    // Add custom Category-specific business logic here if needed
}