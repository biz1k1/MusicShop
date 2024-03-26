using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MusicShop.Infrastructure.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> GetById(int id) {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task <IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression).AsNoTracking();
        }
        public T FindByCondition(T entity) {
            return _dbContext.Set<T>().FirstOrDefault(entity);
        }

        public  void Add(T entity)
        {
           _dbContext.Set<T>().Add(entity);
        }

        public async void Update(T entity)
        {
           _dbContext.Set<T>().Update(entity);
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}
