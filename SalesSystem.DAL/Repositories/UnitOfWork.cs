using SalesSystem.DAL.DBContext;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SalesSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SalesSystem.DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync();
        ValueTask<EntityEntry> AddAsync(object model);
        IDbContextTransaction BeginTransaction();
        void RollbackTransaction();
        void CommitTransaction();
        IGenericRepository<T> GetRepository<T>() where T : class;
        ISaleRepository SaleRepository { get; } 
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
            _saleRepository ??= new SaleRepository(_dbContext);

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (_typedGenericRepository.TryGetValue(type, out var repo))
                return (IGenericRepository<T>)repo;

            var repoInstance = new GenericRepository<T>(_dbContext);
            _typedGenericRepository[type] = repoInstance;

            return repoInstance;
        }
        


        public void Commit() => _dbContext.SaveChanges();

        public Task CommitAsync() => _dbContext.SaveChangesAsync();

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
