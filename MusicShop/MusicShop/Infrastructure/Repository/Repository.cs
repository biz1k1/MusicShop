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

        public async Task <T> GetAll()
        {
            //return _dbContext.Set<T>().AsNoTracking();
        }

        public async Task<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            //return _dbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}
