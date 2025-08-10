using AutoMapper;
using DreamSoftData.Entities.Inventory;
using DreamSoftData.Repositories.Inventory.Interface;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Inventory.Interface;
using DreamSoftModel.Models.Inventory;

namespace DreamSoftLogic.Services.Inventory.Impl;

public class ModelServices(IModelsRepository repository, IMapper mapper)
    : GenericServices<Models, Model, int>(repository, mapper), IModelServices
{
    // Add custom Model-specific business logic here if needed
}