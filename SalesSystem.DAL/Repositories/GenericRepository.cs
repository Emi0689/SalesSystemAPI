using SalesSystem.DAL.Repositories.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace SalesSystem.DAL.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<TModel> dbSet;
                    
        public GenericRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
            dbSet = unitOfWork.GetDbSet<TModel>();
        }

        public async Task<TModel?> GetSingleAsync(
                                    Expression<Func<TModel, bool>>? whereCondition = null,
                                    List<Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>>? includes = null,
                                    bool asNoTracking = false)
        {
            try
            {
                return await GetQuery(whereCondition, includes, null, asNoTracking, null, null).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> GetCountAsync(
                    Expression<Func<TModel, bool>>? whereCondition = null,
                    List<Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>>? includes = null,
                    Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
                    bool asNoTracking = false,
                    int? skip = null,
                    int? take = null)
        {
            return await GetQuery(whereCondition, includes, orderBy, asNoTracking, skip, take).CountAsync();
        }

        public async Task<List<TModel>> GetAllAsync(
                            Expression<Func<TModel, bool>>? whereCondition = null,
                            List<Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>>? includes = null,
                            Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
                            bool asNoTracking = false,
                            int? skip = null,
                            int? take = null)
        {
            return await GetQuery(whereCondition, includes, orderBy, asNoTracking, skip, take).ToListAsync();
        }

        public IQueryable<TModel> GetQuery(
                            Expression<Func<TModel, bool>>? whereCondition = null,
                            List<Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>>? includes = null,
                            Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
                            bool asNoTracking = false, 
                            int? skip = null, 
                            int? take = null)
        {
            IQueryable<TModel> query = dbSet;

            if (whereCondition != null)
            {
                query = query.Where(whereCondition);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = include(query);
                }
            }

            if (orderBy != null)
            {
                orderBy(query);
            }

            if (skip != null && skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null && take.HasValue)
            {
                query = query.Take(take.Value);
            }

            if (asNoTracking)
            {
                query.AsNoTracking();
            }

            return query;
        }

        public async Task<TModel> CreateAsync(TModel model)
        {
            try
            {
                dbSet.Add(model);
                await _unitOfWork.CommitAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(TModel model)
        {
            try
            {
                dbSet.Update(model);
                var result = await _unitOfWork.CommitAsync(); ;
                if (result > 0)
                    return true;
                else 
                    return false;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(TModel model)
        {
            try
            {
                dbSet.Remove(model);
                var result = await _unitOfWork.CommitAsync();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }


    }
}
