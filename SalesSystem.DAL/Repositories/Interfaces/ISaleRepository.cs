
using SalesSystem.Model.Entities;

namespace SalesSystem.DAL.Repositories.Interfaces
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        Task<Sale> CreateSaleAsync(Sale Sale);
    }
}
