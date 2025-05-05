using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace SalesSystem.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<TModel?> GetSingleAsync(
                    Expression<Func<TModel, bool>>? whereCondition = null,
                    List<Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>>? includes = null,
                    bool asNoTracking = false);

        IQueryable<TModel> GetQuery(
                        Expression<Func<TModel, bool>>? whereCondition = null,
                        List<Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>>? includes = null,
                        Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
                        bool asNoTracking = false,
                        int? skip = null,
                        int? take = null);

      Task<List<TModel>> GetAllAsync(
                    Expression<Func<TModel, bool>>? whereCondition = null,
                    List<Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>>? includes = null,
                    Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
                    bool asNoTracking = false,
                    int? skip = null,
                    int? take = null);

    Task<int> GetCountAsync(
                Expression<Func<TModel, bool>>? whereCondition = null,
                List<Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>>? includes = null,
                Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
                bool asNoTracking = false,
                int? skip = null,
                int? take = null);
        Task<TModel> CreateAsync(TModel model);
        Task<bool> UpdateAsync(TModel model);
        Task<bool> DeleteAsync(TModel model);

    }
}
