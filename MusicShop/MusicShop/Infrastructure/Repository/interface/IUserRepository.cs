using MusicShop.Domain.Enums;
using MusicShop.Domain.Model.Core;

namespace MusicShop.Infrastructure.Repository
{
    public interface IUserRepository:IRepository<UserEntity>
    {
        Task<IEnumerable<UserEntity>> GetAllUsersIncludeRoleAsync();
        Task<UserEntity?> GetUserByLoginAsync(string login);
        HashSet<Permissions> GetUserPermissions(int userId);
        Task<UserEntity?> GetUserIncludeRoleAsync(int id);
    }
}
