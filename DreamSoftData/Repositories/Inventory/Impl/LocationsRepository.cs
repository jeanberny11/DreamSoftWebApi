using DreamSoftData.Context;
using DreamSoftData.Entities.Inventory;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Inventory.Interface;

namespace DreamSoftData.Repositories.Inventory.Impl;

public class LocationsRepository(DreamSoftDbContext dbContext)
    : ActiveGenericRepository<Locations, int>(dbContext), ILocationsRepository
{
}