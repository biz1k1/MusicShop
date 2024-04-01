using MusicShop.Domain.Model;

namespace MusicShop.Infrastructure.Repository
{
    public interface IUserRepository:IRepository<User>
    {
        Task<IEnumerable<User>> GetAllCategoryAsync();
        Task<User> GetCategoryByIdAsync(int id);
    }
}
