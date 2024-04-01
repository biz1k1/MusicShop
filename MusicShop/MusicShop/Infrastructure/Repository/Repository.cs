using Microsoft.EntityFrameworkCore;
using System.Linq;
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
        //public IQueryable<T> GetById(int id) {
        //    return _dbContext.Set<T>().Find(id);
        //}

        public IQueryable<T> GetAll()
        {
            return  _dbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression).AsNoTracking();
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
