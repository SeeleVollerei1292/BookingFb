
using bookingfootball.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using bookingfootball.Interfaces;
using bookingfootball.Db_QL;
using bookingfootball.Data;


namespace bookingfootball.Persistence.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly SbDbcontext _context;
        protected readonly DbSet<T> _dbSet;

        protected RepositoryBase(SbDbcontext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        //AsNoTracking (ON) : True
        //AsNoTracking (OFF) : False

        public async Task<T> GetOrCreateAsync(Expression<Func<T, bool>> predicate, Func<T> createNew)
        {
            var entity = await FindByCondition(predicate).FirstOrDefaultAsync();
            if (entity == null)
            {
                entity = createNew();
                await AddAsync(entity);
                await SaveChangesAsync();
            }
            return entity;
        }

        public async Task<T?> GetByIdAsync(int id, bool asNoTracking = false)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity != null && asNoTracking)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }


        public async Task<T?> GetUserByIdAsync(int id, bool asNoTracking = false)
        {
            return await GetByIdAsync(id, asNoTracking);
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool asNoTracking)
        {
            var query = asNoTracking ? _context.Set<T>().AsNoTracking() : _context.Set<T>();
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false)
        {
            return await FindByCondition(predicate, asNoTracking).ToListAsync();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool asNoTracking = false)
        {
            var query = asNoTracking ? _context.Set<T>().AsNoTracking() : _context.Set<T>();
            return query.Where(expression);
        }

        public IQueryable<T> FindAll(bool asNoTracking = false)
        {
            return asNoTracking ? _context.Set<T>().AsNoTracking() : _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

