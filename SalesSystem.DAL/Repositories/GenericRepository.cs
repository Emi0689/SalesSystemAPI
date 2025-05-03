using SalesSystem.DAL.Repositories.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SalesSystem.DAL.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private DbSet<TModel> dbSet;
                    
        public GenericRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
            dbSet = unitOfWork.GetDbSet<TModel>();
        }

        public async Task<TModel?> Get(Expression<Func<TModel, bool>>? filter = null)
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
                await _unitOfWork.CommitAsync();
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

        public async Task<bool> Delete(TModel model)
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
