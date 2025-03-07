using SalesSystem.DAL.DBContext;
using SalesSystem.DAL.Repositories.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SalesSystem.DAL.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly DbsaleContext _dbContext;

        public GenericRepository(DbsaleContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TModel> Get(Expression<Func<TModel, bool>> filter)
        {
            try
            {
                TModel model = await _dbContext.Set<TModel>().FirstOrDefaultAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<TModel>> GetAll(Expression<Func<TModel, bool>> filter = null)
        {
            try
            {
                IQueryable<TModel> queryModel = filter == null ? _dbContext.Set<TModel>() : _dbContext.Set<TModel>().Where(filter);
                return queryModel;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TModel> Create(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Add(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Update(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Update(model);
                var result = await _dbContext.SaveChangesAsync();
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

        public async Task<bool> Delete(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Remove(model);
                var result = await _dbContext.SaveChangesAsync();
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
