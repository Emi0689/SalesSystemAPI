using System.Linq.Expressions;

namespace SalesSystem.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<TModel?> GetAsync(Expression<Func<TModel, bool>>? filter = null);
        Task<TModel> CreateAsync(TModel model);
        Task<bool> UpdateAsync(TModel model);
        Task<bool> DeleteAsync(TModel model);
        IQueryable<TModel> GetAllAsync(Expression<Func<TModel, bool>>? filter = null);
    }
}
