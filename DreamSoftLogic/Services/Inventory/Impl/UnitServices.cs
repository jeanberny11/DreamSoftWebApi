using AutoMapper;
using DreamSoftData.Entities.Inventory;
using DreamSoftData.Repositories.Inventory.Interface;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Inventory.Interface;
using DreamSoftModel.Models.Inventory;

namespace DreamSoftLogic.Services.Inventory.Impl;

public class UnitServices(IUnitsRepository repository, IMapper mapper)
    : ActiveGenericServices<Units, Unit, int>(repository, mapper), IUnitServices
{
    // Add custom Unit-specific business logic here if needed
}