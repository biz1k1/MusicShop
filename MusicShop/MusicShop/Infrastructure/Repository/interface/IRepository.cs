using System.Linq.Expressions;

namespace MusicShop.Infrastructure.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);

        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);

    }
}