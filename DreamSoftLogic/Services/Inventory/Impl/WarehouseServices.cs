using AutoMapper;
using DreamSoftData.Entities.Inventory;
using DreamSoftData.Repositories.Inventory.Interface;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Inventory.Interface;
using DreamSoftModel.Models.Inventory;

namespace DreamSoftLogic.Services.Inventory.Impl;

public class WarehouseServices(IWarehousesRepository repository, IMapper mapper)
    : GenericServices<Warehouses, Warehouse, int>(repository, mapper), IWarehouseServices
{
    // Add custom Warehouse-specific business logic here if needed
}