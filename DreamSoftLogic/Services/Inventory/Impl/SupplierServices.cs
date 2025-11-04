using AutoMapper;
using DreamSoftData.Entities.Inventory;
using DreamSoftData.Repositories.Inventory.Interface;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Inventory.Interface;
using DreamSoftModel.Models.Inventory;

namespace DreamSoftLogic.Services.Inventory.Impl;

public class SupplierServices(ISuppliersRepository repository, IMapper mapper)
    : ActiveGenericServices<Suppliers, Supplier, int>(repository, mapper), ISupplierServices
{
    // Add custom Supplier-specific business logic here if needed
}