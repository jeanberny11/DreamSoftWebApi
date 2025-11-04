using AutoMapper;
using DreamSoftData.Entities.Inventory;
using DreamSoftData.Repositories.Inventory.Interface;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Inventory.Interface;
using DreamSoftModel.Models.Inventory;

namespace DreamSoftLogic.Services.Inventory.Impl;

public class LocationServices(ILocationsRepository repository, IMapper mapper)
    : ActiveGenericServices<Locations, Location, int>(repository, mapper), ILocationServices
{
    // Add custom Location-specific business logic here if needed
}