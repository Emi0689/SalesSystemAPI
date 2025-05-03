using SalesSystem.DAL.DBContext;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SalesSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Model.Entities;

namespace SalesSystem.DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
        ValueTask<EntityEntry> AddAsync(object model);
        IDbContextTransaction BeginTransaction();
        void RollbackTransaction();
        void CommitTransaction();
        IGenericRepository<T> GetGenRepo<T>() where T : class;
        void Update(object entity);
        ISaleRepository SaleRepository { get; }
        DbSet<TModel> GetDbSet<TModel>() where TModel : class;
    }

    public class UnitOfWork : IUnitOfWork
    {
        public readonly DbsaleContext _dbContext;
        private ISaleRepository? _saleRepository;
        private readonly Dictionary<Type, object> _typedGenericRepository = [];

        public UnitOfWork(DbsaleContext db)
        {
            _dbContext = db;
        }

        public ISaleRepository SaleRepository =>
            _saleRepository ??= new SaleRepository(this);

        public IGenericRepository<T> GetGenRepo<T>() where T : class
        {
            var type = typeof(T);
            if (_typedGenericRepository.TryGetValue(type, out var repo))
                return (IGenericRepository<T>)repo;

            var repoInstance = new GenericRepository<T>(this);
            _typedGenericRepository[type] = repoInstance;

            return repoInstance;
        }

        public void Update(object entity) => _dbContext.Update(entity);

        public DbSet<TModel> GetDbSet<TModel>() where TModel : class
        {
            return _dbContext.Set<TModel>(); 
        }

        public Task<int> CommitAsync() => _dbContext.SaveChangesAsync();

        public ValueTask<EntityEntry> AddAsync(object model) => _dbContext.AddAsync(model);

        public IDbContextTransaction BeginTransaction() => _dbContext.Database.BeginTransaction();

        public void CommitTransaction() => _dbContext.Database.CommitTransaction();

        public void RollbackTransaction() => _dbContext.Database.RollbackTransaction();

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
