using DreamSoftData.Context;
using DreamSoftData.Entities.Sales;
using DreamSoftData.Repositories.Base;
using DreamSoftData.Repositories.Sales.Interface;

namespace DreamSoftData.Repositories.Sales.Impl;

public class CustomersRepository(DreamSoftDbContext dbContext)
    : BaseRepository<Customers, int>(dbContext), ICustomersRepository
{
}