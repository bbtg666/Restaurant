using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IQueryable<T>>? paging = null,
            string includeProperties = "");
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
    }

    /// <summary>
    /// The generic repository, contains all general functions to interact with the database.
    /// </summary>
    /// <typeparam name="T">The entity class</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IQueryable<T>>? paging = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var includePropertiesArr = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var includeProperty in includePropertiesArr)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (paging != null)
            {
                query = paging(query);
            }

            return query.ToList();
        }

        public virtual T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual void Remove(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Attach(entity);
            }
            _context.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                if (_context.Entry(item).State == EntityState.Detached)
                {
                    _context.Attach(item);
                }
            }
            _context.Set<T>().RemoveRange(entities);
        }

        public virtual void Update(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
            }
            _context.Set<T>().Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                if (_context.Entry(item).State == EntityState.Detached)
                {
                    _context.Set<T>().Attach(item);
                }
            }
            _context.Set<T>().UpdateRange(entities);
        }
    }
}
