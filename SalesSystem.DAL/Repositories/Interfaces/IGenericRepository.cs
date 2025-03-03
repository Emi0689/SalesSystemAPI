using System.Linq.Expressions;

namespace SalesSystem.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<TModel> Get(Expression<Func<TModel, bool>> filter);
        Task<TModel> Create(TModel model);
        Task<bool> Update(TModel model);
        Task<bool> Delete(TModel model);
        Task<IQueryable<TModel>> GetAll(Expression<Func<TModel, bool>> filter = null);
    }
}
