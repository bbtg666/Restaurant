using Data.DBContext;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> CompleteAsync();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(RestaurantDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(_dbContext);
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                var row = await _dbContext.SaveChangesAsync();
                return row;
            }
            catch
            {
                return 0;
            }
        }

        public void Dispose()
        {
            if (_dbContext is not null)
            {
                _dbContext.Dispose();
            }
        }
    }
}
