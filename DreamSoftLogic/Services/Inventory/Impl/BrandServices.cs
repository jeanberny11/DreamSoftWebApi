using AutoMapper;
using DreamSoftData.Entities.Inventory;
using DreamSoftData.Repositories.Inventory.Interface;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Inventory.Interface;
using DreamSoftModel.Models.Inventory;

namespace DreamSoftLogic.Services.Inventory.Impl;

public class BrandServices(IBrandsRepository repository, IMapper mapper)
    : GenericServices<Brands, Brand, int>(repository, mapper), IBrandServices
{
    // Add custom Brand-specific business logic here if needed
}