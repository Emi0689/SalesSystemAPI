
using SalesSystem.Model.Entities;
using SalesSystem.DAL.DBContext;

namespace SalesSystem.DAL.Repositories.Interfaces
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        Task<Sale> Create(Sale Sale);
    }
}
