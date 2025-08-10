using DreamSoftData.Entities.Sales;
using DreamSoftData.Repositories.Base;

namespace DreamSoftData.Repositories.Sales.Interface;

public interface ICustomersRepository : IGenericRepository<Customers, int>
{
}