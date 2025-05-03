using SalesSystem.DAL.DBContext;
using SalesSystem.DAL.Repositories.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SalesSystem.DAL.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly DbsaleContext _dbContext;
        internal DbSet<TModel> dbSet;
                    

        public GenericRepository(DbsaleContext dbContext)
        {
            _dbContext = dbContext; 
            this.dbSet = dbContext.Set<TModel>();
        }

        public async Task<TModel?> Get(Expression<Func<TModel, bool>>? filter)
        {
            try
            {

                TModel? model = filter == null ? await dbSet.FirstOrDefaultAsync() : 
                                                 await dbSet.Where(filter).FirstOrDefaultAsync();

                return model;
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<TModel> GetAll(Expression<Func<TModel, bool>>? filter = null)
        {
            try
            { 
                IQueryable<TModel> queryModel = filter == null ? dbSet :
                                                                 dbSet.Where(filter);
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
                dbSet.Add(model);
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
                dbSet.Update(model);
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
                dbSet.Remove(model);
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
