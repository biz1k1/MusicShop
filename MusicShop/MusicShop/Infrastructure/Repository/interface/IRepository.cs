using System.Linq.Expressions;

namespace MusicShop.Infrastructure.Repository
{
    public interface IRepository<T>
    {
        Task<T> GetAll();
        Task<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}