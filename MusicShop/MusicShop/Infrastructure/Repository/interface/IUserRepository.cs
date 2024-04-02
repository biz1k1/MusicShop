using MusicShop.Domain.Model;

namespace MusicShop.Infrastructure.Repository
{
    public interface IUserRepository:IRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
    }
}
